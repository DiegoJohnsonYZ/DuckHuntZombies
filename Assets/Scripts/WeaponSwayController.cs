using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwayController : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField] private float swayMultiplier;
    [SerializeField] private float smooth;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GamePaused == false)
        {
            Vector2 movementInput = playerInput.actions["MoveCamera"].ReadValue<Vector2>();

            float mouseX = movementInput.x * swayMultiplier;
            float mouseY = movementInput.y * swayMultiplier;


            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        
        }
    }
}
