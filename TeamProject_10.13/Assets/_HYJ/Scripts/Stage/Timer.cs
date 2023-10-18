using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float gameTime = 5.0f;
    private float currentTime;
    private bool isGameActive = false;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        StartGame();
        Time.timeScale = 1;
        currentTime = gameTime;
       
    }

    private void Update()
    {
        if (!isGameActive)
        {
            EndGame();
            return;
        }

        
        currentTime -= Time.deltaTime;


        timeText.text = "TIME " + currentTime.ToString("N2");

        if (currentTime <= 0)
        {
            isGameActive = false;

        }
    }

    public void StartGame()
    {
        isGameActive = true;
    }

    private void EndGame()
    {
        timeText.text = "TIME 0.00";
        Time.timeScale = 0;
        currentTime = 0;
        // 게임 종료 시 수행할 작업 추가
    }

   
}
