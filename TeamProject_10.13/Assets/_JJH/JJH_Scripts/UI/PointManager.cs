using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    public int printScore = 0;

    private static PointManager instance;
    public static PointManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int score)
    {
        printScore += score;
        scoreTxt.text = printScore.ToString();
    }
}
