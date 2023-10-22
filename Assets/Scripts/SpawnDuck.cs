using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDuck : MonoBehaviour
{
    public List<GameObject> duckZombiePrefabs; // Lista de prefabs de patos zombie
   
    //public GameObject duckZombiePrefab; // Prefab del pato zombie
    public Transform player; 
    public float spawnInterval = 2f; // Intervalo entre cada aparición
    public float spawnAltitudeRange = 5f; // Rango de altitudes
    //public float spawnForce = 10f; // Fuerza de impulso
    public float minSpawnForce = 5f; // Fuerza de impulso mínima
    public float maxSpawnForce = 15f; // Fuerza de impulso máxima

    private void Start()
    {
        // Inicia la generación de patos zombies en intervalos regulares
        StartCoroutine(SpawnDuckZombies());
    }
   
    private IEnumerator SpawnDuckZombies()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false);

            // Calcula múltiples posiciones en el borde del plano
            List<Vector3> spawnPositions = new List<Vector3>();
            for (float angle = 0; angle < 360; angle += 10) // Ajusta el ángulo según la cantidad de posiciones deseadas
            {
                Vector3 spawnDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                float planeHeight = 0.0f; // Ajusta la altura del plano
                Vector3 spawnPosition = new Vector3(player.position.x, planeHeight, player.position.z) + spawnDirection.normalized * 10f;
                spawnPositions.Add(spawnPosition);
            }

            // Selecciona aleatoriamente una posición en el borde del plano
            Vector3 selectedSpawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];

            // Crea una instancia del pato zombie en la posición seleccionada
            GameObject duckZombiePrefab = duckZombiePrefabs[Random.Range(0, duckZombiePrefabs.Count)];
            GameObject duckZombie = Instantiate(duckZombiePrefab, selectedSpawnPosition, Quaternion.identity);


            // Ajusta la altura aleatoriamente
            float randomAltitude = Random.Range(1f, spawnAltitudeRange);
            Vector3 duckPosition = duckZombie.transform.position;
            duckPosition.y += randomAltitude;
            duckZombie.transform.position = duckPosition;

          
            // Calcula la dirección hacia el jugador
            Vector3 lookDirection = (player.position - duckZombie.transform.position).normalized;
            //  duckZombie.transform.rotation = Quaternion.LookRotation(lookDirection);

            // Calcula una fuerza aleatoria hacia arriba para este pato en particular
            float randomForce = Random.Range(minSpawnForce, maxSpawnForce);

            // Aplica la fuerza hacia arriba al pato zombie
            Rigidbody duckRigidbody = duckZombie.GetComponent<Rigidbody>();
            duckRigidbody.AddForce(Vector3.up * randomForce, ForceMode.Impulse);

            yield return new WaitUntil(() => GameManager.Instance.GamePaused == false);
            yield return new WaitForSeconds(spawnInterval);            
        }
    }
}