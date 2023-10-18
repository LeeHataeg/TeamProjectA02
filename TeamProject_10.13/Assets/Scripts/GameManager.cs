using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    #region Player Heart
    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        private set { maxHP = Mathf.Clamp(value, 0, 10); }
    }

    private int currentHP;
    public int CurrentHP
    {
        get { return currentHP; }
        private set { currentHP = Mathf.Clamp(value, 0, maxHP); }
        }
    #endregion

}
