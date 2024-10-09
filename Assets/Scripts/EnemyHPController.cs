using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] bool isEneymyDead;

    public void GetDamage(float damageAmount)
    {
        if (!isEneymyDead)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
