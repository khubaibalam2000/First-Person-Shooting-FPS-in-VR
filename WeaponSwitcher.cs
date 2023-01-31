using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nokobot.Assets.Crossbow;

public class WeaponSwitcher : MonoBehaviour
{
    private Knife knife;
    private Pistol pistol;
    private CrossbowShoot crossbow;
    private Rifle rifle;
    
    // Start is called before the first frame update
    void Awake()
    {
        knife = GetComponent<Knife>();
        pistol = GetComponent<Pistol>();
        crossbow = GameObject.Find("crossShots").GetComponent<CrossbowShoot>();
        rifle = GetComponent<Rifle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pistol.pistolActive = false;
            crossbow.crossbowActive = false;
            rifle.rifleActive = false;
            pistol.pistol.SetActive(false);
            crossbow.crossbow.SetActive(false);
            rifle.rifle.SetActive(false);
            pistol.crosshairP.SetActive(false);
            crossbow.crosshairC.SetActive(false);
            rifle.crosshairR.SetActive(false);
            pistol.pistolImage.SetActive(false);
            crossbow.crossImage.SetActive(false);
            rifle.rifleImage.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            knife.knifeActive = false;
            crossbow.crossbowActive = false;
            rifle.rifleActive = false;
            knife.knife.SetActive(false);
            crossbow.crossbow.SetActive(false);
            rifle.rifle.SetActive(false);
            crossbow.crosshairC.SetActive(false);
            rifle.crosshairR.SetActive(false);
            knife.knifeImage.SetActive(false);
            crossbow.crossImage.SetActive(false);
            rifle.rifleImage.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            knife.knifeActive = false;
            pistol.pistolActive = false;
            rifle.rifleActive = false;
            knife.knife.SetActive(false);
            pistol.pistol.SetActive(false);
            rifle.rifle.SetActive(false);
            pistol.crosshairP.SetActive(false);
            rifle.crosshairR.SetActive(false);
            knife.knifeImage.SetActive(false);
            pistol.pistolImage.SetActive(false);
            rifle.rifleImage.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            knife.knifeActive = false;
            pistol.pistolActive = false;
            crossbow.crossbowActive = false;
            knife.knife.SetActive(false);
            pistol.pistol.SetActive(false);
            crossbow.crossbow.SetActive(false);
            pistol.crosshairP.SetActive(false);
            crossbow.crosshairC.SetActive(false);
            knife.knifeImage.SetActive(false);
            pistol.pistolImage.SetActive(false);
            crossbow.crossImage.SetActive(false);
        }
    }
}