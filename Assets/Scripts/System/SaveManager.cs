using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : TemporalSingleton<SaveManager>
{
    public SaveItem mySave;

    void SaveJSON()
    {
        string json = JsonUtility.ToJson(mySave);
        File.WriteAllText(Application.persistentDataPath + "/GameData.json", json);
    }
    void LoadJSON()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/GameData.json");
        mySave = JsonUtility.FromJson<SaveItem>(json);
    }
}
[System.Serializable]
public class SaveItem
{
    public GameObject actualRoom;
    public int actualPuzzle;
    public List<NPC> activeNPCs;
    public List<ScriptableObject> inventoryObjects;
    public Dictionary<ObjectScript, bool> usedObjects = new Dictionary<ObjectScript, bool>();
}
