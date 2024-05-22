using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameLose : MonoBehaviour
{
    public Button btnRetry;

    public Button btnSelectAnotherStage;
    private void Start()
    {
        btnRetry.onClick.AddListener(() =>
        {
            var currentScene = SceneManager.GetActiveScene().name;
            LoadingScene.instance.LoadScene(currentScene);
            gameObject.SetActive(false);
        });
        
        btnSelectAnotherStage.onClick.AddListener(() =>
        {
            LoadingScene.instance.TriggerStagePanel(true);
            gameObject.SetActive(false);
        });
    }
}