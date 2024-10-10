using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    //적의 체력과 죽었는지 여부 판단
    [SerializeField] float health = 10f;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

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
                spawnController.ReturnBirdToPool(gameObject); //적이 죽었으면 풀로 반환
            }
        }
    }
    
    //적이 다시 풀링되었을 때 상태 초기화
    public void ResetBird()
    {
        health = 10f; //체력 초기화
        isEnemyDead = false; //죽지 않은 상태로 초기화
    }
}
