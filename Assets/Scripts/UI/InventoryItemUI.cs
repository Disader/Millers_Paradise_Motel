using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] Image m_myIcon;
    public InventoryItem m_myActualObject;
    [SerializeField] TextMeshProUGUI m_description;

    public void FillSpace(InventoryItem newItem)
    {
        m_myActualObject = newItem;
        m_myIcon.sprite = newItem.icon;
        m_description.text = m_myActualObject.description;
    }
    public void ClearSpace()
    {
        m_myActualObject = null;
        m_myIcon.sprite = null;
        m_description.text = string.Empty;
    }
    public void ShowDescription()
    {
        m_description.gameObject.SetActive(true);
    }
    public void HideDescription()
    {
        m_description.gameObject.SetActive(false);
    }
    public void UseObject()
    {
        if (InventoryUI.Instance.CompareItems(m_myActualObject))
        {
            ClearSpace();
        }
    }
}
