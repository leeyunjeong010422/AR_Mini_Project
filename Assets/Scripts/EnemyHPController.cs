using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    private float health;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<SpawnController>();
        ResetEnemy();
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

    public void ResetEnemy()
    {
        health = maxHealth;
        isEnemyDead = false;
    }
}
