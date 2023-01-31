using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Animator animKnife;
    public GameObject knife;
    public GameObject knifeImage;
    private Inventory invent;
    public AudioClip knifeShots;
    AudioSource knifeShots1;
    private PauseMenu pm;
    public bool knifeActive;
    public bool firingKnife;
    private int count1;
    private float volume;
    public int knifeDamage;

    public int GetKnifeCount1()
    {
        return count1;
    }

    // Start is called before the first frame update
    void Start()
    {
        firingKnife = false;
        count1 = 0;
        knifeDamage = 15;
        volume = 15f;
        knifeActive = false;
        pm = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        knifeShots1 = GameObject.Find("knifeShots").GetComponent<AudioSource>();
        invent = GetComponent<Inventory>();
        animKnife = knife.GetComponent<Animator>();
        knife.SetActive(false);
        knifeImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && invent.enable[0])
        {
            knifeActive = true;
            knife.SetActive(true);
            knifeImage.SetActive(true);
            count1++;
        }
        if (count1 % 2 == 0)
        {
            knifeActive = false;
            knife.SetActive(false);
            knifeImage.SetActive(false);
        }

        if (knifeActive && !pm.gameIsPause)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                firingKnife = true;
                //Stabbing();
                knifeShots1.PlayOneShot(knifeShots, volume);
                animKnife.SetBool("Fire", true);
                StartCoroutine(Fire());
            }

            IEnumerator Fire()
            {
                yield return new WaitForSeconds(0.001f);
                animKnife.SetBool("Fire", false);
                firingKnife = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                firingKnife = true;
                //Stabbing();
                knifeShots1.PlayOneShot(knifeShots, volume);
                animKnife.SetBool("LeftFire", true);
                StartCoroutine(LeftFire());
            }

            IEnumerator LeftFire()
            {
                yield return new WaitForSeconds(0.01f);
                animKnife.SetBool("LeftFire", false);
                firingKnife = false;
            }
        }
    }

    public void SaveGame()
    {
        KnifeData save = new KnifeData();
        save.knifeActiveFile = knifeActive;
        save.knifeCount1File = GetKnifeCount1();
        SavingSystem<KnifeData>.Save(Application.persistentDataPath + "/file4.pqr", save);
    }

    public void LoadGame()
    {
        KnifeData load = SavingSystem<KnifeData>.Load(Application.persistentDataPath + "/file4.pqr");
        knifeActive = load.knifeActiveFile;
        count1 = load.knifeCount1File;
    }
}

[System.Serializable]
public class KnifeData
{
    public bool knifeActiveFile;
    public int knifeCount1File;
}