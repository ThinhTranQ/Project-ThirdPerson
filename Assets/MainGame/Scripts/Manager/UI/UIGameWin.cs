using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameWin : MonoBehaviour
{
    public Button btnNextGame;

    private void Start()
    {
        btnNextGame.onClick.AddListener(() =>
        {
            LoadingScene.instance.TriggerStagePanel(true);
            gameObject.SetActive(false);
        });
    }
}