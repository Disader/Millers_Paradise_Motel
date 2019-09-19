using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class PuzzleManager : TemporalSingleton<PuzzleManager>
{

    [SerializeField] ScriptableEvent startingEvent;

    public List<PuzleListElements> puzzlesList;
    List<string> currentObjetives = new List<string>();
    public int currentPuzzle;
    // Start is called before the first frame update
 
    void Start()
    {
        RefreshObjetives();
        if (startingEvent != null)
        {
            DirectorScript.Instance.StartEvent(startingEvent.playable);
            EventsDialogSystem.Instance.ChangeDialog(startingEvent.dialog);
        }                
    }

    public void CheckObject(string completed)
    {
        if (puzzlesList[currentPuzzle].puzzle.ordered && completed == currentObjetives[0])
        {
            currentObjetives.Remove(completed);
            PuzzleCompleted();
        }
        else if(!puzzlesList[currentPuzzle].puzzle.ordered)
        {
            for (int i = 0; i < currentObjetives.Count; i++)
            {
                if (currentObjetives[i] == completed)
                {
                    currentObjetives.Remove(completed);
                    PuzzleCompleted();
                    break;
                }
            }
        }
    }

    void RefreshObjetives()
    {
        if (puzzlesList.Count>currentPuzzle&& puzzlesList[currentPuzzle]!=null)
        {
            foreach (ScriptableObject currentInventoryItem in puzzlesList[currentPuzzle].puzzle.objetives)
            {
                if (currentInventoryItem as ScriptableDialog != null)
                {
                    currentObjetives.Add((currentInventoryItem as ScriptableDialog).ID);
                }
                if (currentInventoryItem as InventoryItem != null)
                {
                    currentObjetives.Add((currentInventoryItem as InventoryItem).ID);
                }
            }
        }
        else
        {
            EndGame();
        }
    }

    void PuzzleCompleted()
    {
        if (currentObjetives.Count == 0 && puzzlesList[currentPuzzle] != null)
        {
            Debug.Log("puzlecompleted");
            MilestoneEvents();
            currentPuzzle++;
            RefreshObjetives();
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene(AppScenes.CREDITS_SCENE);
    }
 


    void MilestoneEvents()
    {
        foreach (GameObject currentObject in puzzlesList[currentPuzzle].changesAtEnd.objectsToChange)
        {
            if (currentObject != null)
            {
                currentObject.SetActive(!currentObject.activeSelf);
            }
        }

        foreach (Dialog currentDialog in puzzlesList[currentPuzzle].changesAtEnd.dialogsToChange)
        {
            if (currentDialog.GetComponent<Dialog>() != null)
            {
                currentDialog.GetComponent<Dialog>().NextDialog();
            }
        }
        foreach (NPC currentNPC in puzzlesList[currentPuzzle].changesAtEnd.NPCWhoChangeFollowing)
        {
            if (currentNPC.followPlayer)
            {
                currentNPC.StopFollowing();
            }
            else if (!currentNPC.followPlayer)
            {
                currentNPC.StartFollowing();
            }

        }

        if (puzzlesList[currentPuzzle].changesAtEnd.endEvent != null)
        {
            DirectorScript.Instance.StartEvent(puzzlesList[currentPuzzle].changesAtEnd.endEvent.playable);
            EventsDialogSystem.Instance.ChangeDialog(puzzlesList[currentPuzzle].changesAtEnd.endEvent.dialog);
        }
        Debug.Log("newMilestone");
        if (puzzlesList[currentPuzzle].changesAtEnd.doorDestination != null)
        {
            puzzlesList[currentPuzzle].changesAtEnd.doorDestination.UseDoor();
        }
    }
}

[System.Serializable]
public class PuzleListElements
{
    public ScriptablePuzle puzzle;
    public Milestone changesAtEnd;
}

[System.Serializable]
public class Milestone
{
    public List<GameObject> objectsToChange;
    public List<Dialog> dialogsToChange;
    public List<NPC> NPCWhoChangeFollowing;
    public ScriptableEvent endEvent;
    public RoomChanger doorDestination;
}
