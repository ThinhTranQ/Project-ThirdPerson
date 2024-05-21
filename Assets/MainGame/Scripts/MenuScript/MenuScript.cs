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
    
    public GameObject selectionPanel;
    
    
    
    private void Awake()
    {
        btnStartGame.onClick.AddListener(OnStartGame);
        btnBack.onClick.AddListener(OnBackMenu);
        
        menuPanel.SetActive(true);
        selectionPanel.SetActive(false);
    }

    private void OnBackMenu()
    {
        selectionPanel.gameObject.SetActive(false);
    }

    private void OnStartGame()
    {
        selectionPanel.gameObject.SetActive(true);
    }
}
