using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public GameObject gameEndGroup;
    public Text score;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name != "_Stage2")
            {
                SceneManager.LoadScene("_Stage2");
            }
            else
            {
                // 현재 스테이지가 "_Stage2"일 때 게임 피니시로 처리
                GameFinish();
            }
        }
    }

    void GameFinish()
    {
        score.text = "점수 : " + PointManager.Instance.printScore.ToString();
        gameEndGroup.SetActive(true);
    }
}
