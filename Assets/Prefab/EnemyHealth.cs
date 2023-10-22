using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

 public   void Die()
    {
        // Realiza las acciones necesarias cuando el enemigo muere, como reproducir una animación, efectos de sonido, etc.
        // En este ejemplo, simplemente se destruye el objeto.
        Destroy(gameObject);
    }
 
}
