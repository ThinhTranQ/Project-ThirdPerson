using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public BlockDurability blockDurability;
    public Health          health;
    public RectTransform   progress;
    public RectTransform   healthProgress;

    private float maxHealthDurability;
    
    private float maxDurability;
    public  float speed;

    protected virtual void Awake()
    {
        maxDurability       = blockDurability.BLOCk_MAX;
        maxHealthDurability = health.MaxHealth;
    }

    protected virtual void Update()
    {
        transform.LookAt(Camera.main.transform);

        // UpdateHealth();
        UpdateBlock();

    }

    protected virtual void UpdateHealth()
    {
        var percent = health.CurrentHealth / maxHealthDurability;
        if (percent >= 1) return;
        var newWidth = healthProgress.sizeDelta.x;
        
        newWidth                 = Mathf.Lerp(newWidth, percent * 1080, Time.deltaTime * speed);
        healthProgress.sizeDelta = new Vector2(newWidth, healthProgress.sizeDelta.y);
    }
    
    protected virtual void UpdateBlock()
    {
        var percent = blockDurability.currentBlock / maxDurability;
        if (percent >= 1) return;
        var newWidth = progress.sizeDelta.x;
        
        newWidth           = Mathf.Lerp(newWidth, percent * 1080, Time.deltaTime * speed);
        progress.sizeDelta = new Vector2(newWidth, progress.sizeDelta.y);
    }
}