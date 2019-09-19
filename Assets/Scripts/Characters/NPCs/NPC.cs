using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject m_arrow;

    public bool followPlayer;
    NavMeshAgent agent;
    [SerializeField] float m_reductionFactor;

    private void Start()
    {
        transform.localScale = new Vector3((transform.position.y * m_reductionFactor + 1), transform.position.y * m_reductionFactor + 1, 1);
        if (followPlayer)
        {
            StartFollowing();
        }
    }
    private void Update()
    {
        if (followPlayer)
        {
            agent.destination = new Vector3(PlayerController2D.Instance.GetPlayerXPosition(), 0, PlayerController2D.Instance.GetPlayerYPosition());
            transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3((transform.position.y * m_reductionFactor + 1), transform.position.y * m_reductionFactor + 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            if (m_arrow != null)
            {
                m_arrow.SetActive(true);
            }
            

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            if (m_arrow != null)
            {
                m_arrow.SetActive(false);
            }
        }
    }
    public void StartFollowing()
    {
        followPlayer = true;
        SetAgent();
        agent.enabled = true;
    }
    public void StopFollowing()
    {
        followPlayer = false;
        RoomManager.Instance.RemoveNPCToChange(transform);
        agent.enabled = false;
    }

    void SetAgent()
    {
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {
            agent = gameObject.GetComponent<NavMeshAgent>();            
        }
        else if (gameObject.GetComponent<NavMeshAgent>() == null)
        {
            agent = gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        }
        agent.stoppingDistance = 2.5f;
        agent.radius = 0.2f;
        agent.height = 0.5f;
        RoomManager.Instance.AddNPCToChange(transform);
    }
}