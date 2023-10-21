using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    private bool isWeaponLoading = false;

    public GameObject weaponPoint;
    public PlayerInput playerInput;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Shoot"].WasPressedThisFrame() && !isWeaponLoading )
        {
            Shoot();
        }

        Vector3 forward = transform.TransformDirection(weaponPoint.transform.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

    }

    void Shoot()
    {
        isWeaponLoading = true;
        //anim.SetTrigger("Shoot");
        print("shoot");
        RaycastHit hit;
        if (Physics.Raycast(weaponPoint.transform.position, weaponPoint.transform.forward, out hit))
        {
            print(hit.transform);
        }
    }

    public void FinishLoadWeapon()
    {
        isWeaponLoading = false;
    }
}
