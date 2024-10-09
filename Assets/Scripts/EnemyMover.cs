using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float moveRangeX = 10f;
    [SerializeField] float moveRangeZ = 10f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moveRight;

    void Start()
    {
        startPos = transform.position;

        if (transform.rotation.eulerAngles.y == 90f)
        {
            moveRight = true;
        }
        else if (transform.rotation.eulerAngles.y == -90f)
        {
            moveRight = false;
        }

        SetRandomTargetPosition();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX;
        if (moveRight)
        {
            randomX = Random.Range(startPos.x, startPos.x + moveRangeX);
        }
        else
        {
            randomX = Random.Range(startPos.x - moveRangeX, startPos.x);
        }

        float randomZ = Random.Range(startPos.z - moveRangeZ, startPos.z + moveRangeZ);
        targetPos = new Vector3(randomX, startPos.y, randomZ);
    }
}
