using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chur : ItemData
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            PlayerController.Instance.MagneticEffect();
        }
    }
}