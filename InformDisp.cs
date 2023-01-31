using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformDisp : MonoBehaviour, ISoundOfPop
{
    public GameObject[] msg;
    private bool[] alreadyPlayed;
    public AudioClip pop;
    AudioSource pop1;

    public bool[] GetAlreadyPlayed()
    {
        return alreadyPlayed;
    }

    // Start is called before the first frame update
    void Start()
    {
        alreadyPlayed = new bool[13];
        for(int i=0; i<13; i++)
        {
            msg[i].SetActive(false);
            alreadyPlayed[i] = false;
        }
        pop1 = GameObject.Find("Pop Audio").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "info" && !alreadyPlayed[0])
        {
            msg[0].SetActive(true);
            Sound();
            StartCoroutine(cube0());
            alreadyPlayed[0] = true;

        }
        if (other.gameObject.name == "info (1)" && !alreadyPlayed[1])
        {
            msg[1].SetActive(true);
            Sound();
            StartCoroutine(cube1());
            alreadyPlayed[1] = true;
        }
        if (other.gameObject.name == "info (2)" && !alreadyPlayed[2])
        {
            msg[2].SetActive(true);
            Sound();
            StartCoroutine(cube2());
            alreadyPlayed[2] = true;
        }
        if (other.gameObject.name == "info (3)" && !alreadyPlayed[3])
        {
            msg[3].SetActive(true);
            Sound();
            StartCoroutine(cube3());
            alreadyPlayed[3] = true;
        }
        if (other.gameObject.name == "info (4)" && !alreadyPlayed[4])
        {
            msg[4].SetActive(true);
            Sound();
            StartCoroutine(cube4());
            alreadyPlayed[4] = true;
        }
        if (other.gameObject.name == "info (5)" && !alreadyPlayed[5])
        {
            msg[5].SetActive(true);
            Sound();
            StartCoroutine(cube5());
            alreadyPlayed[5] = true;
        }
        if (other.gameObject.name == "info (6)" && !alreadyPlayed[6])
        {
            msg[6].SetActive(true);
            Sound();
            StartCoroutine(cube6());
            alreadyPlayed[6] = true;
        }
        if (other.gameObject.name == "info (7)" && !alreadyPlayed[7])
        {
            msg[7].SetActive(true);
            Sound();
            StartCoroutine(cube7());
            alreadyPlayed[7] = true;
        }
        if (other.gameObject.name == "info (8)" && !alreadyPlayed[8])
        {
            msg[8].SetActive(true);
            Sound();
            StartCoroutine(cube8());
            alreadyPlayed[8] = true;
        }
        if (other.gameObject.name == "info (9)" && !alreadyPlayed[9])
        {
            msg[9].SetActive(true);
            Sound();
            StartCoroutine(cube9());
            alreadyPlayed[9] = true;
        }
        if (other.gameObject.name == "info (10)" && !alreadyPlayed[10])
        {
            msg[10].SetActive(true);
            Sound();
            StartCoroutine(cube10());
            alreadyPlayed[10] = true;
        }
        if (other.gameObject.name == "info (11)" && !alreadyPlayed[11])
        {
            msg[11].SetActive(true);
            Sound();
            StartCoroutine(cube11());
            alreadyPlayed[11] = true;
        }
        if (other.gameObject.name == "info (12)" && !alreadyPlayed[12])
        {
            msg[12].SetActive(true);
            Sound();
            StartCoroutine(cube12());
            alreadyPlayed[12] = true;
        }
    }
    IEnumerator cube0()
    {
        yield return new WaitForSeconds(5f);
        msg[0].SetActive(false);
    }
    IEnumerator cube1()
    {
        yield return new WaitForSeconds(5f);
        msg[1].SetActive(false);
    }
    IEnumerator cube2()
    {
        yield return new WaitForSeconds(2.75f);
        msg[2].SetActive(false);
    }
    IEnumerator cube3()
    {
        yield return new WaitForSeconds(4f);
        msg[3].SetActive(false);
    }
    IEnumerator cube4()
    {
        yield return new WaitForSeconds(4f);
        msg[4].SetActive(false);
    }
    IEnumerator cube5()
    {
        yield return new WaitForSeconds(3f);
        msg[5].SetActive(false);
    }
    IEnumerator cube6()
    {
        yield return new WaitForSeconds(3f);
        msg[6].SetActive(false);
    }
    IEnumerator cube7()
    {
        yield return new WaitForSeconds(10f);
        msg[7].SetActive(false);
    }
    IEnumerator cube8()
    {
        yield return new WaitForSeconds(10f);
        msg[8].SetActive(false);
    }
    IEnumerator cube9()
    {
        yield return new WaitForSeconds(10f);
        msg[9].SetActive(false);
    }
    IEnumerator cube10()
    {
        yield return new WaitForSeconds(1.85f);
        msg[10].SetActive(false);
    }
    IEnumerator cube11()
    {
        yield return new WaitForSeconds(8f);
        msg[11].SetActive(false);
    }
    IEnumerator cube12()
    {
        yield return new WaitForSeconds(4f);
        msg[12].SetActive(false);
    }
    public void Sound()
    {
        pop1.PlayOneShot(pop, 15f);
    }

    public void SaveGame()
    {
        InfoData save = new InfoData();
        save.informAlreadyPlayedFile = GetAlreadyPlayed();
        SavingSystem<InfoData>.Save(Application.persistentDataPath + "/file2.pqr", save);
    }

    public void LoadGame()
    {
        InfoData load = SavingSystem<InfoData>.Load(Application.persistentDataPath + "/file2.pqr");
        alreadyPlayed = load.informAlreadyPlayedFile;
    }
}

[System.Serializable]
public class InfoData
{
    public bool[] informAlreadyPlayedFile;

    public InfoData() 
    {
        informAlreadyPlayedFile = new bool[13];
    }
}