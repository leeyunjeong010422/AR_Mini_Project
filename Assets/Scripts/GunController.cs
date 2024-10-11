using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;

    [SerializeField] int maxAmmo = 10; //�ִ� �Ѿ� ��

    private int currentAmmo;
    private bool isReloading = false;

    public AudioSource gunsound; 

    public ParticleSystem muzzleFlash; //�ѱ����� ������ ��ƼŬ
    public GameObject impactEffect; //���� ������ �� ������ ��Ÿ���� ����Ʈ

    private void Start()
    {
        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    public void shoot()
    {
        //���� ���̰ų� �Ѿ��� ������ �߻����� ����
        if (isReloading || currentAmmo <= 0)
        {
            return;
        }

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

        currentAmmo--;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    //�ѱ��� ��ƼŬ ȿ�� ��� �ڷ�ƾ
    public IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.2f); 
        muzzleFlash.Stop();
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(2f); //���� �ð� 2��
        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
        isReloading = false;
    }
}
