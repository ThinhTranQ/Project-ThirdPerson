using UnityEngine;

public class BossDoor : MonoBehaviour, IInteractable
{
    public void DoAction()
    {
        UIManager.Instance.ShowBossEncounter();
    }
}