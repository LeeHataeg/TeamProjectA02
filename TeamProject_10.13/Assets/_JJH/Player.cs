using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    float originSpeed;
    public int playerScore;
    Rigidbody2D rb;

    public float magnetTime = 3f;
    public float magnetRange = 5f;
    public float magnetPower = 10f;
    private bool isMagneticActive = false;

    private static Player instance;
    public static Player Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originSpeed = maxSpeed;
    }

    void Update()
    {
        MovePlayer();
        Jump();
    }

    void FixedUpdate()
    {
        
    }

    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x <maxSpeed * (-1))
        {
            rb.velocity = new Vector2(maxSpeed * -1, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }






    #region Player Speed Up
    public void IncreaseSpeed(float currentRunningTime, float runningMultipleValue)
    {
        float changeSpeed = originSpeed * runningMultipleValue;
        print(changeSpeed);

        maxSpeed = changeSpeed;
        // ������ �ӵ��� ���� �ӵ��� �ǵ����� ���� �ڷ�ƾ ����
        StartCoroutine(ResetSpeedAfterTime(currentRunningTime, originSpeed));
    }

    private IEnumerator ResetSpeedAfterTime(float currentRunningTime, float originalSpeed)
    {
        yield return new WaitForSeconds(currentRunningTime);
        print(currentRunningTime);
        // currentRunninTime ���� ���� �ӵ��� ���ư�
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
            // �÷��̾��� ��ġ
            Vector2 playerPosition = transform.position;

            // ��� ItemData ������Ʈ�� ã��
            ItemData[] itemsInScene = FindObjectsOfType<ItemData>();

            foreach (ItemData item in itemsInScene)
            {
                // �����۰� �÷��̾� ������ �Ÿ��� ���
                float distanceToPlayer = Vector2.Distance(playerPosition, item.transform.position);

                // ���� �������� �ڼ� ȿ�� ���� ���� �ִٸ�
                if (distanceToPlayer <= magnetRange)
                {
                    // �ڼ� ȿ�� ������ ���
                    Vector2 forceDirection = (playerPosition - (Vector2)item.transform.position).normalized;

                    // �����ۿ� �ڼ� ȿ���� ����
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