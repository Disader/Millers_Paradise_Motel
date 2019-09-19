using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : TemporalSingleton<RoomManager>
{
    public List<GameObject> rooms;
    int activeRoom;
    public Transform player;
    List<Transform> NPCsToChange=new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= rooms.Count; i++)
        {
            if (rooms[i].activeSelf)
            {
                activeRoom = i;
                break;
            }
        }
    }

    public void ChangeRoom(string roomName, Transform entrance, Vector2 offset)
    {
        for (int i = 0; i <= rooms.Count; i++)
        {
            if (rooms[i].name == roomName)
            {

                rooms[activeRoom].SetActive(false);
                activeRoom = i;
                rooms[i].SetActive(true);
                player.position = entrance.position + new Vector3(offset.x, offset.y);
                foreach(Transform transToMove in NPCsToChange)
                {
                    transToMove.position = entrance.position;
                }

                break;
            }
        }
    }
    public void AddNPCToChange(Transform newTransform)
    {
        if (!NPCsToChange.Contains(newTransform))
        {
            NPCsToChange.Add(newTransform);
        }   
    }
    public void RemoveNPCToChange(Transform transToRemove)
    {
        if (NPCsToChange.Contains(transToRemove))
        {
            NPCsToChange.Remove(transToRemove);
        }
    }
}
