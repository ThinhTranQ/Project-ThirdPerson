using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public BlockDurability blockDurability;
    public  RectTransform progress;

    private float maxDurability;
    public  float speed;

    private void Awake()
    {
        maxDurability   = blockDurability.BLOCk_MAX;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);

        var percent = blockDurability.currentBlock / maxDurability;
        if (percent >= 1) return;
        var newWidth = progress.sizeDelta.x;
        
        newWidth           = Mathf.Lerp(newWidth, percent * 1080, Time.deltaTime * speed);
        progress.sizeDelta = new Vector2(newWidth, progress.sizeDelta.y);
    }
}