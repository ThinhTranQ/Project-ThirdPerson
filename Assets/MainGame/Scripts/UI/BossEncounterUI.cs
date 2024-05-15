using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MainGame.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

public class BossEncounterUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public CanvasGroup canvasGroupAll;

    public float fadeValue;

    public float fadeDuration;
    // Start is called before the first frame update
    
    [Button]
    void Start()
    {
        canvasGroup.DOFade(fadeValue, fadeDuration).SetLoops(3, LoopType.Yoyo).OnComplete(() =>
        {
            canvasGroupAll.DOFade(0, fadeDuration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
        
    }
    
}
