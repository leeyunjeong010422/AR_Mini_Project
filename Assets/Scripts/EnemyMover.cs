using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float moveRangeX = 20f;
    [SerializeField] float moveRangeZ = 20f;
    private float speed;
    private Vector3 startPos; //�ʱ� ��ġ
    private Vector3 targetPos; //�̵��� ��ǥ ��ġ
    private bool moveRight;

    private void Start()
    {
        startPos = transform.position; //�ʱ� ��ġ ����

        //���� �ʱ� ȸ������ ���� �̵� ���� ����
        if (transform.rotation.eulerAngles.y == 90f)
        {
            //���������� �̵�
            moveRight = true;
        }
        else if (transform.rotation.eulerAngles.y == -90f)
        {
            //�������� �̵�
            moveRight = false;
        }

        SetRandomTargetPosition();
    }

    private void Update()
    {
        //��ǥ ��ġ�� ���ϴ� ���� ���
        Vector3 direction = (targetPos - transform.position).normalized;

        //���� �ش� ������ �ٶ󺸰� ȸ��
        if (direction != Vector3.zero) //ȸ���� ������ �����ϴ��� Ȯ��
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        //���� ��ǥ ��ġ�� �̵���Ŵ
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        //���࿡ ��ǥ ��ġ�� �ٿԴٸ� ���ο� ���� ��ǥ ��ġ�� ������
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    //���� ��ǥ ��ġ ����
    private void SetRandomTargetPosition()
    {
        float randomX;

        if (moveRight)
        {
            //���� ���������� �̵� ���̶��
            //������ġ���� �̵� ���� ���� �߿��� �������� ����
            randomX = Random.Range(startPos.x + 7f, startPos.x + moveRangeX);
        }
        else
        {
            //�������� �̵� ���̶��
            //�ݴ�� ������ ������ġ���� �������� ����
            randomX = Random.Range(startPos.x - moveRangeX, startPos.x - 7f);
        }

        //Z�� �̵� ��ǥ�� ���� ����
        float randomZ = Random.Range(startPos.z + 7f - moveRangeZ, startPos.z + 7f + moveRangeZ);
        //Y���� ó�� ������ ��ġ �״��
        targetPos = new Vector3(randomX, startPos.y, randomZ);

        //���� ������ �ӵ� ���� ����
        speed = Random.Range(2f, 8f);
    }
}
