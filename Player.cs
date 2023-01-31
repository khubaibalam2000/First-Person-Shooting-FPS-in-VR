using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PauseMenu
{
    private MedKit medk;
    private Player playe1;
    private Player playe2;
    public GameObject gameEnd;
    public GameObject thanks;
    public GameObject resume;
    public Text health;
    private bool gameCompleted;
    public float playerHealth;
    private int tempHealth;
    private int tempMedkitAmounts;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 80000f;
        tempHealth = (int)playerHealth;
        medk = GameObject.FindGameObjectWithTag("Player").GetComponent<MedKit>();
        playe1 = this;
        gameCompleted = false;
        resume.SetActive(false);
        Cursor.visible = false;
        playe2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameEnd.SetActive(false);
        thanks.SetActive(false);
    }

    private void Update()
    {
        tempMedkitAmounts = medk.GetAmountOfMedkits();
        tempHealth = (int)playerHealth / 800;
        health.text = tempHealth.ToString();
        if (Input.GetKeyDown(KeyCode.Alpha6) && playerHealth <= 40000f && tempMedkitAmounts > 0 && !gameIsPause)
        {
            
            tempMedkitAmounts--;
            medk.SetAmountOfMedkits(tempMedkitAmounts);
            playe2 = medk + playe1;
        }
    }

    public bool GetGameCompleted()
    {
        return gameCompleted;
    }

    public static Player operator + (MedKit m, Player p1)
    {
        m = GameObject.FindGameObjectWithTag("Player").GetComponent<MedKit>();
        Player p2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p2.playerHealth = p1.playerHealth + m.GetMedKitHealth();
        return p2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ZombieAI1.zombieCounter > 70 && (other.gameObject.name == "Cube (5)" || other.gameObject.name == "Cube (6)" || other.gameObject.name == "Cube (7)" || other.gameObject.name == "Cube (8)"))
        {
            Cursor.visible = true;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPause = true;
            gameEnd.SetActive(true);
            thanks.SetActive(true);
            resume.SetActive(false);
            gameCompleted = true;
        }
    }

    public void SaveGame1()
    {
        PlayerData save = new PlayerData();
        save.playerHealthFile = playerHealth;
        SavingSystem<PlayerData>.Save(Application.persistentDataPath + "/file7.pqr", save);
    }

    public void LoadGame1()
    {
        gameEnd.SetActive(false);
        Cursor.visible = false;
        thanks.SetActive(false);
        gameCompleted = false;
        PlayerData load = SavingSystem<PlayerData>.Load(Application.persistentDataPath + "/file7.pqr");
        playerHealth = load.playerHealthFile;
    }
}

[System.Serializable]
public class PlayerData
{
    public float playerHealthFile;
}