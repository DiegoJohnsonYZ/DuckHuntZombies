using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;


public class Weapon 
{
    
    public int bullets;
    public bool needsReload;
    public MMFeedbacks weaponFeedback;
    public GameObject weaponPoint;
    public Animator anim;
    public Weapon(int _bullets, MMFeedbacks _weaponFeedback, GameObject _weaponPoint)
    {
        bullets = _bullets;
        needsReload = false;
        weaponFeedback = _weaponFeedback;
        weaponPoint = _weaponPoint;
        anim = weaponPoint.transform.parent.transform.GetComponent<Animator>();
    }

}

public class FPSController : MonoBehaviour
{
    private int weaponIndex = 0;
    public bool isWeaponLoading = false;
    private Weapon shotgun;
    private Weapon rifle;
    private Weapon selectedWeapon;


    public MMFeedbacks shotgunFeedback;
    public MMFeedbacks rifleFeedback;



    public GameObject weaponPointShotgun;
    public GameObject weaponPointRifle;
    public PlayerInput playerInput;
    
    private int rifleShots = 0;
    public MuertePato muertePato;
    public EnemyHealth enemyHealth;
    public GameObject weaponPointer;


    // Start is called before the first frame update
    void Start()
    {
        shotgun = new Weapon(4, shotgunFeedback,weaponPointShotgun);
        rifle = new Weapon(6, rifleFeedback,weaponPointRifle);
        selectedWeapon = rifle;
        shotgun.anim.SetTrigger("Down");
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Shoot"].WasPressedThisFrame() && !isWeaponLoading )
        {
            Shoot();
        }
        if (playerInput.actions["ChangeWeapon"].WasPressedThisFrame() && !isWeaponLoading)
        {
            ChangeWeapon();
        }

        Vector3 forward = transform.TransformDirection(selectedWeapon.weaponPoint.transform.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        
        // Obtén la dirección hacia donde apunta el arma seleccionada
        Vector3 weaponDirection = selectedWeapon.weaponPoint.transform.forward;

        // Calcula la posición en la pantalla donde debe estar el puntero
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(selectedWeapon.weaponPoint.transform.position + weaponDirection * 10f);

        // Actualiza la posición del puntero en el Canvas
        weaponPointer.transform.position = screenPosition;

    }

        void Shoot()
        {
            isWeaponLoading = true;
            //anim.SetTrigger("Shoot");
            selectedWeapon.weaponFeedback.PlayFeedbacks();
            print("shoot");
            RaycastHit hit;

            if (Physics.Raycast(selectedWeapon.weaponPoint.transform.position, selectedWeapon.weaponPoint.transform.forward, out hit))
            {
                print(hit.transform);
                // Verifica si el objeto impactado tiene la etiqueta "enemy"
                if (hit.transform.CompareTag("Enemy"))
                {
                    // Dependiendo del arma seleccionada, determina el número de disparos necesarios
                    if (selectedWeapon == rifle)
                    {
                        // Si el rifle está seleccionado, un solo disparo elimina al enemigo
                        EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(2); // Reduce la salud del enemigo en 1
                        }
                    }
                    else
                    {
                        // Si otro arma está seleccionada, necesitas dos disparos para eliminar al enemigo
                        EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(1); // Reduce la salud del enemigo en 1
                        }
                    }
                }
            }
            
        }
    void ChangeWeapon()
    {
        selectedWeapon.anim.SetTrigger("Down");
        if (weaponIndex == 0)
        {
            selectedWeapon = shotgun;
            weaponIndex = 1;
        }
        else
        {
            selectedWeapon = rifle;
            weaponIndex = 0;
        }
        Invoke("UpWeapon", 0.2f);
    }

    void UpWeapon()
    {
        selectedWeapon.anim.SetTrigger("Up");
    }


    public void FinishLoadWeapon()
    {
        isWeaponLoading = false;
    }
    
   
}
