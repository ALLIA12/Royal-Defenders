using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] float damage;
    public bool slowsDown = false;
    public float getDamage()
    {
        return this.damage;
    }
    public bool getSlowsDown()
    {
        return this.slowsDown;
    }
    public void IncreaseDmage(float increase)
    {
        damage += Mathf.Abs(increase);
    }
}
