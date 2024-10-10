using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<SpawnController>();
    }

    public void GetDamage(float damageAmount)
    {
        if (!isEnemyDead)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                isEnemyDead = true;
                spawnController.ReturnBirdToPool(gameObject);
            }
        }
    }
    public void ResetBird()
    {
        health = 10f;
        isEnemyDead = false;
    }
}
