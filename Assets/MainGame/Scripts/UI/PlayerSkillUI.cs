using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUI : MonoBehaviour
{
    public Image skillSprite;

    public BaseSkill currentSkill;

    private bool startFetch;

    public Image cooldownImg;
    
    
    public void InitSkill(BaseSkill skill)
    {
        currentSkill = skill;
        startFetch   = skill != null;
        cooldownImg.gameObject.SetActive(cooldownImg);
        skillSprite.sprite = skill.iconSkill;
    }

    private void Update()
    {
        if (!startFetch) return;
        
        // update Slider
        var maxCd = currentSkill.maxSkillCD;
        var currentCd = currentSkill.SkillCD;

        cooldownImg.fillAmount = currentCd / maxCd;
        
        cooldownImg.gameObject.SetActive(cooldownImg.fillAmount <= 0.95f);

    }
}