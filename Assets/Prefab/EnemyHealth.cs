using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 7; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public ParticleSystem blood;
    public int duckType;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Vector3 dir = ((transform.position - FPSController.instance.transform.position).normalized);
        if(duckType == 1)
        {
            FliyngDuckZombieMovement fDuck = GetComponent<FliyngDuckZombieMovement>();
            
            fDuck.Fall();
        }
        
        print("DIRECCION" + dir);
        if (duckType == 0)
        {
            GetComponent<Rigidbody>().AddForce(dir * 15, ForceMode.Impulse);
            GetComponent<Animator>().SetTrigger("Hurt");
        }
            
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

        print("hit");
        ParticleSystem particleSystem = Instantiate(blood, this.transform.position, new Quaternion(0, 0, 0, 0));
        float timeDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constantMax;
        print(timeDuration);
        Destroy(particleSystem.gameObject, timeDuration);
    }

    public void Die()
    {
        // Realiza las acciones necesarias cuando el enemigo muere, como reproducir una animación, efectos de sonido, etc.
        // En este ejemplo, simplemente se destruye el objeto.
        Destroy(gameObject);
    }
 
}
