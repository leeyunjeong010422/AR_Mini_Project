using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] birds;
    [SerializeField] int enemyCount;

    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        while (enemyCount < 10)
        {
            int xPos = Random.Range(-10, 10);
            int zPos = Random.Range(6, 20);

            int randomBirdIndex = Random.Range(0, birds.Length);
            GameObject randomBird = birds[randomBirdIndex];

            //90도 또는 -90도 중 하나의 값을 랜덤으로 선택
            float randomRotationY = (Random.Range(0, 2) == 0) ? 90f : -90f;
            Quaternion birdRotation = Quaternion.Euler(0, randomRotationY, 0);

            Instantiate(randomBird, new Vector3(xPos, Random.Range(-1, 1), zPos), birdRotation);

            yield return new WaitForSeconds(2);
            enemyCount += 1;
        }
    }
}
