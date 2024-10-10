using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject[] birdPrefabs; //적의 종류를 배열에 담음
    [SerializeField] int poolSize = 20; //풀에 저장할 적의 수

    [SerializeField] private List<GameObject> pool; //풀에 저장된 적의 리스트

    private void Start()
    {
        pool = new List<GameObject>();

        //풀에 지정된 크기만큼 적을 미리 생성, 비활성화해둠
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, birdPrefabs.Length); //추가한 종류 배열에서 랜덤으로 선택
            GameObject bird = Instantiate(birdPrefabs[randomIndex]);
            bird.SetActive(false);
            pool.Add(bird);
        }
    }

    //비활성화된 적을 반환
    public GameObject GetPooledObject()
    {
        foreach (GameObject bird in pool)
        {
            //만약에 적이 비활성화가 되어있다면
            if (!bird.activeInHierarchy)
            {
                //적의 상태를 초기화
                bird.GetComponent<EnemyHPController>().ResetBird();
                return bird;
            }
        }

        //풀에 비활성화된 적이 없으면 새로운 적을 생성하여 반환
        int randomIndex = Random.Range(0, birdPrefabs.Length);
        GameObject newBird = Instantiate(birdPrefabs[randomIndex]);
        newBird.SetActive(false);
        pool.Add(newBird);
        return newBird;
    }
}
