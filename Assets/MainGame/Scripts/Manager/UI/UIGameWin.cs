using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGameWin : MonoBehaviour
{
    public Button btnNextGame;
    public int    stageIndex;
    private void Start()
    {
        btnNextGame.onClick.AddListener(() =>
        {
            LoadingScene.instance.TriggerStagePanel(true);
            gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt(TOPICNAME.SKILL + stageIndex, 1);
    }
}