using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field : SerializeField][field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 5f;
    //[field : Serialze]

    [field : Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 25f)] public float walkSpeedModifier { get; private set; } = 0.225f;
}
