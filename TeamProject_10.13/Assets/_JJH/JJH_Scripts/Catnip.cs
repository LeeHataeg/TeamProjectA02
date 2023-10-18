using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : ItemData
{
    [SerializeField]
    private float runningTime;
    [SerializeField]
    private float runningMultipleValue;
    float currentRunningTime;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            currentRunningTime = runningTime;
            PlayerController.Instance.IncreaseSpeed(currentRunningTime, runningMultipleValue);
        }
    }
}