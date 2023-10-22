using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePato : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FPSController>().HitPlayer();
            // Verifica si el pato zombie ha colisionado con el jugador
            Destroy(gameObject); // Destruye el pato zombie
        }
    }
    
    
    public  void Kill()
    {
        // Aquí puedes agregar la lógica para destruir el pato, reproducir una animación de muerte, etc.
        Destroy(gameObject);
    }
}
