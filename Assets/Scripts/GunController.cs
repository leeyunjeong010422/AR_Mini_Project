using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f; 

    public AudioSource gunsound; 

    public ParticleSystem muzzleFlash; //�ѱ����� ������ ��ƼŬ
    public GameObject impactEffect; //���� ������ �� ������ ��Ÿ���� ����Ʈ

    public void shoot()
    {
        RaycastHit hit; 
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, range))
        {
            //�� ����ĳ��Ʈ�� ���� �浹�ϸ� EnemyHPController�� �����ͼ�
            EnemyHPController enemy = hit.transform.GetComponent<EnemyHPController>();

            if (enemy != null)  
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity); //�浹�� ������ ����Ʈ ����
                enemy.GetDamage(damage); //�������� ����
            }

        }
        gunsound.Play(); 
        StartCoroutine(OnMuzzleFlashEffect()); 
    }

    //�ѱ��� ��ƼŬ ȿ�� ��� �ڷ�ƾ
    public IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.2f); 
        muzzleFlash.Stop();
    }
}
