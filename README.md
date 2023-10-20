# TeamProjectA02
![header](https://capsule-render.vercel.app/api?type=wave&color=auto&height=300&section=header&text=A02-임시(작업중)%20&fontSize=90)

 <img src="https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white"/> <img src="https://img.shields.io/badge/C sharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/>

# 유니티 심화 주차 팀 프로젝트
- A02조 이하택 임현진 정재호 함영주
<br>

## 🖥 프로젝트 소개 

<br>

### 🕰 **프로젝트 기간** : 2023.10.13 ~ 2023.10.20

### ✅ 개발 환경 
**Unity 2022.3.2f** 
-해상도 :  1980 * 1280
<br>

### ✅제작 과정
- 기획
    
[와이어 프레임](https://www.figma.com/file/mwMfzVmJ7YWsKoBJJx8fVk/%EC%8B%AC%ED%99%94%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8?type=whiteboard&node-id=0-1&t=ybKfU21kvG7C84Cj-0) <br>
[팀 노션](https://www.notion.so/02-55ee06ccbc1148429fee53db9ece7bc8)
<br>


<br>

## ✅ 게임 소개

## 🐈‍⬛ **게임명** : Hataeg, Attack!!
- 우리들의 깜찍한 고양이가 적(인간)들을 피해 아기 고양이를 구출 하는 여정의 플랫포머 2D 게임

<br>

### **게임 화면**

- 구현 사항
   - 고양이 데미지
     
      ![고양이_데미지](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/2da82f9e-5fb3-44ec-ae0c-0d319b800a8e)
     
   - 적 처치
     
      ![고양이_처치](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/a10d73ea-2627-479e-9298-9725f385078c)
     
   - 맵 전환
     
      ![고양이_맵 전환](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/70d7b174-5bbf-42d5-b119-b0ffc468c0af)
     
   - 게임 끝
     
      ![고양이_도착](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/8640b19b-1d63-4351-81aa-adca0c5ab2db)

<br>
<br>
<br>

## 이하택님
- Player 기능 담당 
- 1차 시도 : "StateMachine으로 구현하자"
  - 본강의를 참고하려 했으나, 3D캐릭터 움직임에 대한 State와 2D 플랫포머 캐릭터의 State의 차이를 메꾸지 못하고 시간을 소비했다.

- 결론 : 플레이어가 필요한 분이 많아 업데이트로 키 입력 확인하도록 프로토타입을 만들어 전해주었는데, 변경할 시간이 없었다....


```Cs
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
    Respawn();
}
```
<br>
<br>
<br>

## 정재호님
- 아이템 기능 담당
  
![image](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/787f482a-65aa-4d82-b800-7ca73b46c646)

- 아이템을 맵에 배치하여 Player태그와 충돌 시 Active 설정을 변경하도록 한다.

- 각 아이템은 속도, 자석효과 등이 있다.
![아이템 - 자석](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/79e89383-cd3b-428c-91d0-6e132dca27f2)

```cs

//........

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
```
<br>
<br>
<br>

## 함영주님
- 맵 기능 담당
- 타일맵 : 애니매이티드 타일, 움직이는 플랫폼, 물 플랫폼, 스프링 타일
- 타이머 : 타임어택
<br>
<br>
<br>

## 임현진님
- 적 기능 담당
<br>
<br>
<br>

