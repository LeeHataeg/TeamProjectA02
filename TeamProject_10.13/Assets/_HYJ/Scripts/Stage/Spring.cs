using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public Animator anim;
    public SpriteRenderer SR;

    private void Awake()
    {
        SR.enabled = false;
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) // 플레이어가 애니매이션 영역을 밟았을 때 애니메이션 효과가 켜짐
        {
            anim.enabled = true; 
        }
    }

    public void animOff()
    {
        anim.enabled = false;
        SR.enabled = false; 
    }
}
