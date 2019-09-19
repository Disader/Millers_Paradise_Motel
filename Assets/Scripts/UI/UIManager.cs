using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : TemporalSingleton<UIManager>
{
    [SerializeField] GameObject m_inventoryUI;
    bool m_inventoryIsShown;
    [SerializeField] float m_showInventoryPercentage;
    [SerializeField] float m_hideInventoryPercentage;
    CanvasGroup m_canvasGroup;
    [SerializeField] GameObject m_canvas;

    public override void Awake()
    {
        base.Awake();
        m_canvasGroup = m_inventoryUI.GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (!m_inventoryIsShown && (Input.mousePosition.y * 100 / Screen.height) <= m_showInventoryPercentage)
        {
            ShowUI();
        }
        if (m_restart && m_inventoryIsShown && (Input.mousePosition.y * 100 / Screen.height) >= m_hideInventoryPercentage)
        {
            HideUI();
        }
        if((Input.mousePosition.y * 100 / Screen.height) <= m_hideInventoryPercentage){
            m_restart=true;
        }
    }
 
    public void HideAllUIEvents()
    {
        m_canvas.SetActive(false);
    }
    public void ShowAllUIEvents()
    {
        m_canvas.SetActive(true);
    }
    public void ShowUI()
    {
        if (!m_isPaused)
        {
            InputManager.Instance.PauseInteraction();
            InputManager.Instance.PauseMovement();
            m_canvasGroup.alpha = 1f;
            m_canvasGroup.blocksRaycasts = true;
            m_inventoryIsShown = true;
        }  
    }
    bool m_restart;
    public void HideUI()
    {
        if (!m_isPaused)
        {
            InputManager.Instance.ResumeInteraction();
            InputManager.Instance.ResumeMovement();
            m_restart = false;
            m_canvasGroup.alpha = 0f;
            m_canvasGroup.blocksRaycasts = false;
            m_inventoryIsShown = false;
            InventoryUI.Instance.StopComparing();
        }
    }
    bool m_isPaused;
    bool m_itIsShown;
    int m_actualInventoyPauses;
    public void PauseInventory()
    {
        m_isPaused = true;
        m_canvasGroup.blocksRaycasts = false;
        m_actualInventoyPauses++;
        if (m_canvasGroup.blocksRaycasts)
        {
            m_itIsShown = true;
        }
        else
        {
            m_itIsShown = false;
        }
    }
    public void ResumeInventory()
    {
        m_isPaused = false;
        m_actualInventoyPauses--;
        if (m_actualInventoyPauses==0)
        {
            if (m_itIsShown)
            {
                m_canvasGroup.blocksRaycasts = true;
            }
            else
            {
                m_canvasGroup.blocksRaycasts = false;
            }
        }
      
       
    }
}
