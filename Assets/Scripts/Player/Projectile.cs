using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectilelife = 3.0f;

    public ParticleSystem m_ExplosionParticles;

    void Start()
    {
        Destroy(gameObject, projectilelife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        Destroy(gameObject);
    }
}
