using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 20f;
    public bool isEneymyDead;

    public void GetDamage(float damageAmount)
    {
        if (!isEneymyDead)
        {
            health -= damageAmount;

            if (health <= 0)
            {
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
