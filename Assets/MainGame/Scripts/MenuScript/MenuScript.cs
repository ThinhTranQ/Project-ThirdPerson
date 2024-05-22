using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [Header("Menu")]
    public Button btnStartGame;
    public Button btnExit;

    public GameObject menuPanel;

    [Header("Level Selection")] 
    public Button btnBack;
    
    
    private void Awake()
    {
        btnStartGame.onClick.AddListener(OnStartGame);
        btnBack.onClick.AddListener(OnBackMenu);
        
        menuPanel.SetActive(true);
    }

    private void OnBackMenu()
    {
       LoadingScene.instance.TriggerStagePanel(false);
    }

    private void OnStartGame()
    {
        LoadingScene.instance.TriggerStagePanel(true);
    }
}
