using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFly : MonoBehaviour
{
    public List<GameObject> duckZombiePrefabs; 
    public Transform player;
    public float spawnInterval = 5f; 
    public float spawnRadius = 20f; 
    public float spawnAltitude = 10f;
    public float flyingSpeed = 5f; // Velocidad a la que se mueve el pato hacia el jugador
    public float diveSpeed = 10f; // Velocidad de ataque en picada
    private bool shouldDestroy = false; // Variable para indicar que se debe destruir el pato

    private void Start()
    {
        StartCoroutine(SpawnFlyingDuckZombies());
    }

    private IEnumerator SpawnFlyingDuckZombies()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false);

            GameObject duckZombiePrefab = duckZombiePrefabs[Random.Range(0, duckZombiePrefabs.Count)];
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(player.position.x + randomCircle.x, player.position.y + spawnAltitude, player.position.z + randomCircle.y);
            GameObject flyingDuckZombie = Instantiate(duckZombiePrefab, spawnPosition, Quaternion.identity);
            
            StartCoroutine(MoveTowardsPlayer(flyingDuckZombie));

            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveTowardsPlayer(GameObject duck)
    {
        float elapsedTime = 0f;
        bool diving = false;
        float speed = flyingSpeed;


        while (true) // El pato volará durante 6 segundos
        {
            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false);

            if (duck == null)
            {
                yield break; // Salir de la rutina si el pato ha sido destruido
            }
            
            // Check if it's time to initiate a dive attack
            if (!diving && elapsedTime >= 6f)
            {
                duck.transform.LookAt(player);
                diving = true;
                print("picada");
                duck.GetComponent<Animator>().SetTrigger("Picada");
                speed = diveSpeed;
            }

            duck.transform.Translate(duck.transform.forward * flyingSpeed * Time.deltaTime, Space.World);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    void DestroyDuck(GameObject duck)
    {
        shouldDestroy = true;
    }

    
}