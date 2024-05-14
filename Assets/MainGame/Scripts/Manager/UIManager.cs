using MainGame.Utils;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IUIManager
{
    public static IUIManager Instance;

    public GameObject uiWin;
    public GameObject uiLose;
    protected override void Initial()
    {
        base.Initial();
        Instance = InstancePrivate;
    }
    
    public void ShowGameWin()
    {
        uiWin.SetActive(true);
    }

    public void ShowGameLose()
    {
        uiLose.SetActive(false);
    }
}


public interface IUIManager
{
    public void ShowGameWin();
    public void ShowGameLose();
}