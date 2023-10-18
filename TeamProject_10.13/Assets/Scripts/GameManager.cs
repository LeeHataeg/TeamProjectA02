using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region SceneName

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

    public GameObject HeartBox;
    #endregion

    HeartBox heart;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        heart = HeartBox.GetComponent<HeartBox>();
    }

    private void Start()
    {
        maxHP = 6;
        currentHP = maxHP;
        heart.UpdateHP();
    }

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
        heart.UpdateHP();
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
