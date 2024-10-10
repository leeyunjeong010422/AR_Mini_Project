using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    //적의 체력과 죽었는지 여부 판단
    [SerializeField] float health = 10f;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

    [SerializeField] int enemyType;

    private void Start()
    {
        spawnController = FindObjectOfType<SpawnController>();
    }

    //데미지를 입었을 때 (총에 맞았을 때)
    public void GetDamage(float damageAmount)
    {
        //적이 죽어있지 않는다면
        if (!isEnemyDead)
        {
            health -= damageAmount; //데미지를 입음 (-health)

            if (health <= 0)
            {
                isEnemyDead = true;

                AddScoreBasedOnType();

                spawnController.ReturnBirdToPool(gameObject); //적이 죽었으면 풀로 반환
            }
        }
    }

    //타입(유형)에 따라 점수 증가
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

    //적이 다시 풀링되었을 때 상태 초기화
    public void ResetBird()
    {
        health = 10f; //체력 초기화
        isEnemyDead = false; //죽지 않은 상태로 초기화
    }
}
