﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

    public int LeftAmmo() { return ammoAmount; }

    public void ReduceAmmo() { ammoAmount -= 1; }
}
