using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public FPSController fpsController;
    public void FinishAnimation()
    {
        fpsController.FinishLoadWeapon();
        print("anim finish");
    }
}
