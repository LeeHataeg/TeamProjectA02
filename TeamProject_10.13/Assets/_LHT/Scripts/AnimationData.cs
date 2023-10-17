using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string walkParameterName = "IsWalk";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "IsJump";
    [SerializeField] private string landParameterName = "Land";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
    }
}
