using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISoundOfPop
{
    #region Singleton
    public static PlayerManager instance;
    private bool alreadyPlayed;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public GameObject[] endGame;
    public AudioClip popOut;
    AudioSource popOut1;

    public bool GetAlreadyPlayed()
    {
        return alreadyPlayed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        alreadyPlayed = false;
        for(int i=0; i<6; i++)
        {
            endGame[i].SetActive(false);
        }
        popOut1 = GameObject.Find("Pop Audio").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(ZombieAI1.zombieCounter == 70 && !alreadyPlayed)
        {
            endGame[0].SetActive(true);
            Sound();
            StartCoroutine(End0());
            alreadyPlayed = true;
        }
        IEnumerator End0()
        {
            yield return new WaitForSeconds(10f);
            endGame[0].SetActive(false);
            endGame[1].SetActive(true);
            Sound();
            StartCoroutine(End1());
        }
        IEnumerator End1()
        {
            yield return new WaitForSeconds(10f);
            endGame[1].SetActive(false);
            endGame[2].SetActive(true);
            Sound();
            StartCoroutine(End2());
        }
        IEnumerator End2()
        {
            yield return new WaitForSeconds(10f);
            endGame[2].SetActive(false);
            endGame[3].SetActive(true);
            Sound();
            StartCoroutine(End3());
        }
        IEnumerator End3()
        {
            yield return new WaitForSeconds(10f);
            endGame[3].SetActive(false);
            endGame[4].SetActive(true);
            Sound();
            StartCoroutine(End4());
        }
        IEnumerator End4()
        {
            yield return new WaitForSeconds(10f);
            endGame[4].SetActive(false);
            endGame[5].SetActive(true);
            Sound();
            StartCoroutine(End5());
        }
        IEnumerator End5()
        {
            yield return new WaitForSeconds(10f);
            endGame[5].SetActive(false);
        }
    }
    public void Sound()
    {
        popOut1.PlayOneShot(popOut, 16f);
    }

    public void SaveGame()
    {
        PlayerManagerData save = new PlayerManagerData();
        save.playerManAlreadyPlayedFile = GetAlreadyPlayed();
        SavingSystem<PlayerManagerData>.Save(Application.persistentDataPath + "/file8.pqr", save);
    }

    public void LoadGame()
    {
        PlayerManagerData load = SavingSystem<PlayerManagerData>.Load(Application.persistentDataPath + "/file8.pqr");
        alreadyPlayed = load.playerManAlreadyPlayedFile;
    }
}

[System.Serializable]
public class PlayerManagerData
{
    public bool playerManAlreadyPlayedFile;
}