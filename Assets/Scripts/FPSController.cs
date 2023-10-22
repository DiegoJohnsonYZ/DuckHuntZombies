using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;


public class Weapon 
{
    
    public int bullets;
    public int maxBullets;
    public float reloadTime;
    public bool needsReload;
    public MMFeedbacks weaponFeedback;
    public GameObject weaponPoint;
    public Animator anim;
    public AudioClip reloadSound;
    public Weapon(int _bullets, int _maxBullets,float _reloadTime, MMFeedbacks _weaponFeedback, GameObject _weaponPoint, AudioClip _reloadSound)
    {
        bullets = _bullets;
        maxBullets = _maxBullets;
        reloadTime = _reloadTime;
        needsReload = false;
        weaponFeedback = _weaponFeedback;
        weaponPoint = _weaponPoint;
        reloadSound = _reloadSound;
        anim = weaponPoint.transform.parent.transform.GetComponent<Animator>();
    }

}

public class FPSController : MonoBehaviour
{
    private float health= 100f;
    private int weaponIndex = 0;
    public bool isWeaponLoading = false;
    public bool isWeaponReloading = false;
    public bool isWeaponChanging = false;
    private Weapon shotgun;
    private Weapon rifle;
    private Weapon selectedWeapon;

    public MMProgressBar TargetProgressBar;

    public AudioClip reloadRifle;
    public AudioClip reloadShotgun;

    public MMFeedbacks shotgunFeedback;
    public MMFeedbacks rifleFeedback;



    public GameObject weaponPointShotgun;
    public GameObject weaponPointRifle;
    public PlayerInput playerInput;


    // Start is called before the first frame update
    void Start()
    {
        shotgun = new Weapon(4,4,2f, shotgunFeedback,weaponPointShotgun,reloadShotgun);
        rifle = new Weapon(6,6,3f, rifleFeedback,weaponPointRifle,reloadRifle);
        selectedWeapon = rifle;
        shotgun.anim.SetTrigger("Down");
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Shoot"].WasPressedThisFrame() && !isWeaponLoading && !isWeaponReloading && !isWeaponChanging)
        {
            selectedWeapon.bullets--;
            if (selectedWeapon.bullets >= 0)
            {
                Shoot();
            }else
            {
                print("no anmmo PONER CODIGO DE FEEDBACK");
            }
            
        }
        if (playerInput.actions["ChangeWeapon"].WasPressedThisFrame() && !isWeaponLoading && !isWeaponReloading && !isWeaponChanging)
        {
            isWeaponChanging = true;
            ChangeWeapon();
        }
        if (playerInput.actions["Reload"].WasPressedThisFrame() && !isWeaponLoading && !isWeaponReloading && !isWeaponChanging)
        {
            isWeaponReloading = true;
            ReloadWeapon();
        }
        Vector3 forward = transform.TransformDirection(selectedWeapon.weaponPoint.transform.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

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
        }
    }
    void ChangeWeapon()
    {
        health -= 20;
        TargetProgressBar.UpdateBar(health,0,100);
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
        isWeaponChanging = false;
        isWeaponReloading = false;
    }

    void ReloadWeapon()
    {
        selectedWeapon.bullets = selectedWeapon.maxBullets;
        selectedWeapon.anim.SetTrigger("Down");
        MMSoundManagerSoundPlayEvent.Trigger(selectedWeapon.reloadSound,MMSoundManager.MMSoundManagerTracks.Sfx,this.transform.position);
        Invoke("UpWeapon", selectedWeapon.reloadTime);
    }


    public void FinishLoadWeapon()
    {
        isWeaponLoading = false;
    }
}
