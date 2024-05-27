using System;
using MainGame.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameWin : MonoBehaviour
{
    public Button      btnNextGame;
    public int         stageIndex;
    public SkillData[] skillDataDetails;
    public GameObject  claimRewardPanel;

    public TMP_Text skillName;
    public TMP_Text skillDescription;
    public Image    skillSprite;

    public Button btnClose;
    private void Start()
    {
        btnNextGame.onClick.AddListener(() =>
        {
            LoadingScene.instance.TriggerStagePanel(true);
            gameObject.SetActive(false);
        });
        
        btnClose.onClick.AddListener(() =>
        {
            claimRewardPanel.SetActive(false);
        });
    }

    private void OnEnable()
    {
        if (!IsClaimedAbility())
        {
            PlayerPrefs.SetInt(TOPICNAME.SKILL + stageIndex, 1);
            var skillData = skillDataDetails[stageIndex];
            InitSkillDetails(skillData);
            claimRewardPanel.SetActive(true);
        }
        else
        {
            GameLogger.Log("Ability has already claimed");
        }
    }

    private bool IsClaimedAbility()
    {
        return PlayerPrefs.GetInt(TOPICNAME.SKILL + stageIndex) == 1;
    }

    private void InitSkillDetails(SkillData data)
    {
        skillName.text        = data.skillName;
        skillDescription.text = data.skillDescription;
        skillSprite.sprite    = data.skillIcon;
    }
}