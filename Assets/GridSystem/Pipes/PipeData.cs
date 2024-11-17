using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct PipeData
{
    public PipeType PipeType;
    public int Rotation;
    public int energy;
    public bool isInteractable;
}
