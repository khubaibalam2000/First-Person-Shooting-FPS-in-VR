using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private float medKitHealth;
    private int amountOfMedkits;

    private void Start()
    {
        medKitHealth = 40000f;
        amountOfMedkits = 2;
    }

    public void SetAmountOfMedkits(int temp)
    {
        amountOfMedkits = temp;
    }

    public float GetMedKitHealth()
    {
        return medKitHealth;
    }

    public int GetAmountOfMedkits()
    {
        return amountOfMedkits;
    }

    public void SaveGame()
    {
        MedkitData save = new MedkitData();
        save.amountOfMedkitsFile = amountOfMedkits;
        SavingSystem<MedkitData>.Save(Application.persistentDataPath + "/file5.pqr", save);
    }

    public void LoadGame()
    {
        MedkitData load = SavingSystem<MedkitData>.Load(Application.persistentDataPath + "/file5.pqr");
        amountOfMedkits = load.amountOfMedkitsFile;
    }
}

[System.Serializable]
public class MedkitData
{
    public int amountOfMedkitsFile;
}