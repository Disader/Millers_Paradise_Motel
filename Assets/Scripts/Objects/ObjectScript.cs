using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectScript : MonoBehaviour
{
    public InventoryItem m_objectScriptable; //Item guardado en esta posicion
    SpriteRenderer rend;//Sprite del objeto en escena

    [SerializeField] TextMeshProUGUI m_descriptionText;
    [SerializeField] GameObject m_panel;
    [SerializeField] GameObject m_arrow;
    [SerializeField] GameObject m_pickUpButton;
    [SerializeField] GameObject m_interactButton;
    public Sprite useSpriteToChange;
    public GameObject useActivateObject;
    public GameObject objectToMove;
    public Transform destination;

    [SerializeField] GameObject m_usedWithObject;
    public InventoryItem itemNeeded;


    void Start()
    {
        
        //Llamada a funciones y dar valor a las variables
        rend = GetComponent<SpriteRenderer>();
        NewItem();
    }
    void NewItem()
    {
        m_descriptionText.text = m_objectScriptable.description;
    }
    void DeleteItem()
    {
        m_objectScriptable = null;
        m_descriptionText.text = null;
        rend.sprite = null;
        m_playerIsInside = false;
        GetComponent<Collider2D>().enabled = false;
        SaveManager.Instance.mySave.usedObjects[this]= true;
    }

    private void Update()
    {
        if (m_playerIsInside && InputManager.Instance.InteractionKey)
        {
            ShowPanel();
        }
    }
    bool m_playerIsInside;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null && m_objectScriptable != null)
        {
            m_arrow.SetActive(true);
            m_playerIsInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            m_arrow.SetActive(false);
            m_playerIsInside = false;
        }
    }
    public void PickUp()
    {
        if (m_objectScriptable != null)
        {
            PuzzleManager.Instance.CheckObject(m_objectScriptable.ID);
            InventoryUI.Instance.AddItem(m_objectScriptable);
            DeleteItem();
            HidePanel();
            m_arrow.SetActive(false);
        }
    }
    public void Leave()
    {
        HidePanel();
    }
    void HidePanel()
    {
        m_panel.gameObject.SetActive(false);
        InputManager.Instance.ResumeMovement();
    }
    void ShowPanel()
    {
        m_panel.gameObject.SetActive(true);
        InputManager.Instance.PauseMovement();

        switch (m_objectScriptable.objType)
        {
            case ObjectType.canBePickedUp:
                m_pickUpButton.SetActive(true);
                break;
            case ObjectType.canBeUsed:
                m_interactButton.SetActive(true);
                break;
            case ObjectType.canBeUsedWithAnObject:
                m_usedWithObject.SetActive(true);
                break;
            default:
                break;
        }         
    }

    public void UseObject ()
    {
        ActivateObject();
    }
    public void UsedByObject()
    {
        if (itemNeeded != null)
        {
            UIManager.Instance.ShowUI();
            InventoryUI.Instance.SetNeededObject(itemNeeded, this);
        }
    }
    public void ActivateObject()
    {
        HidePanel();
        m_playerIsInside = false;
        PuzzleManager.Instance.CheckObject(m_objectScriptable.ID);
        GetComponent<Collider2D>().enabled = false;

        ActivableObject activableObjectScript = gameObject.GetComponent<ActivableObject>();
        if (activableObjectScript != null)
        {
            activableObjectScript.ActivateObject();
        }
        if(useActivateObject != null)
        {          
            useActivateObject.SetActive(true);
        }
        if (useSpriteToChange != null)
        {
            rend.sprite = useSpriteToChange;   
        }
        if(objectToMove != null)
        {
            objectToMove.transform.position = destination.position;
        }
    }
}
