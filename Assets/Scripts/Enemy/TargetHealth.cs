using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public int points = 1;

    private GameManager gameManager;

    public GameManager GameManager { get { return gameManager; } set { gameManager = value; } }
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void DisableTarget()
    {
        if(gameManager != null) 
        {
            gameManager.AddScore(points);
        }
        gameObject.SetActive(false);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0) 
        {
        DisableTarget();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
