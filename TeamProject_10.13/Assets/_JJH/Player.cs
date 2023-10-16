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






    #region Player Speed Up
    public void IncreaseSpeed(float currentRunningTime, float runningMultipleValue)
    {
        float changeSpeed = originSpeed * runningMultipleValue;
        print(changeSpeed);

        speed = changeSpeed;
        // ������ �ӵ��� ���� �ӵ��� �ǵ����� ���� �ڷ�ƾ ����
        StartCoroutine(ResetSpeedAfterTime(currentRunningTime, originSpeed));
    }

    private IEnumerator ResetSpeedAfterTime(float currentRunningTime, float originalSpeed)
    {
        yield return new WaitForSeconds(currentRunningTime);

        // currentRunninTime ���� ���� �ӵ��� ���ư�
        speed = originalSpeed;
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