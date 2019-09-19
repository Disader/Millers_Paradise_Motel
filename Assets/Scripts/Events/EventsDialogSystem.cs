using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class EventsDialogSystem : TemporalSingleton<EventsDialogSystem>
{

    private Story story;
   [SerializeField] Dictionary<string, TextMeshProUGUI> m_dialogText = new Dictionary<string, TextMeshProUGUI>();

    [HideInInspector]public TextAsset actualDialog;
    string currentSpeaker;

  
    public void ChangeDialog(TextAsset eventDialog)
    {
        actualDialog = eventDialog;
    }
    public void NextDialog()
    {
        story = null;       
    }
    void StartStory()
    {
        InitSpeakers();
        story = new Story(actualDialog.text);
        RefreshView();
    }
    void InitSpeakers()
    {
        story = new Story(actualDialog.text);
        string rawText = story.Continue().Trim();
        string parsedText = ParseContent(rawText); //This also changes the actualspeaker

    }
    
    public void NextDialogLine()
    {
        if (story == null)
        {
            StartStory();
        }
        else if (story != null)
        {
            if (story.canContinue)
            {
                RefreshView();
            }
            else if (story.currentChoices.Count > 0)
            {
                RefreshView();
            } // If we've read all the content and there's no choices, the story is finished!
            else
            {

            }
        }
    }
    public void HideText()
    {
        m_dialogText[currentSpeaker].text = "";
    }
    private void Update()
    {
        if (currentSpeaker != null)
        {
            // Force and update of the mesh to get valid information.
            m_dialogText[currentSpeaker].ForceMeshUpdate();

            int totalVisibleCharacters = m_dialogText[currentSpeaker].textInfo.characterCount; // Get # of Visible Character in text object
                                                                                               //int counter = 0;
            int visibleCount = 0;
            visibleCount = (int)(totalVisibleCharacters * RevealPercentage);

            m_dialogText[currentSpeaker].maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            // Once the last character has been revealed, wait 1.0 second and start over.
            if (visibleCount < totalVisibleCharacters)
            {
                RevealPercentage += (m_charactersPerSecond / totalVisibleCharacters) * Time.deltaTime;
                RevealPercentage = Mathf.Clamp(RevealPercentage, 0, 1);
            }
        }   
    }
    [SerializeField] float m_charactersPerSecond;
    float RevealPercentage = 0.0f;

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        m_dialogText[currentSpeaker].maxVisibleCharacters = 0;
        RevealPercentage = 0;


        // Read all the content until we can't continue any more
        if (story.canContinue)
        {
            // Continue gets the next line of the story
            string rawText = story.Continue().Trim();

            m_dialogText[currentSpeaker].text = "";
            // This removes any white space from the text.
            string parsedText = ParseContent(rawText); //This also changes the actualspeaker
            m_dialogText[currentSpeaker].text = parsedText;
        }

        // Display all the choices, if there are any!
        else if (story.currentChoices.Count > 0)
        {
           
        }
    }
    public string ParseContent(string rawContent)
    {
        string subjectID = "";
        string content = "";
        if (!TrySplitContentBySearchString(rawContent, ": ", ref subjectID, ref content)) return rawContent;
        ChangeSpeaker(subjectID);
        return content;
    }
    public bool TrySplitContentBySearchString(string rawContent, string searchString, ref string left, ref string right)
    {
        int firstSpecialCharacterIndex = rawContent.IndexOf(searchString);
        if (firstSpecialCharacterIndex == -1) return false;
        left = rawContent.Substring(0, firstSpecialCharacterIndex).Trim();
        right = rawContent.Substring(firstSpecialCharacterIndex + searchString.Length, rawContent.Length - firstSpecialCharacterIndex - searchString.Length).Trim();
        return true;
    }
    void ChangeSpeaker(string speaker)
    {
        currentSpeaker = speaker;
    }
    public void AddDialogText(string name, TextMeshProUGUI dialogText)
    {

        m_dialogText.Add(name, dialogText);
    }
}
