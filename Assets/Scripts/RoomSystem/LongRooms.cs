using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public class LongRooms : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera vCamera;
    public GameObject lobby;
    public GameObject parking;
    public Transform player;
    Cinemachine.CinemachineConfiner vConfiner;
    [SerializeField] float xPos;
    [SerializeField] float yPos;
    // Start is called before the first frame update
    void Start()
    {
        vCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vConfiner = GetComponent<Cinemachine.CinemachineConfiner>();
;    }

    // Update is called once per frame
    void Update()
    {
        if(lobby.activeSelf || parking.activeSelf)
        {
            vCamera.Follow = player;
            vConfiner.enabled = true;

        } else
        {
            vCamera.Follow = null;
            vConfiner.enabled = false;
            vCamera.transform.position = new Vector3(xPos, yPos, -1f);
        }
    }
}
