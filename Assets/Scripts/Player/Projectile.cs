using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectilelife = 3.0f;
    private int DamageAmount = 1;

    public ParticleSystem m_ExplosionParticles;

    void Start()
    {
        Destroy(gameObject, projectilelife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TargetHealth targetHit = collision.gameObject.GetComponent<TargetHealth>();
        
        if (targetHit != null)
        {
            targetHit.Damage(DamageAmount);

            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        }
        Destroy(gameObject);
    }
}
