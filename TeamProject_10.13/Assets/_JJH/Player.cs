using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
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
        originSpeed = speed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector3(h, v, 0).normalized * speed * Time.deltaTime;
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void IncreaseSpeed(float currentRunningTime , float runningMultipleValue)
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
            yield return null; // 매 프레임마다 업데이트

            if (time > magnetTime)
            {

                isMagneticActive = false;
                yield break;
            }
            // 계속 적용하도록 설정한 시간이 지난 후에 자석 효과 중지
            // 예를 들어, 자석 효과를 사용하는 키 입력에 따라 조절 가능
        }

    }

    // 자석 효과를 중지하는 메서드
    public void StopMagneticEffect()
    {
        isMagneticActive = false;
    }
}