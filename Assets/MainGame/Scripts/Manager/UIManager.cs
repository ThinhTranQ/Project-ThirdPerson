using MainGame.Utils;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    public static IUIManager Instance;

    public GameObject uiPlayer;
    public GameObject uiWin;
    public GameObject uiLose;
    public GameObject uiBossEncounter;
    // protected override void Initial()
    // {
    //     base.Initial();
    //     Instance = InstancePrivate;
    // }
    
    public void ShowGameWin()
    {
        uiWin.SetActive(true);
        
    }

    public void ShowGameLose()
    {
        uiLose.SetActive(true);
    }

    public void ShowBossEncounter()
    {
        uiBossEncounter.SetActive(true);
    }
}


public interface IUIManager
{
    public void ShowGameWin();
    public void ShowGameLose();

    public void ShowBossEncounter();
}