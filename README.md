# TeamProjectA02
![header](https://capsule-render.vercel.app/api?type=wave&color=auto&height=300&section=header&text=A02-임시(작업중)%20&fontSize=90)

 <img src="https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white"/> <img src="https://img.shields.io/badge/C sharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/>

## 게임 컨셉

[팀 노션](https://www.notion.so/02-55ee06ccbc1148429fee53db9ece7bc8)

## 게임 소개

- 게임 이름 : Hataeg, Attack!!

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

![image](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/787f482a-65aa-4d82-b800-7ca73b46c646)

- 아이템을 맵에 배치하여 Player태그와 충돌 시 Active 설정을 변경하도록 한다.

- 각 아이템은 속도, 자석효과 등이 있다.
![아이템 - 자석](https://github.com/LeeHataeg/TeamProjectA02/assets/139848355/79e89383-cd3b-428c-91d0-6e132dca27f2)

<br>
<br>
<br>

## 함영주님

<br>
<br>
<br>

## 임현진님

<br>
<br>
<br>

