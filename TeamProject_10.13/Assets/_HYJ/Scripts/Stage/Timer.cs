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
            GameManager.Instance.GameOver();
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

  

   
}
