using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBox : MonoBehaviour
{
    public GameObject FullHeart;
    public GameObject HalfHeart;
    public GameObject EmptyHeart;

    private void Start()
    {
        UpdateHP(GameManager.Instance.CurrentHP);
    }

    public void UpdateHP(int hp)
    {
        int count = transform.childCount;
        for ( int i = 0; i < count; i++ )
        {
            Destroy (transform)
        }
    }
}
