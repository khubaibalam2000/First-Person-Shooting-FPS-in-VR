using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : WeaponsAbstract
{
    private Animator animPistol;
    public GameObject pistol;
    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEffect;
    public Text magPUI;
    public Text pistolBulletsUI;
    public GameObject pistolImage;
    public AudioClip pisShots;
    public GameObject crosshairP;
    public AudioClip pisReload;
    private Inventory invent;
    AudioSource pisShots1;
    AudioSource pisReload1;
    private PauseMenu pm;
    private Camera fpsCam;
    public bool pistolActive;
    private int magP;
    private int pistolBullets;
    private int pistolDamage;

    public int GetMagP()
    {
        return magP;
    }

    public int GetPistolBullets()
    {
        return pistolBullets;
    }

    // Start is called before the first frame update
    void Start()
    {
        reloading = false;
        count1 = 0;
        count2 = 12;
        count3 = 0;
        pistolDamage = 20;
        range = 800f;
        volume = 15f;
        magP = 12;
        pistolBullets = 180;
        pistolActive = false;
        crosshairP.SetActive(false);
        pm = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        pisShots1 = GameObject.Find("pisShot").GetComponent<AudioSource>();
        pisReload1 = GameObject.Find("pisReload").GetComponent<AudioSource>();
        fpsCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        invent = GetComponent<Inventory>();
        animPistol = pistol.GetComponent<Animator>();
        pistol.SetActive(false);
        pistolImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        magPUI.text = magP.ToString();
        pistolBulletsUI.text = pistolBullets.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha2) && invent.enable[1])
        {
            pistolActive = true;
            pistol.SetActive(true);
            pistolImage.SetActive(true);
            crosshairP.SetActive(true);
            //ammoP.SetActive(true);
            count1++;
        }
        if (count1 % 2 == 0)
        {
            pistolActive = false;
            pistol.SetActive(false);
            pistolImage.SetActive(false);
            crosshairP.SetActive(false);
        }

        if (pistolActive && !pm.gameIsPause)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                animPistol.SetFloat("Walk_Mag", 1f);
            }
            else
            {
                animPistol.SetFloat("Walk_Mag", 0f);
            }

            if (Input.GetKey(KeyCode.LeftShift) && ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))))
            {
                animPistol.SetBool("Run", true);
            }
            
            else
            {
                animPistol.SetBool("Run", false);
            }
            if (!reloading)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && count3 <= 1)
                {
                    count2--;
                    Shoot();
                    StartCoroutine(Gun());
                }
            }
            IEnumerator Gun()
            {
                
                yield return new WaitForSeconds(0.1f);
                animPistol.SetBool("Fire", false);
            }
            if ((magP <= 0 || Input.GetKeyDown(KeyCode.R)) && magP < 12 && count3 <= 1)
            {
                Reloader();
                StartCoroutine(Reload());
            }
            IEnumerator Reload()
            {
                yield return new WaitForSeconds(1f);
                animPistol.Play("Idle");
                reloading = false;
            }
            if (count3 > 1)
            {
                magP = 0;
            }
        }
    }

    public override void Shoot()
    {
        magP--;
        muzzleFlash.Play();
        pisShots1.PlayOneShot(pisShots, volume);
        cartridgeEffect.Play();
        animPistol.SetBool("Fire", true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fpsCam.transform.forward, out hit, range))
        {
            ZombieAI1 target = hit.transform.GetComponent<ZombieAI1>();
            if (target != null)
            {
                target.Damage(pistolDamage);
            }
        }
    }
    public override void Reloader()
    {
        reloading = true;
        animPistol.Play("Reload");
        magP = 12;
        pisReload1.PlayOneShot(pisReload, volume);
        pistolBullets = pistolBullets - (12 - count2);
        if (pistolBullets < 0)
        {
            magP += pistolBullets;
            pistolBullets = 0;
            count3++;
        }
        count2 = 12;
    }

    public void SaveGame()
    {
        PistolData save = new PistolData();
        save.pistolActiveFile = pistolActive;
        save.magPFile = GetMagP();
        save.pistolBulletsFile = GetPistolBullets();
        save.pistolCount1 = count1;
        save.pistolCount2 = count2;
        save.pistolCount3 = count3;
        SavingSystem<PistolData>.Save(Application.persistentDataPath + "/file6.pqr", save);
    }

    public void LoadGame()
    {
        PistolData load = SavingSystem<PistolData>.Load(Application.persistentDataPath + "/file6.pqr");
        pistolActive = load.pistolActiveFile;
        magP = load.magPFile;
        pistolBullets = load.pistolBulletsFile;
        count1 = load.pistolCount1;
        count2 = load.pistolCount2;
        count3 = load.pistolCount3;
    }
}

[System.Serializable]
public class PistolData
{
    public bool pistolActiveFile;
    public int magPFile;
    public int pistolBulletsFile;
    public int pistolCount1;
    public int pistolCount2;
    public int pistolCount3;
}