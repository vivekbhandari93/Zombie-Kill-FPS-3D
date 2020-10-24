using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;

    [SerializeField] ParticleSystem muzzleVFX;
    [SerializeField] GameObject hitVFX;

    [SerializeField] Ammo ammoSlot;

    [SerializeField] float timeBetweenShots = 0f;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
            
        }
    }

    IEnumerator  Shoot()
    {
        canShoot = false;

        if (ammoSlot.LeftAmmo() > 0)
        {
            ProcessRaycast();
            PlayMuzzleVFX();
            ammoSlot.ReduceAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            PlayHitVFX(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; }
            target.TakeDamage(damage);
        }
    }

    private void PlayMuzzleVFX()
    {
        muzzleVFX.Play();
    }

    private void PlayHitVFX(RaycastHit hit)
    {
        var hitInstance = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitInstance, 0.1f);
    }
}
