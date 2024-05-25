using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public List<BaseSkill> baseSkills;

    private Dictionary<BaseSkill, bool> listSkills;

    public int CurrentSkillIndex { get; private set; }

    public bool isUsingSkill;
    private void Awake()
    {
        listSkills = new Dictionary<BaseSkill, bool>();
        for (var i = 0; i < baseSkills.Count; i++)
        {
            bool isUnlock = PlayerPrefs.GetInt(TOPICNAME.SKILL + i) == 1;
            if (isUnlock)
            {
                listSkills.Add(baseSkills[i], true);
            }
        }

        //for testing
        listSkills.Add(baseSkills[0], true);
    }

    public bool IsAnySkillAvailable()
    {
        return listSkills.Count > 0;
    }

    public BaseSkill GetCurrentSkill()
    {
        return baseSkills[CurrentSkillIndex];
    }

    public void ActiveCurrentSkill()
    {
       
    }

    public bool IsUsingSkill()
    {
        return isUsingSkill;
    }
}