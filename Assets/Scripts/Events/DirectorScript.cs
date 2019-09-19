using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DirectorScript : TemporalSingleton<DirectorScript>
{
    PlayableDirector myDirector;

    public override void Awake()
    {
        base.Awake();
        myDirector = GetComponent<PlayableDirector>();       
    }

    public void StartEvent(PlayableAsset newEvent)
    {
        if (newEvent != null)
        {
            myDirector.playableAsset = newEvent;
            myDirector.Play();
            UIManager.Instance.PauseInventory();
            InputManager.Instance.PauseMovement();
            UIManager.Instance.HideAllUIEvents();
        }  
    }

    void OnEnable()
    {
        myDirector.stopped += OnPlayableDirectorStopped;
    }
    void OnDisable()
    {
        myDirector.stopped -= OnPlayableDirectorStopped;
    }
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        InputManager.Instance.ResumeMovement();
        UIManager.Instance.ResumeInventory();
        EventsDialogSystem.Instance.NextDialog();
        UIManager.Instance.ShowAllUIEvents();
    }   
}
