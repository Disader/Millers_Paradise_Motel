using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "Object", order = 1)]
public class InventoryItem : ScriptableObject
{
    public string ID;
    public Sprite icon;
    public string objName;
    [TextArea]
    public string description;
    public ObjectType objType;
}