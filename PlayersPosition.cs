using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersPosition : MonoBehaviour
{
    private PauseMenu pause;
    public Vector3 position;

    // Start is called before the first frame update
    private void Start()
    {
        pause = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    public void SaveGame()
    {
        PlayerPositionData save = new PlayerPositionData();
        save.position[0] = transform.position.x;
        save.position[1] = transform.position.y;
        save.position[2] = transform.position.z;
        SavingSystem<PlayerPositionData>.Save(Application.persistentDataPath + "/file9.pqr", save);
    }

    public void LoadGame()
    {
        PlayerPositionData load = SavingSystem<PlayerPositionData>.Load(Application.persistentDataPath + "/file9.pqr");
        position.x = load.position[0];
        position.y = load.position[1];
        position.z = load.position[2];
        transform.position = position;
        pause.Resume();
    }
}

[System.Serializable]
public class PlayerPositionData
{
    public float[] position;

    public PlayerPositionData()
    {
        position = new float[3];
    }
}