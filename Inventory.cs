using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject main_panel;
    public GameObject weapon_panel;
    public GameObject bow_panel;
    public GameObject pistol_panel;
    public GameObject knife_panel;
    public GameObject canteen_panel;
    public GameObject medical_kit_panel;
    public GameObject bag;
    public GameObject rifle;
    public GameObject crossbowToDestroy;
    public GameObject rifleToDestroy;
    public bool[] enable;
    private int count;

    public int GetCount()
    {
        return count;
    }

    private void Start()
    {
        count = 0;
        enable = new bool[6];
        for(int i = 0; i < 6; i++)
        {
            enable[i] = false;
        }
        weapon_panel.SetActive(false);
        main_panel.SetActive(false);
        bow_panel.SetActive(false);
        pistol_panel.SetActive(false);
        knife_panel.SetActive(false);
        canteen_panel.SetActive(false);
        medical_kit_panel.SetActive(false);
        rifle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("tab"))
        {
            main_panel.SetActive(true);
            weapon_panel.SetActive(true);
            if (enable[2])
            {
                bow_panel.SetActive(true);
            }
            if(enable[0] && enable[1] && enable[4] && enable[5])
            {
                pistol_panel.SetActive(true);
                knife_panel.SetActive(true);
                canteen_panel.SetActive(true);
                medical_kit_panel.SetActive(true);
            }
            if (enable[0] && enable[1] && enable[2] && enable[4] && enable[5])
            {
                bow_panel.SetActive(true);
                pistol_panel.SetActive(true);
                knife_panel.SetActive(true);
                canteen_panel.SetActive(true);
                medical_kit_panel.SetActive(true);
            }
            if (enable[0] && enable[1] && enable[2] && enable[3] && enable[4] && enable[5])
            {
                bow_panel.SetActive(true);
                pistol_panel.SetActive(true);
                knife_panel.SetActive(true);
                canteen_panel.SetActive(true);
                medical_kit_panel.SetActive(true);
                rifle.SetActive(true);
            }
        }
        else
        {
            main_panel.SetActive(false);
            weapon_panel.SetActive(false);
            bow_panel.SetActive(false);
            pistol_panel.SetActive(false);
            knife_panel.SetActive(false);
            canteen_panel.SetActive(false);
            medical_kit_panel.SetActive(false);
        }
        if (count >= 2)
        {
            Destroy(bag);
        }
        if (enable[2])
        {
            Destroy(crossbowToDestroy);
        }
        if (enable[3])
        {
            Destroy(rifleToDestroy);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //for crossbow
        if (other.gameObject.name == "cross" && Input.GetKey(KeyCode.G))
        {
            count++;
            enable[2] = true;
            
        }
        //for bagpack
        if(other.gameObject.name == "bagpack" && Input.GetKey(KeyCode.G))
        {
            enable[0] = true;
            enable[1] = true;
            enable[4] = true;
            enable[5] = true;
            count = count + 2;
        }
        //for rifle
        if(other.gameObject.name == "rifle" && Input.GetKey(KeyCode.G))
        {
            count = 4;
            enable[3] = true;
            other.gameObject.SetActive(false);
        }
    }

    public void SaveGame()
    {
        InventoryData save = new InventoryData();
        save.enableFile = enable;
        save.inventCountFile = GetCount();
        SavingSystem<InventoryData>.Save(Application.persistentDataPath + "/file3.pqr", save);
    }

    public void LoadGame()
    {
        InventoryData load = SavingSystem<InventoryData>.Load(Application.persistentDataPath + "/file3.pqr");
        enable = load.enableFile;
        count = load.inventCountFile;
    }
}

[System.Serializable]
public class InventoryData
{
    public bool[] enableFile;
    public int inventCountFile;

    public InventoryData()
    {
        enableFile = new bool[6];
    }
}