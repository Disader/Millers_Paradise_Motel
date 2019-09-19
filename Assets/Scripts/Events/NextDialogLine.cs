using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogLine : MonoBehaviour
{

    void OnEnable()
    {
        EventsDialogSystem.Instance.NextDialogLine();
    }
    private void OnDisable()
    {
        EventsDialogSystem.Instance.HideText();
    }

}
