using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddPlayerToDialogSystem : MonoBehaviour
{
    private TextMeshProUGUI m_textMeshPro;
    [SerializeField] ScriptableCharacter character;

    void Start()
    {
        // Get Reference to TextMeshPro Component
        m_textMeshPro = GetComponent<TextMeshProUGUI>();
        EventsDialogSystem.Instance.AddDialogText(character.characterName, m_textMeshPro);
        m_textMeshPro.maxVisibleCharacters = 0;
    }
}
