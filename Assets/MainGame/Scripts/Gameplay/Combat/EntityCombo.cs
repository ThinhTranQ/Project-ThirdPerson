using System.Collections.Generic;
using UnityEngine;

public class EntityCombo : MonoBehaviour
{
    public List<ComboList> comboLists2;

    protected ComboList currentCombo;

    protected int currentIndex;

    public bool canChangeCombo;

    protected virtual void ImplementCombo()
    {
        
    }

    public virtual Attack GetCurrentAttackInCombo(int index)
    {
        if (index >= currentCombo.attacks.Count)
        {
            index = currentCombo.attacks.Count - 1;
        }
        
        return currentCombo.attacks[index];
    }

    public virtual void ChangeCombo()
    {
        
    }
}