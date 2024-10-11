using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float moveRangeX = 20f;
    [SerializeField] float moveRangeZ = 20f;
    private float speed;
    private Vector3 startPos; //초기 위치
    private Vector3 targetPos; //이동할 목표 위치
    private bool moveRight;

    private void Start()
    {
        startPos = transform.position; //초기 위치 저장

        //적의 초기 회전값에 따라 이동 방향 설정
        if (transform.rotation.eulerAngles.y == 90f)
        {
            //오른쪽으로 이동
            moveRight = true;
        }
        else if (transform.rotation.eulerAngles.y == -90f)
        {
            //왼쪽으로 이동
            moveRight = false;
        }

        SetRandomTargetPosition();
    }

    private void Update()
    {
        //목표 위치로 향하는 방향 계산
        Vector3 direction = (targetPos - transform.position).normalized;

        //적이 해당 방향을 바라보게 회전
        if (direction != Vector3.zero) //회전할 방향이 존재하는지 확인
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        //적을 목표 위치로 이동시킴
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        //만약에 목표 위치에 다왔다면 새로운 랜덤 목표 위치를 설정함
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    //랜덤 목표 위치 설정
    private void SetRandomTargetPosition()
    {
        float randomX;

        if (moveRight)
        {
            //적이 오른쪽으로 이동 중이라면
            //시작위치부터 이동 가능 범위 중에서 랜덤으로 선택
            randomX = Random.Range(startPos.x + 7f, startPos.x + moveRangeX);
        }
        else
        {
            //왼쪽으로 이동 중이라면
            //반대로 끝부터 시작위치까지 랜덤으로 선택
            randomX = Random.Range(startPos.x - moveRangeX, startPos.x - 7f);
        }

        //Z축 이동 좌표도 랜덤 설정
        float randomZ = Random.Range(startPos.z + 7f - moveRangeZ, startPos.z + 7f + moveRangeZ);
        //Y축은 처음 시작한 위치 그대로
        targetPos = new Vector3(randomX, startPos.y, randomZ);

        //몬스터 움직임 속도 랜덤 선택
        speed = Random.Range(2f, 8f);
    }
}
