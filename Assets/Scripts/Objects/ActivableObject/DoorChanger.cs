using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChanger : ActivableObject
{
    [SerializeField] GameObject doorObject;
    public override void ActivateObject()
    {
        doorObject.SetActive(true);
    }
}
