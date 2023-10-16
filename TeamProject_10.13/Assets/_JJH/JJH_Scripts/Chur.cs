using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chur : ItemData
{
    

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // 기존의 처리를 수행
    }
}
