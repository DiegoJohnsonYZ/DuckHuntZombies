using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Rifle")]
    public TMP_Text bulletCounterRifle;
    public MMProgressBar rifleBulletBar;
    public MMFeedbacks rifleBulletBarFeedbacks;
    [Header("Shotgun")]
    public TMP_Text bulletCounterShotgun;
    public MMProgressBar shotgunBulletBar;
    public MMFeedbacks shotgunBulletBarFeedbacks;




    private void Awake()
    {
        instance = this;
    }

    public void UpdateBulletCounter(int weaponType, int bullets, int maxbullets)
    {
        if (weaponType == 0) { 
            bulletCounterRifle.text = bullets.ToString() + "/" + maxbullets.ToString();
            rifleBulletBar.UpdateBar(((float)bullets / (float)maxbullets)*100f, 0f, 100f);
            rifleBulletBarFeedbacks.PlayFeedbacks();
        }
        else if (weaponType == 1)
        {
            bulletCounterShotgun.text = bullets.ToString() + "/" + maxbullets.ToString();
            shotgunBulletBar.UpdateBar(((float)bullets / (float)maxbullets) * 100f, 0f, 100f);
            shotgunBulletBarFeedbacks.PlayFeedbacks();
        }
    }

    public void ReloadWeapon(int weaponType,int maxbullets, float reloadTime)
    {
        if (weaponType == 0)
        {
            rifleBulletBar.UpdateBar(0f, 0f, 100f);
            StartCoroutine(LoadWeaponBar(rifleBulletBar, rifleBulletBarFeedbacks, reloadTime, maxbullets, bulletCounterRifle));
        }
        else if (weaponType == 1)
        {
            shotgunBulletBar.UpdateBar(0f, 0f, 100f);
            StartCoroutine(LoadWeaponBar(shotgunBulletBar, shotgunBulletBarFeedbacks, reloadTime, maxbullets, bulletCounterShotgun));
        }
        
        
        //rifleBulletBarFeedbacks.PlayFeedbacks();
        
    }

    public void ChangeWeapon(int weaponType)
    {
        if (weaponType == 0)
        {
            StartCoroutine(ChangeWeaponAnimation(0.2f,rifleBulletBar.GetComponent<RectTransform>(),shotgunBulletBar.GetComponent<RectTransform>()));
        }
        else if (weaponType == 1)
        {
            StartCoroutine(ChangeWeaponAnimation(0.2f, shotgunBulletBar.GetComponent<RectTransform>(), rifleBulletBar.GetComponent<RectTransform>()));
        }
    }

    private IEnumerator LoadWeaponBar(MMProgressBar weaponBar,MMFeedbacks weaponFeedbacks , float reloadTime, int maxbullets, TMP_Text weaponText)
    {
        weaponText.text = 0 + "/" + maxbullets.ToString();
        float bulletTime = 100 / (float)maxbullets;
        float time = 0f;
        int bullets = 0;
        float fill;
        float bulletTimeCount = bulletTime;

        while (time < reloadTime)
        {
            time += Time.deltaTime;
            fill =  (time / reloadTime) * 100f;
            if (fill > bulletTimeCount)
            {
                bulletTimeCount += bulletTime;
                bullets++;
                weaponText.text = bullets.ToString() + "/" + maxbullets.ToString();
                weaponFeedbacks.PlayFeedbacks();
            }
            weaponBar.UpdateBar(fill,0f,100f);
            yield return null;
        }
        weaponFeedbacks.PlayFeedbacks();
    }

    private IEnumerator ChangeWeaponAnimation(float maxTime, RectTransform oldWeapon, RectTransform newWeapon)
    {
        Vector3 finalPos = newWeapon.anchoredPosition;
        Vector3 initialPos = oldWeapon.anchoredPosition;
        float time = 0f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            float progreso = time / maxTime;
            oldWeapon.anchoredPosition = Vector2.Lerp(initialPos, finalPos, progreso);

            yield return null; // Espera hasta el siguiente frame
        }
        time = 0f;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            float progreso = time / maxTime;
            newWeapon.anchoredPosition = Vector2.Lerp(finalPos, initialPos, progreso);

            yield return null; // Espera hasta el siguiente frame
        }

    }

}
