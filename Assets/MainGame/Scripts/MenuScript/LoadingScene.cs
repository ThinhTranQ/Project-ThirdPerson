using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MainGame.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : Singleton<LoadingScene>
{
    public static LoadingScene instance;

    private float      value;
    public  GameObject loadingPanel;
    public  GameObject stageSelectPanel;
    public  GameObject introPanel;

    public Image    blackScreen;
    public TMP_Text loadingText;

    public Slider loadingSlider;

    private bool startLoad;

    protected override void Initial()
    {
        base.Initial();
        instance = InstancePrivate;
    }

    private void Start()
    {
        Sequence fadeSequence = DOTween.Sequence();

        fadeSequence.Append(blackScreen.DOFade(0f, 1f))
                    .AppendInterval(2)
                    .Append(blackScreen.DOFade(1, 1f)).OnComplete(() =>
                    {
                        introPanel.SetActive(false);
                        blackScreen.gameObject.SetActive(false);
                        // gameObject.SetActive(false);
                    });
    }
    
    
    private void Update()
    {
        if (!startLoad) return;
        loadingSlider.value = Mathf.Lerp(loadingSlider.value, value, Time.deltaTime);
        loadingText.text    = $"Loading... {Mathf.RoundToInt(loadingSlider.value) * 100}";
    }

    public void TriggerStagePanel(bool isOn)
    {
        stageSelectPanel.SetActive(isOn);
    }

    public async void LoadScene(string sceneName)
    {
        TriggerStagePanel(false);
        startLoad = true;
        loadingPanel.SetActive(true);
        value = 0;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        do
        {
            await UniTask.Delay(200);
            value = loadOperation.progress;
        } while (loadOperation.progress < 0.9f);

        value = 100;

        await UniTask.Delay(1000);

        loadOperation.allowSceneActivation = true;
        await UniTask.Delay(1000);
        loadingPanel.SetActive(false);
    }
}