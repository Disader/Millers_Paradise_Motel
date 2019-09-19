using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerChanger : MonoBehaviour
{
    [SerializeField] Transform m_feetTransformReference;
    SpriteRenderer m_mySpriteRenderer;
    private void Start()
    {
        m_mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
       
        //Controla la posición del jugador para pintarse por delante o detrás de este
        if (PlayerController2D.Instance.GetPlayerYPosition() <= m_feetTransformReference.position.y)
        {        
            m_mySpriteRenderer.sortingLayerName = AppSortingLayers.CHARACTER_NORMAL;
        }
        else if (PlayerController2D.Instance.GetPlayerYPosition() >= m_feetTransformReference.position.y)
        {
            m_mySpriteRenderer.sortingLayerName = AppSortingLayers.CHARACTER_OVER;
        }       

    }
}
