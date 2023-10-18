using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private string enemyTag = "Enemy";

    public float maxSpeed;
    float originSpeed;
    public float jumpForce;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public float magnetTime = 3f;
    public float magnetRange = 5f;
    public float magnetPower = 10f;
    private bool isMagneticActive = false;

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
        originSpeed = maxSpeed;
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

        //landing
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
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

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if(collision.gameObject.tag == enemyTag)
        {

        }
    }

    #region Player Speed Up
    public void IncreaseSpeed(float currentRunningTime, float runningMultipleValue)
    {
        float changeSpeed = originSpeed * runningMultipleValue;
        print(changeSpeed);

        maxSpeed = changeSpeed;
        // 증가한 속도를 원래 속도로 되돌리기 위한 코루틴 시작
        StartCoroutine(ResetSpeedAfterTime(currentRunningTime, originSpeed));
    }
    private IEnumerator ResetSpeedAfterTime(float currentRunningTime, float originalSpeed)
    {
        yield return new WaitForSeconds(currentRunningTime);
        print(currentRunningTime);
        // currentRunninTime 이후 원래 속도로 돌아감
        maxSpeed = originalSpeed;
    }
    #endregion

    #region Player Magnet Effect
    public void MagneticEffect()
    {
        if (!isMagneticActive)
        {
            StartCoroutine(ApplyMagneticEffectContinuously());
        }
    }

    private IEnumerator ApplyMagneticEffectContinuously()
    {
        isMagneticActive = true;
        float time = 0;

        while (isMagneticActive)
        {
            // 플레이어의 위치
            Vector2 playerPosition = transform.position;

            // 모든 ItemData 오브젝트를 찾음
            ItemData[] itemsInScene = FindObjectsOfType<ItemData>();

            foreach (ItemData item in itemsInScene)
            {
                // 아이템과 플레이어 사이의 거리를 계산
                float distanceToPlayer = Vector2.Distance(playerPosition, item.transform.position);

                // 만약 아이템이 자석 효과 범위 내에 있다면
                if (distanceToPlayer <= magnetRange)
                {
                    // 자석 효과 방향을 계산
                    Vector2 forceDirection = (playerPosition - (Vector2)item.transform.position).normalized;

                    // 아이템에 자석 효과를 적용
                    Rigidbody2D itemRigidbody = item.GetComponent<Rigidbody2D>();
                    if (itemRigidbody != null)
                    {
                        itemRigidbody.AddForce(magnetPower * forceDirection, ForceMode2D.Force);
                    }
                }
            }

            time += Time.deltaTime;

            yield return null;

            if (time > magnetTime)
            {

                isMagneticActive = false;
                yield break;
            }
        }
    }
    #endregion
}
