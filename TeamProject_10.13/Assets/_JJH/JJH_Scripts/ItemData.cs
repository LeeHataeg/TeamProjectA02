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
            //Player.Instance.AddScore(score);
            gameObject.SetActive(false);
        }
    }

    #region Legacy Code. Can't stop AllCoroutines when I want...


    //[SerializeField]
    //[Range(0, 0.1f)] private float animYPos = 0.001f;
    //[SerializeField]
    //private float animDelayTime;

    //private bool isAnimation = false; ////Change Animation Direction

    //private Coroutine animCoroutine;

    //public virtual void Awake()
    //{
    //    StartAnimation();
    //}

    //public void StartAnimation()
    //{
    //    if (animCoroutine == null)
    //    {
    //        animCoroutine = StartCoroutine(ItemAnim());
    //    }
    //}


    //public IEnumerator ItemAnim()
    //{
    //    float itemPosX = gameObject.transform.position.x;
    //    float itemPosY = gameObject.transform.position.y;

    //    float originalItemPosY = itemPosY;


    //    while (true)
    //    {
    //        gameObject.transform.position = new Vector3(itemPosX, itemPosY);

    //        if (!isAnimation)
    //        {
    //            itemPosY += animYPos;

    //            if (originalItemPosY - itemPosY <= -0.1f)
    //            {
    //                isAnimation = true;
    //            }
    //        }
    //        else
    //        {
    //            itemPosY -= animYPos;

    //            if (originalItemPosY - itemPosY >= 0.1f)
    //            {
    //                isAnimation = false;
    //            }
    //        }

    //        yield return new WaitForSeconds(animDelayTime);
    //    }
    //} 

    #endregion
}