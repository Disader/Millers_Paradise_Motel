using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : TemporalSingleton<InputManager>
{
    //Interaction
    int actualInteractionPauses;
    bool canInteract = true;

    public bool InteractionKey
    {
        get
        {
            if (canInteract)
            {
                return Input.GetButtonDown("Interact");
            }
            else
            {
                return false;
            }
            
        }
    }

    public void PauseInteraction()
    {
        actualInteractionPauses++;
        canInteract = false;
    }
    public void ResumeInteraction()
    {
        actualInteractionPauses--;
        actualInteractionPauses = Mathf.Clamp(actualInteractionPauses, 0, 10000);
        if (actualInteractionPauses == 0)
        {
            canInteract = true;
        }    
    }

    //Movement
    bool canMove = true;
    int actualMovementPauses;

    public float MovementHorizontalAxis
    {
        get
        {
            if (canMove)
            {
                return Input.GetAxis("Horizontal");
            }
            else
            {
                return 0;
            }

        }
    }
    public float MovementVerticalAxis
    {
        get
        {
            if (canMove)
            {
                return Input.GetAxis("Vertical");
            }
            else
            {
                return 0;
            }

        }
    }

    public void PauseMovement()
    {
        actualMovementPauses++;
        canMove = false;
    }
    public void ResumeMovement()
    {
        actualMovementPauses--;
        actualMovementPauses = Mathf.Clamp(actualMovementPauses, 0, 10000);
        if (actualMovementPauses == 0)
        {
            canMove = true;
        }
    }

    //Pause
    public bool PauseKey
    {
        get
        {
            return Input.GetButtonDown("Pause");
        }
    }
}
