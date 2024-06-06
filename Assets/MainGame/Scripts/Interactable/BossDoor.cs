using UnityEngine;

public class BossDoor : MonoBehaviour, IInteractable
{
    public void DoAction()
    {
        FindObjectOfType<UIManager>().ShowBossEncounter();
    }
}