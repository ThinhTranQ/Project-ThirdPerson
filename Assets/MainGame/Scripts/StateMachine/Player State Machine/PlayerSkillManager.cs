using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    private PlayerStateMachine playerStateMachine;
    private PlayerSkillUI      skillUI;
    public  List<BaseSkill>    baseSkills;

    private Dictionary<int, BaseSkill> listSkills;

    public int currentSkillIndex { get; private set; }

    public bool isUsingSkill;

    private BaseSkill currentSkill;

    private void Awake()
    {
        skillUI            = FindObjectOfType<PlayerSkillUI>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        listSkills         = new Dictionary<int, BaseSkill>();
        for (var i = 0; i < baseSkills.Count; i++)
        {
            bool isUnlock = PlayerPrefs.GetInt(TOPICNAME.SKILL + i) == 1;
            var tempSkill = baseSkills[i];
            if (isUnlock)
            {
                tempSkill.UpdateSkillStatus(true);
                listSkills.Add(i, tempSkill);
            }
            else
            {
                tempSkill.UpdateSkillStatus(false);
            }
        }

       
        if (listSkills.Count > 0)
        {
            currentSkill = listSkills[0];


            playerStateMachine.InputReader.SkillChangeEvent += OnPlayerChangeSkill;
            skillUI.InitSkill(currentSkill);
        }
        else
        {
            skillUI.TriggerSkillUI(false);
        }
    }


    [Button]
    public void TestUnlockAllSkill()
    {
        for (var i = 0; i < baseSkills.Count; i++)
        {
            PlayerPrefs.SetInt(TOPICNAME.SKILL + i, 1);
            var currentSkill = baseSkills[i];
            currentSkill.UpdateSkillStatus(true);
            listSkills.Add(i, currentSkill);
        }
    }

    public bool IsCurrentSkillIsDoneCD()
    {
        return currentSkill.SkillCD <= 0;
    }

    private void OnPlayerChangeSkill()
    {
        if (!IsAnySkillAvailable()) return;
        currentSkillIndex++;
        if (currentSkillIndex >= listSkills.Count)
        {
            currentSkillIndex = 0;
        }

        print("Change skill:" + listSkills[currentSkillIndex].gameObject.name);
        currentSkill = listSkills[currentSkillIndex];
        skillUI.InitSkill(currentSkill);
    }

    private void OnDisable()
    {
        playerStateMachine.InputReader.SkillChangeEvent -= OnPlayerChangeSkill;
    }

    public bool IsAnySkillAvailable()
    {
        return listSkills.Count > 0;
    }

    public BaseSkill GetCurrentSkill()
    {
        return currentSkill;

        if (listSkills.ContainsKey(currentSkillIndex))
        {
            return listSkills[currentSkillIndex];
        }

        return listSkills[0];
    }
}