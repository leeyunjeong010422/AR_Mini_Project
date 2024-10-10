using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject[] birdPrefabs; //���� ������ �迭�� ����
    [SerializeField] int poolSize = 20; //Ǯ�� ������ ���� ��

    [SerializeField] private List<GameObject> pool; //Ǯ�� ����� ���� ����Ʈ

    private void Start()
    {
        pool = new List<GameObject>();

        //Ǯ�� ������ ũ�⸸ŭ ���� �̸� ����, ��Ȱ��ȭ�ص�
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, birdPrefabs.Length); //�߰��� ���� �迭���� �������� ����
            GameObject bird = Instantiate(birdPrefabs[randomIndex]);
            bird.SetActive(false);
            pool.Add(bird);
        }
    }

    //��Ȱ��ȭ�� ���� ��ȯ
    public GameObject GetPooledObject()
    {
        foreach (GameObject bird in pool)
        {
            //���࿡ ���� ��Ȱ��ȭ�� �Ǿ��ִٸ�
            if (!bird.activeInHierarchy)
            {
                //���� ���¸� �ʱ�ȭ
                bird.GetComponent<EnemyHPController>().ResetBird();
                return bird;
            }
        }

        //Ǯ�� ��Ȱ��ȭ�� ���� ������ ���ο� ���� �����Ͽ� ��ȯ
        int randomIndex = Random.Range(0, birdPrefabs.Length);
        GameObject newBird = Instantiate(birdPrefabs[randomIndex]);
        newBird.SetActive(false);
        pool.Add(newBird);
        return newBird;
    }
}
