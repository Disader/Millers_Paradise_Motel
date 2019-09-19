using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Eventer : MonoBehaviour
{
    [SerializeField] ScriptableEvent eventToPlay;
    public List<GameObject> ObjectsToChange = new List<GameObject>();
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController2D>() != null)
        {
            DirectorScript.Instance.StartEvent(eventToPlay.playable);
            EventsDialogSystem.Instance.ChangeDialog(eventToPlay.dialog);
            Destroy(gameObject);
            if (ObjectsToChange != null)
            {
                foreach(GameObject actualObject in ObjectsToChange)
                {
                    if (actualObject != null)
                    {
                        actualObject.SetActive(!actualObject.activeSelf);
                    }
                }
            }
        }
    }
}
