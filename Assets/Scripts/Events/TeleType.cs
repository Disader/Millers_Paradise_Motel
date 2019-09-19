using UnityEngine;
using System.Collections;
using TMPro;

public class TeleType : MonoBehaviour
    {
    
    [Range(0, 1)]
    public float RevealPercentage = 0.0f;

    //private string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";

    private TMP_Text m_textMeshPro;

    void Awake()
    {
        // Get Reference to TextMeshPro Component
        m_textMeshPro = GetComponent<TMP_Text>();
        m_textMeshPro.maxVisibleCharacters = 0;
    }


    void Update()
    {

        // Force and update of the mesh to get valid information.
        m_textMeshPro.ForceMeshUpdate();
        
        int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
        //int counter = 0;
        int visibleCount = 0;
        visibleCount = (int)(totalVisibleCharacters * RevealPercentage);

        m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

        // Once the last character has been revealed, wait 1.0 second and start over.
        if (visibleCount >= totalVisibleCharacters)
        {

        }

       
    }

}


