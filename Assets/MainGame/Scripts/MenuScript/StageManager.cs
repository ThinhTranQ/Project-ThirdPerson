using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Stage[] stages;

    private void Start()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            var stageData = PlayerPrefs.GetInt(TOPICNAME.STAGE + i);
            stages[i].InitData(i,stageData, i== 0);
        }
    }
}

public static class TOPICNAME
{
    public const string STAGE = "STAGE";
}

