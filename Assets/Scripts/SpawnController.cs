using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnController : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] int enemyCount = 20; //적을 몇마리 랜덤 생성 시킬 건지 (최대)
    private int currentEnemyCount; //현재 몇 마리의 적이 활성화되었는지

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        //현재 활성화된 적의 수가 최대 적의 수보다 작을 때마다 랜덤 생성
        while (currentEnemyCount <= enemyCount)
        {
            SpawnBird();
            yield return new WaitForSeconds(1f); //n초마다 생성
        }
    }

    private void SpawnBird()
    {
        GameObject bird = pool.GetPooledObject(); //풀에서 적을 가져와서 생성함
        
        if (bird != null)
        {
            //랜덤으로 생성될 위치와 회전값 설정
            int xPos = Random.Range(-20, 20);
            int zPos = Random.Range(7, 25);
            float randomRotationY = (Random.Range(0, 2) == 0) ? 90f : -90f; //오른쪽을 볼 건지 왼쪽을 볼 건지 (회전값 Y축)

            //위에 설정한 값을 가지고 랜덤으로 위치와 회전을 결정하여 적을 활성화시킴
            bird.transform.position = new Vector3(xPos, Random.Range(-1, 1), zPos);
            bird.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);
            bird.SetActive(true);

            currentEnemyCount++;
        }
    }

    //적이 죽으면 풀로 반환함
    public void ReturnBirdToPool(GameObject bird)
    {
        bird.SetActive(false);
        currentEnemyCount--;
    }
}
