using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField]
    private int score;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("ASD");
            Player.Instance.AddScore(score);
            gameObject.SetActive(false);
        }
    }
}