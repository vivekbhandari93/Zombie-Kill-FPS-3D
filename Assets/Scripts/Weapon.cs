using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;

    [SerializeField] ParticleSystem muzzleVFX;
    [SerializeField] GameObject hitVFX;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Shoot();
            PlayMuzzleVFX();
            
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            PlayHitVFX(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null) { return; }
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
