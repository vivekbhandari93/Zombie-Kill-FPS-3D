using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;

    public bool IsDead() { return isDead; }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        BroadcastMessage("OnDamageTaken");
        if(hitPoints <= Mathf.Epsilon)
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("fall");
            Destroy(gameObject, 5f);
        }
    }
}
