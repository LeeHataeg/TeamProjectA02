using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field : SerializeField] public PlayerGroundData groundData { get; private set; }
    [field : SerializeField] public PlayerAirData airData { get; private set; }
}
