using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog", order = 3)]
public class ScriptableDialog : ScriptableObject
{
    public string ID;
    public List<DialogInfo> dialogs;
}
[System.Serializable]
public class DialogInfo
{
    public TextAsset jsonAsset;
    public InventoryItem objectGivenAtEnd;
    public bool newDialogAtEnd;
}
