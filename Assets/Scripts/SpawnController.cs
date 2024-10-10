using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnController : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] int enemyCount = 20; //���� ��� ���� ���� ��ų ���� (�ִ�)
    private int currentEnemyCount; //���� �� ������ ���� Ȱ��ȭ�Ǿ�����

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        //���� Ȱ��ȭ�� ���� ���� �ִ� ���� ������ ���� ������ ���� ����
        while (currentEnemyCount <= enemyCount)
        {
            SpawnBird();
            yield return new WaitForSeconds(1f); //n�ʸ��� ����
        }
    }

    private void SpawnBird()
    {
        GameObject bird = pool.GetPooledObject(); //Ǯ���� ���� �����ͼ� ������
        
        if (bird != null)
        {
            //�������� ������ ��ġ�� ȸ���� ����
            int xPos = Random.Range(-20, 20);
            int zPos = Random.Range(7, 25);
            float randomRotationY = (Random.Range(0, 2) == 0) ? 90f : -90f; //�������� �� ���� ������ �� ���� (ȸ���� Y��)

            //���� ������ ���� ������ �������� ��ġ�� ȸ���� �����Ͽ� ���� Ȱ��ȭ��Ŵ
            bird.transform.position = new Vector3(xPos, Random.Range(-1, 1), zPos);
            bird.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);
            bird.SetActive(true);

            currentEnemyCount++;
        }
    }

    //���� ������ Ǯ�� ��ȯ��
    public void ReturnBirdToPool(GameObject bird)
    {
        bird.SetActive(false);
        currentEnemyCount--;
    }
}
