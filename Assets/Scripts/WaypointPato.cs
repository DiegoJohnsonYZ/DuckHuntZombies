using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPato : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    public List<Transform> waypoints; // Lista de waypoints
    private Transform currentWaypoint;
    private int currentWaypointIndex = -1;

    private void Start()
    {
        // Inicia el movimiento hacia el primer waypoint
        SetNextWaypoint();
    }

    private void Update()
    {
        // Verifica si el jugador existe.
        if (player != null)
        {
            if (currentWaypoint != null)
            {
                // Calcula la dirección hacia el waypoint actual
                Vector3 moveDirection = (currentWaypoint.position - transform.position).normalized;

                // Mueve al pato zombie en la dirección del waypoint a la velocidad especificada
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

                // Si el pato zombie llega al waypoint actual, selecciona el siguiente de manera aleatoria
                if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
                {
                    SetNextWaypoint();
                }
            }
        }
    }

    private void SetNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            // Selecciona aleatoriamente el siguiente waypoint de la lista
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, waypoints.Count);
            } while (randomIndex == currentWaypointIndex);

            currentWaypointIndex = randomIndex;
            currentWaypoint = waypoints[randomIndex];
        }
    }
}