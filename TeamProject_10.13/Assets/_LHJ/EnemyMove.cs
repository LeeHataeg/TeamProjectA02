using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D collider2D;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think",5);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        // anim.SetInteger("WalkSpeed", nextMove);
        anim.SetBool("isWalk", false);
        if (nextMove != 0) spriteRenderer.flipX = nextMove == 1;
        if (nextMove != 0) spriteRenderer.flipX = nextMove == 1;
        
        float nextThinkTime = Random.Range(1f, 5f);
        Invoke("Think", nextThinkTime);
        anim.SetBool("isWalk", true);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 playerPos = collision.gameObject.transform.position;
            GameManager.Instance.ReduceHp(1);
            PointManager.Instance.AddScore(1000);
            StartCoroutine(PlayerController.Instance.PlayerFreeze());
            collision.gameObject.transform.position = new Vector3(playerPos.x - 5, playerPos.y + 2, 0);

        }
    }
}
