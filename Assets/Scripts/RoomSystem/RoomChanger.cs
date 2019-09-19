using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public string roomToGo;
    public Transform nextDoor;
    public Vector2 offset;
    public bool door;
    public GameObject hightlight;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        if (door)
        {
            rend.enabled = true;
        } else
        {
            rend.enabled = false;
        }
        hightlight.SetActive(false);
    }
    bool m_playerIsInside;
    private void Update()
    {
        if (m_playerIsInside && Input.GetButtonDown("Interact"))
        {   
            RoomManager.Instance.ChangeRoom(roomToGo, nextDoor, offset);
            MusicManager.Instance.PlaySound(AppSounds.DOOR_SFX);
            m_playerIsInside = false;
        }
    }
    public void UseDoor()
    {
        RoomManager.Instance.ChangeRoom(roomToGo, nextDoor, offset);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            if (door)
            {
                hightlight.SetActive(true);
                m_playerIsInside = true;
            }
            if (!door)
            {
                RoomManager.Instance.ChangeRoom(roomToGo, nextDoor, offset);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            m_playerIsInside = false;
            hightlight.SetActive(false);
        }
    }
    public void OnDisable()
    {
        hightlight.SetActive(false);
    }

}
