using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillData", order = 0)]
public class SkillData : ScriptableObject
{
    public Sprite skillIcon;
    public string skillName;
    public string skillDescription;

}