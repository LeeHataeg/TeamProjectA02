using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private float speed;
    [SerializeField] private float originSpeed = 4f;
    public float jumpForce;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        speed = originSpeed;
    }

    private void Update()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector2.down * 0.8f);
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump"))
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
        }

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("IsWalk", false);
        }
        else
        {
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsWalk", true);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        ////Move Speed
        //float h = Input.GetAxisRaw("Horizontal");
        //rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        ////Max Speed
        //if (rigid.velocity.x > speed)
        //    rigid.velocity = new Vector2(speed, rigid.velocity.y);
        //else if (rigid.velocity.x < speed * (-1))
        //    rigid.velocity = new Vector2(speed * (-1), rigid.velocity.y);

        //Landing
        //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                
                if (rayHit.distance < 0.8f)
                {
                    anim.SetBool("IsJump", false);
                }
            }
        }
    }

    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector3(h, 0, 0).normalized * speed * Time.deltaTime;
    }

    #region Player Speed Up
    public void IncreaseSpeed(float currentRunningTime, float runningMultipleValue)
    {
        float changeSpeed = originSpeed * runningMultipleValue;
        print(changeSpeed);

        speed = changeSpeed;
        // 증가한 속도를 원래 속도로 되돌리기 위한 코루틴 시작
        StartCoroutine(ResetSpeedAfterTime(currentRunningTime, originSpeed));
    }

    private IEnumerator ResetSpeedAfterTime(float currentRunningTime, float originalSpeed)
    {
        yield return new WaitForSeconds(currentRunningTime);

        // currentRunninTime 이후 원래 속도로 돌아감
        speed = originalSpeed;
    }
    #endregion
}
