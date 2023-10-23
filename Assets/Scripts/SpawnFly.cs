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
    public bool breakTime = false;

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
            float anguloAleatorio = Random.Range(0.0f, 360.0f);
            flyingDuckZombie.transform.rotation = Quaternion.Euler(0, anguloAleatorio, 0);

            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false || !breakTime);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
 
    void DestroyDuck(GameObject duck)
    {
        shouldDestroy = true;
    }  
}