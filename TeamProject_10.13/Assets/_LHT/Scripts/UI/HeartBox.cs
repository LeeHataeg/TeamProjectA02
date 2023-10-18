using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBox : MonoBehaviour
{
    public GameObject FullHeart;
    public GameObject HalfHeart;
    public GameObject EmptyHeart;

    Vector3 initPos = new Vector3(100f, 0, 0);

    private int exHP = 0;

    private void Start()
    {
        UpdateHP();
    }
    public void UpdateHP()
    {
        int heartCount = 0;
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < GameManager.Instance.CurrentHP / 2; i++)
        {
            Instantiate(FullHeart, transform.position + initPos*heartCount, Quaternion.identity, transform);
            heartCount++;
        }
        for (int i = 0; i < GameManager.Instance.CurrentHP % 2; i++)
        {
            Instantiate(HalfHeart, transform.position + initPos * heartCount, Quaternion.identity, transform);
            heartCount++;
        }
        for (int i = 0; i < (GameManager.Instance.MaxHP - GameManager.Instance.CurrentHP) / 2; i++)
        {
            Instantiate(EmptyHeart, transform.position + initPos * heartCount, Quaternion.identity, transform);
            heartCount++;
        }
    }
}
