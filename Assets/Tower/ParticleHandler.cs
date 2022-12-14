using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float slowDownModifier;
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
    public float getSlowDownModifier()
    {
        return this.slowDownModifier;
    }
    public void SetSlowDownModifier(float newValue)
    {
        slowDownModifier = newValue;
    }
    public void IncreaseSlowDownModifier(float increase)
    {
        slowDownModifier += Mathf.Abs(increase);
    }
}
