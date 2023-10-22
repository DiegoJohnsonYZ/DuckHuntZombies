using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckZombieMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    private bool isOnGround = false;
    private bool isChasingPlayer = false;
    public int hitsToKill = 2; // Cambia esto según tus necesidades (2 para dos hits)
    private int currentHits = 0;
    private void Start()
    {
        // Busca el objeto con la etiqueta "Player" y asigna su transform al jugador.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void Update()
    {
        // Verifica si el jugador existe.
        if (player != null)
        {
            if (isOnGround && !isChasingPlayer)
            {
                // Comienza a perseguir al jugador cuando está en el suelo.
                isChasingPlayer = true;
            }

            if (isChasingPlayer)
            {
                // Calcula la dirección hacia el jugador.
                Vector3 moveDirection = (player.position - transform.position).normalized;

                // Mueve al pato zombie en la dirección del jugador a la velocidad especificada.
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }
    
    /*public void ()
    {
        currentHits++;
        if (currentHits >= hitsToKill)
        {
            Die(); // Llama a una función que maneje la muerte del pato.
        }
    }*/

  
}