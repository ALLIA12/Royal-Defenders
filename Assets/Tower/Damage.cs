using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]  float damage;
    public float getDamage() {
        return this.damage;
    }
}
