using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2D : TemporalSingleton<PlayerController2D>
{
    //Velocity variables
    [SerializeField] float m_Xvelocity;
    [SerializeField] float m_Yvelocity;

    [SerializeField] float m_reductionFactor;

    Rigidbody2D m_myRigidbody;
    [SerializeField] Transform playerPosReference;

    SpriteRenderer m_myRenderer;
    Animator m_myAnimator;
    public override void Awake()
    {
        base.Awake();
        m_myRigidbody=gameObject.GetComponent<Rigidbody2D>();
        m_myRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_myAnimator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Animación de andar del jugador
        if (m_myRigidbody.velocity.x > 0.1f || m_myRigidbody.velocity.x < -0.1f || m_myRigidbody.velocity.y < -0.1f || m_myRigidbody.velocity.y > 0.1f)
        {
            
            m_myAnimator.SetBool("isMoving", true);
        }
        else
        {
            m_myAnimator.SetBool("isMoving", false);
        }
        m_myRigidbody.velocity = (Vector3.right * InputManager.Instance.MovementHorizontalAxis * m_Xvelocity)+(Vector3.up * InputManager.Instance.MovementVerticalAxis * m_Yvelocity); //Movimiento
        transform.localScale = new Vector3((transform.position.y * m_reductionFactor+1), transform.position.y * m_reductionFactor + 1, 1);

        //Rotación del sprite del jugador
        if (InputManager.Instance.MovementHorizontalAxis > 0)
        {
            m_myRenderer.flipX = false;
        }
        else if (InputManager.Instance.MovementHorizontalAxis < 0)
        {
            m_myRenderer.flipX = true;
        }
        
    }
    public float GetPlayerYPosition()
    {
        return playerPosReference.position.y;
    }
    public float GetPlayerXPosition()
    {
        return playerPosReference.position.x;
    }
}


