using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;

    [SerializeField] int maxAmmo = 10; //최대 총알 수

    private int currentAmmo;
    private bool isReloading = false;

    public AudioSource gunsound; 

    public ParticleSystem muzzleFlash; //총구에서 나오는 파티클
    public GameObject impactEffect; //적을 맞췄을 때 적에게 나타나는 이펙트

    private void Start()
    {
        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    public void shoot()
    {
        //장전 중이거나 총알이 없으면 발사하지 않음
        if (isReloading || currentAmmo <= 0)
        {
            return;
        }

        RaycastHit hit; 
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, range))
        {
            //쏜 레이캐스트가 적과 충돌하면 EnemyHPController를 가져와서
            EnemyHPController enemy = hit.transform.GetComponent<EnemyHPController>();

            if (enemy != null)  
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity); //충돌한 지점에 이펙트 생성
                enemy.GetDamage(damage); //데미지를 입힘
            }

        }
        gunsound.Play(); 
        StartCoroutine(OnMuzzleFlashEffect());

        currentAmmo--;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
    }

    //총구에 파티클 효과 재생 코루틴
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
        yield return new WaitForSeconds(2f); //장전 시간 2초
        currentAmmo = maxAmmo;
        UIManager.instance.UpdateAmmoText(currentAmmo, maxAmmo);
        isReloading = false;
    }
}
