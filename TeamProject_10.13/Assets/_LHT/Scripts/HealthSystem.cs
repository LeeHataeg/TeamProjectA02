using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //public event Action OnDamage;
    //public event Action OnHeal;
    //public event Action OnDeath;
    //public event Action OnInvincibilityEnd;

    public AudioClip damageClip;
    public float c1urrentHealth { get; private set; }
    public float M1axHealth;
    private void Awake()
    {
        
    }
}
