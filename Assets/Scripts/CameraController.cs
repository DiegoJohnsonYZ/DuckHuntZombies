using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField] private GameObject camera;
    [SerializeField] private float speed;

    float pitch;
    float yaw;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = playerInput.actions["MoveCamera"].ReadValue<Vector2>();

        print(movementInput);

        pitch += speed * -movementInput.y;
        yaw += speed * movementInput.x;

        // Clamp pitch:
        pitch = Mathf.Clamp(pitch, -45f, 45f);

        // Wrap yaw:
        while (yaw < 0f)
        {
            yaw += 360f;
        }
        while (yaw >= 360f)
        {
            yaw -= 360f;
        }

        // Set orientation:
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);


        

    }
}
