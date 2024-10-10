using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    //���� ü�°� �׾����� ���� �Ǵ�
    [SerializeField] float health = 10f;
    [SerializeField] bool isEnemyDead = false;

    private SpawnController spawnController;

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
                spawnController.ReturnBirdToPool(gameObject); //���� �׾����� Ǯ�� ��ȯ
            }
        }
    }
    
    //���� �ٽ� Ǯ���Ǿ��� �� ���� �ʱ�ȭ
    public void ResetBird()
    {
        health = 10f; //ü�� �ʱ�ȭ
        isEnemyDead = false; //���� ���� ���·� �ʱ�ȭ
    }
}
