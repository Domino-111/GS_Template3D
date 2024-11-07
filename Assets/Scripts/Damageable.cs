using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Damageable : MonoBehaviour
{
    [SerializeField] private float healthMax;

    [SerializeField] private UnityEvent onHealthZero;

    private float healthCurrent;

    private bool isDead;

    private void Start()
    {
        healthCurrent = healthMax;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
        {
            return; // If we're dead, stop early
        }

        Debug.Log($"The agent {name} took {amount} damage!");

        healthCurrent -= amount;

        if (healthCurrent <= 0)
        {
            HealthZero();
        }
    }

    public void RestoreHealth(float amount)
    {
        isDead = false;
        healthCurrent += amount;

        if (healthCurrent > healthMax)
        {
            healthCurrent = healthMax;
        }
    }

    private void HealthZero()
    {
        isDead = true;
        healthCurrent = 0;

        onHealthZero.Invoke();

        Debug.Log($"The agent {name} has perished!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckForDamage(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckForDamage(other.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        CheckForDamage(other);
    }

    private void CheckForDamage(GameObject possibleSource)
    {
        // TryGetComponent will return true or false. If true it will also "out" the component it found
        if (possibleSource.TryGetComponent<DamageSource>(out DamageSource damageSource))
        {
            // If the tags match...
            if (possibleSource.CompareTag(tag))
            {
                // do nothing
                return;
            }

            // If we get here, tags don't match so we should take damage
            TakeDamage(damageSource.GetDamage());
        }
    }

    /// <summary>
    /// Returns the percent of health this Damageable has between 0 - 1
    /// </summary>
    /// <returns></returns>
    public float GetHealthPercent()
    {
        return healthCurrent / healthMax;
    }
}
