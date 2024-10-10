using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnController : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] int enemyCount = 20;
    private int currentEnemyCount;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (currentEnemyCount <= enemyCount)
        {
            SpawnBird();
            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnBird()
    {
        GameObject bird = pool.GetPooledObject();
        if (bird != null)
        {
            int xPos = Random.Range(-10, 10);
            int zPos = Random.Range(6, 20);
            float randomRotationY = (Random.Range(0, 2) == 0) ? 90f : -90f;

            bird.transform.position = new Vector3(xPos, Random.Range(-1, 1), zPos);
            bird.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);
            bird.SetActive(true);

            currentEnemyCount++;
        }
    }

    public void ReturnBirdToPool(GameObject bird)
    {
        bird.SetActive(false);
        currentEnemyCount--;
    }
}
