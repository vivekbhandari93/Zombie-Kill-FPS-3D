using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100;

    public void PlayerHurt(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= Mathf.Epsilon)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
