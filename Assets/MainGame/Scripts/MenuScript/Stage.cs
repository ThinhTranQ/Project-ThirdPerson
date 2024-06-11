using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int          index;
    public int          numberStar;
    public TMP_Text     stageName;
    public GameObject[] starContainer;

    public GameObject lockPanel;
    public GameObject unlockPanel;
    
    
    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(StartLoadingScene);
        
    }

    private void StartLoadingScene()
    {
        LoadingScene.instance.LoadScene($"GamePlay {index}");
       
    }

    public void InitData(int index, int star, bool isFirstStage)
    {
        this.index     = index;
        numberStar     = star;
        stageName.text = (index+1).ToString();
        if (numberStar <= 0 && isFirstStage)
        {
            lockPanel.gameObject.SetActive(true);
            for (int i = 0; i < starContainer.Length; i++)
            {
                starContainer[i].SetActive(i < star);
            }
            return;
        }
        lockPanel.gameObject.SetActive(false);
        unlockPanel.gameObject.SetActive(true);
        for (int i = 0; i < starContainer.Length; i++)
        {
            starContainer[i].SetActive(i < star);
        }
        
    }
    
    
}