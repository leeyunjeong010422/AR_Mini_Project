using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    //���� ü�°� �׾����� ���� �Ǵ�
    [SerializeField] float health = 10f;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

    [SerializeField] int enemyType;

    private void Start()
    {
        spawnController = FindObjectOfType<SpawnController>();
    }

    //�������� �Ծ��� �� (�ѿ� �¾��� ��)
    public void GetDamage(float damageAmount)
    {
        //���� �׾����� �ʴ´ٸ�
        if (!isEnemyDead)
        {
            health -= damageAmount; //�������� ���� (-health)

            if (health <= 0)
            {
                isEnemyDead = true;

                AddScoreBasedOnType();

                spawnController.ReturnBirdToPool(gameObject); //���� �׾����� Ǯ�� ��ȯ
            }
        }
    }

    //Ÿ��(����)�� ���� ���� ����
    private void AddScoreBasedOnType()
    {
        int points = 0;

        if (enemyType == 1 || enemyType == 2)
        {
            points = 5;
        }
        else if (enemyType == 3 || enemyType == 4)
        {
            points = 7;
        }
        else if (enemyType == 5 || enemyType == 6)
        {
            points = 10;
        }

        ScoreManager.instance.AddScore(points);
    }

    //���� �ٽ� Ǯ���Ǿ��� �� ���� �ʱ�ȭ
    public void ResetBird()
    {
        health = 10f; //ü�� �ʱ�ȭ
        isEnemyDead = false; //���� ���� ���·� �ʱ�ȭ
    }
}
