using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossCombo", menuName = "BossCombo", order = 0)]
public class ComboList : ScriptableObject
{
    public AnimatorOverrideController animator;
    public float                      attackRange;
    public List<Attack>               attacks;

    public void ImplementCombo(EnemyStateMachine enemy)
    {
       enemy.SetAnimator(animator);
        
        
    }
}