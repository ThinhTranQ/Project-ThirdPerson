using System;
using Cysharp.Threading.Tasks;
using MainGame.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : Singleton<LoadingScene>
{
     public static LoadingScene instance;

     private float      value;
     public  GameObject loadingPanel;
     public  GameObject stageSelectPanel;
     public  Slider     loadingSlider;

     private bool startLoad;
     protected override void Initial()
     {
          base.Initial();
          instance = InstancePrivate;
     }

     private void Update()
     {
          if (!startLoad) return;
          loadingSlider.value = Mathf.Lerp(loadingSlider.value, value,  Time.deltaTime);
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
          value     = 0;
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
          await UniTask.Delay(300);
          loadingPanel.SetActive(false);
     }
}