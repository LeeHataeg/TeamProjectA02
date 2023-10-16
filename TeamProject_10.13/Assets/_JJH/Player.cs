using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public int playerScore;

    Rigidbody2D rb;

    [SerializeField]
    private float magnetTime = 5.0f; // 자석 효과 지속 시간
    [SerializeField]
    private float magnetForce = 5.0f; // 자석 효과 범위

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

    public void IncreaseSpeed(float currentRunninTime, float runningMultipleValue)
    {
        float originSpeed = speed;
        float changeSpeed = speed * runningMultipleValue;

        speed = changeSpeed;
        StartCoroutine(ResetSpeedAfterTime(currentRunninTime, originSpeed));
    }

    private IEnumerator ResetSpeedAfterTime(float delay, float originalSpeed)
    {
        yield return new WaitForSeconds(delay);

        // currentRunninTime 이후 원래 속도로 돌아감
        speed = originalSpeed;
    }
}
