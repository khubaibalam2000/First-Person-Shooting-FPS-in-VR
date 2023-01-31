using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : WeaponsAbstract
{
    private Animator animRifle;
    public GameObject rifle;
    public ParticleSystem muzzleFlash3;
    public ParticleSystem cartridgeEffect3;
    public Text magRUI;
    public Text rifleBulletsUI;
    public GameObject rifleImage;
    private Inventory invent;
    public AudioClip rifShots;
    public AudioClip rifReload;
    public GameObject crosshairR;
    AudioSource rifShots1;
    AudioSource rifReload1;
    private PauseMenu pm;
    private Camera fpsCam;
    public bool rifleActive;
    private int magR;
    private int rifleBullets;
    private int rifleDamage;
    private float fireRate;
    private float nextTimeToFire;

    public int GetMagR()
    {
        return magR;
    }

    public int GetRifleBullets()
    {
        return rifleBullets;
    }

    // Start is called before the first frame update
    void Start()
    {
        reloading = false;
        count1 = 0;
        count2 = 30;
        count3 = 0;
        rifleDamage = 40;
        nextTimeToFire = 0f;
        fireRate = 8f;
        range = 800f;
        volume = 15f;
        magR = 30;
        rifleBullets = 360;
        crosshairR.SetActive(false);
        rifleActive = false;
        pm = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        rifShots1 = GameObject.Find("rifleShots").GetComponent<AudioSource>();
        rifReload1 = GameObject.Find("rifleReload").GetComponent<AudioSource>();
        fpsCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        invent = GetComponent<Inventory>();
        animRifle = rifle.GetComponent<Animator>();
        rifle.SetActive(false);
        rifleImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        magRUI.text = magR.ToString();
        rifleBulletsUI.text = rifleBullets.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha4) && invent.enable[3])
        {
            rifleActive = true;
            rifle.SetActive(true);
            rifleImage.SetActive(true);
            crosshairR.SetActive(true);
            count1++;
        }
        if (count1 % 2 == 0)
        {
            rifleActive = false;
            rifle.SetActive(false);
            rifleImage.SetActive(false);
            crosshairR.SetActive(false);
        }

        if (rifleActive && !pm.gameIsPause)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                animRifle.SetFloat("Walk_Mag", 1f);
            }
            else
            {
                animRifle.SetFloat("Walk_Mag", 0f);
            }

            if (Input.GetKey(KeyCode.LeftShift) && ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))))
            {
                animRifle.SetBool("run", true);
            }

            else
            {
                animRifle.SetBool("run", false);
            }
            if (!reloading)
            {
                if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && count3 <= 1)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    count2--;
                    Shoot();
                    StartCoroutine(Gun());
                }
            }
            IEnumerator Gun()
            {

                yield return new WaitForSeconds(0.1f);
                animRifle.SetBool("Fire", false);
            }
            if ((magR <= 0 || Input.GetKeyDown(KeyCode.R)) && magR < 30 && count3 <= 1)
            {
                Reloader();
                StartCoroutine(Reload());
            }
            IEnumerator Reload()
            {
                yield return new WaitForSeconds(1f);
                animRifle.Play("Idle");
                reloading = false;
            }
            if(count3 > 1)
            {
                magR = 0;
            }
        }
    }

    public override void Shoot()
    {
        magR--;
        muzzleFlash3.Play();
        rifShots1.PlayOneShot(rifShots, volume);
        cartridgeEffect3.Play();
        animRifle.SetBool("Fire", true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fpsCam.transform.forward, out hit, range))
        {
            ZombieAI1 target = hit.transform.GetComponent<ZombieAI1>();
            if (target != null)
            {
                target.Damage(rifleDamage);
            }
        }
    }
    public override void Reloader()
    {
        reloading = true;
        animRifle.Play("Reload");
        magR = 30;
        rifReload1.PlayOneShot(rifReload, volume);
        rifleBullets = rifleBullets - (30 - count2);
        if (rifleBullets < 0)
        {
            magR += rifleBullets;
            rifleBullets = 0;
            count3++;
        }
        count2 = 30;
    }

    public void SaveGame()
    {
        RifleData save = new RifleData();
        save.rifleActiveFile = rifleActive;
        save.magRFile = GetMagR();
        save.rifleBulletsFile = GetRifleBullets();
        save.rifleCount1 = count1;
        save.rifleCount2 = count2;
        save.rifleCount3 = count3;
        SavingSystem<RifleData>.Save(Application.persistentDataPath + "/file10.pqr", save);
    }

    public void LoadGame()
    {
        RifleData load = SavingSystem<RifleData>.Load(Application.persistentDataPath + "/file10.pqr");
        rifleActive = load.rifleActiveFile;
        magR = load.magRFile;
        rifleBullets = load.rifleBulletsFile;
        count1 = load.rifleCount1;
        count2 = load.rifleCount2;
        count3 = load.rifleCount3;
    }
}

[System.Serializable]
public class RifleData
{
    public bool rifleActiveFile;
    public int magRFile;
    public int rifleBulletsFile;
    public int rifleCount1;
    public int rifleCount2;
    public int rifleCount3;
}