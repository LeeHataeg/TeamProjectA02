using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    #region 문자열그 뭐더라 뭐라고 부르더라 그그그

    private string startSceneName = "StartScene";
    private string firstSceneName = "_Stage1";
    private string secondSceneName = "_Stage2";

    #endregion

    #region Player Heart Variable
    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        private set { maxHP = Mathf.Clamp(value, 0, 6); }
    }

    private int currentHP;
    public int CurrentHP
    {
        get { return currentHP; }
        private set { currentHP = Mathf.Clamp(value, 0, maxHP); }
    }

    HeartBox heart;
    #endregion



    public void ReduceHp(int damage)
    {
        currentHP -= damage;
        heart.UpdateHP();

        if (currentHP <= 0)
            GameOver();
    }
    public void TakeHeal(int heal)
    {
        currentHP = (currentHP + heal > maxHP) ? maxHP : currentHP + heal;
    }

    private void GameOver()
    {
        //GameOverUI
        
    }
    private void GameStart()
    {
        SceneManager.LoadScene(firstSceneName);
    }
    public void MoveScene()
    {
        SceneManager.LoadScene(secondSceneName);
    }
}
