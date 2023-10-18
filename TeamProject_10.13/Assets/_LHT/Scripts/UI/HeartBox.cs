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

    Vector3 plusPos = new Vector3(100f, 0, 0);

    private int exHP = 0;

    public void UpdateHP()
    {
        Debug.Log(transform.position);
        int heartCount = 0;
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            //기존 하트 모두 없애기
            Destroy(transform.GetChild(i).gameObject);
            Debug.Log("1");
        }
        for (int i = 0; i < GameManager.Instance.CurrentHP / 2; i++)
        {
            Instantiate(FullHeart, transform.position + plusPos*heartCount, Quaternion.identity, transform);
            heartCount++;
            Debug.Log("2");
        }
        for (int i = 0; i < GameManager.Instance.CurrentHP % 2; i++)
        {
            Instantiate(HalfHeart, transform.position + plusPos * heartCount, Quaternion.identity, transform);
            heartCount++;
        }
        for (int i = 0; i < (GameManager.Instance.MaxHP - GameManager.Instance.CurrentHP) / 2; i++)
        {
            Instantiate(EmptyHeart, transform.position + plusPos * heartCount, Quaternion.identity, transform);
            heartCount++;
        }
    }
}
