﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField] public string    AnimationName      { get; private set; }
    [field: SerializeField] public float     TransitionDuration { get; private set; }
    [field: SerializeField] public int       ComboStateIndex    { get; private set; } = -1;
    [field: SerializeField] public float     CanTransitionCombo { get; private set; }
    [field: SerializeField] public float     ForceTime          { get; private set; }
    [field: SerializeField] public float     Force              { get; private set; }
    [field: SerializeField] public float     Damage             { get; private set; }
    [field: SerializeField] public float     KnockBack          { get; private set; }
    [field: SerializeField] public bool     CantFacePlayer          { get; private set; }
    [field: SerializeField] public bool     CanBeInterrupt          { get; private set; }
}
