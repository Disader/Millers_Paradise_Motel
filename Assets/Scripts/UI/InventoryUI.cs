using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : TemporalSingleton<InventoryUI>
{
    
    public List<InventoryItemUI> myInventorySpaces;

    //Comprueba el siguiente espacio vacío del inventario e introduce el item que se quiera introducir en el inventario
    public void AddItem(InventoryItem newObject)
    {
        foreach (InventoryItemUI inventoryItem in myInventorySpaces)
        {
            if (inventoryItem.m_myActualObject == null)
            {
                inventoryItem.FillSpace(newObject);
                break;
            }
        }
    }
    InventoryItem neededItem;
    ObjectScript usedObject;
    public void StopComparing()
    {
        neededItem = null;
        usedObject = null;
    }
    public void SetNeededObject(InventoryItem newItem,ObjectScript newObject)
    {
        neededItem = newItem;
        usedObject = newObject;
    }
    public bool CompareItems(InventoryItem storedItem)
    {
        if (neededItem!=null && storedItem == neededItem)
        {
            if (usedObject != null)
            {
                usedObject.ActivateObject();
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
