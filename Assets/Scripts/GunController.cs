using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Camera arCamera;     
    public float range = 100f; 
    public float damage = 10f; 

    public AudioSource gunsound; 

    public ParticleSystem muzzleFlash; 
    public GameObject impactEffect;

    public void shoot()
    {
        RaycastHit hit; 
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, range))
        { 

            EnemyController enemy = hit.transform.GetComponent<EnemyController>();

            if (enemy != null)  
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity); 
                enemy.GetDamage(damage);
            }

        }
        gunsound.Play(); 
        StartCoroutine(OnMuzzleFlashEffect()); 
    }

    public IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.2f); 
        muzzleFlash.Stop();
    }
}
