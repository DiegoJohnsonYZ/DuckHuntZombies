using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;


public class Radar : MonoBehaviour
{
   [SerializeField] private Transform pfRadarPing;
    [SerializeField] private LayerMask radarLayerMask;

    private Transform sweepTransform;
    private float rotationSpeed;
    private float radarDistance;
    private List<Collider2D> colliderList;

    private void Awake()
    {
        sweepTransform = transform.Find("Sweep");
        rotationSpeed = 180f;
        radarDistance = 150f;
        colliderList = new List<Collider2D>();
    }

    private void Update()
    {
        float previousRotation = (sweepTransform.eulerAngles.z % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        float currentRotation = (sweepTransform.eulerAngles.z % 360) - 180;

        if (previousRotation < 0 && currentRotation >= 0)
        {
            // Medio giro
            colliderList.Clear();
        }

        RaycastHit2D[] raycastHit2DArray = Physics2D.RaycastAll(transform.position, UtilsClass.GetVectorFromAngle(sweepTransform.eulerAngles.z), radarDistance, radarLayerMask);
        foreach (RaycastHit2D raycastHit2D in raycastHit2DArray)
        {
            if (raycastHit2D.collider != null)
            {
                // Objeto detectado
                if (!colliderList.Contains(raycastHit2D.collider))
                {
                    // Detectado por primera vez
                    colliderList.Add(raycastHit2D.collider);
                    RadarPing radarPing = Instantiate(pfRadarPing, raycastHit2D.point, Quaternion.identity).GetComponent<RadarPing>();

                    if (raycastHit2D.collider.gameObject.CompareTag("Enemy"))
                    {
                        // Detectado un enemigo
                        radarPing.SetColor(new Color(1, 0, 0)); // Rojo para los enemigos
                    }
                    radarPing.SetDisappearTimer(360f / rotationSpeed * 1f);
                }
            }
        }
    }
}





