using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        m_isInPause = true;
        InputManager.Instance.PauseMovement();
        InputManager.Instance.PauseInteraction();
        UIManager.Instance.PauseInventory();
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        m_isInPause = false;
        InputManager.Instance.ResumeMovement();
        InputManager.Instance.ResumeInteraction();
        UIManager.Instance.ResumeInventory();
    }
    bool m_isInPause;
    private void Update()
    {
        if (InputManager.Instance.PauseKey && !m_isInPause)
        {
            PauseGame();
        }
        else if(InputManager.Instance.PauseKey && m_isInPause)
        {
            ResumeGame();
        }
    }
}
