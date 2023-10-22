using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliyngDuckZombieMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    [SerializeField]private bool isOnGround = false;
    private bool isChasingPlayer = false;

    public float flyingSpeed = 5f; // Velocidad a la que se mueve el pato hacia el jugador
    public float diveSpeed = 10f; // Velocidad de ataque en picada

    float elapsedTime = 0f;
    bool diving = false;
    float speed;
    private void Start()
    {
        speed = flyingSpeed;
        // Busca el objeto con la etiqueta "Player" y asigna su transform al jugador.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        //transform.LookAt(player);
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
        

        // Check if it's time to initiate a dive attack
        if (!diving && elapsedTime >= 6f)
        {
            transform.LookAt(player);
            diving = true;
            print("picada");
            GetComponent<Animator>().SetTrigger("Picada");
            speed = diveSpeed;
        }

        

        if (isOnGround && !isChasingPlayer)
        {
            // Comienza a perseguir al jugador cuando está en el suelo.
            GetComponent<Animator>().SetTrigger("Walk");
            transform.LookAt(player);
            isChasingPlayer = true;
            speed = moveSpeed;
        }

        transform.Translate(transform.forward * flyingSpeed * Time.deltaTime, Space.World);


        elapsedTime += Time.deltaTime;
    }

    public void Fall()
    {
        GetComponent<Animator>().SetTrigger("Fall");
        transform.rotation = Quaternion.Euler(90, 0, 0);
        diving = true;
        GetComponent<Animator>().SetTrigger("Picada");
        speed = 30;
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
