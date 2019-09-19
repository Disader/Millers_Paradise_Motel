using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] ScriptableDialog m_scriptableDialog;
    private List<GameObject> activeButtons = new List<GameObject>();
    private Story story;
    [SerializeField] GameObject m_buttonPanel;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] TextMeshProUGUI m_dialogText;
    [SerializeField] TextMeshProUGUI m_nameText;
    [SerializeField] GameObject m_canvas;
    [SerializeField] Image m_playerPortrait;
    [SerializeField] Image m_NPCPortrait;
    Image currentSpeaker;

    public List<GameObject> objectsChangedAtEnd = new List<GameObject>();
    Dictionary<string,ScriptableCharacter> m_participantsList = new Dictionary<string, ScriptableCharacter>();

    private void Awake()
    {
        ScriptableCharacter[] characters = Resources.LoadAll<ScriptableCharacter>(AppPaths.PATH_RESOURCE_CHARACTERS);

        for (int i = 0; i < characters.Length; i++)
        {
            m_participantsList.Add(characters[i].characterName, characters[i]);
        }
    }
    //Detección del input para abrir el panel de diálogos
    bool m_playerIsInside;
    private void Update()
    {
        if (m_playerIsInside && InputManager.Instance.InteractionKey)
        {
            ShowPanel();
           
            PuzzleManager.Instance.CheckObject(m_scriptableDialog.ID);
            
        }
        DialogUpdate();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            m_playerIsInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>() != null)
        {
            m_playerIsInside = false;
        }
    }
    void HidePanel()
    {
        m_canvas.SetActive(false);
        InputManager.Instance.ResumeMovement();
        InputManager.Instance.ResumeInteraction();
        RemoveDialogButtons();
        UIManager.Instance.ResumeInventory();
        story = null;
        if (objectsChangedAtEnd != null)
        {
            foreach (GameObject actualObj in objectsChangedAtEnd)
            {
                if (actualObj != null)
                {
                    actualObj.SetActive(!actualObj.activeSelf);
                }
            }
        }
    }
    void ShowPanel()
    {
        m_canvas.SetActive(true);
        InputManager.Instance.PauseMovement();
        InputManager.Instance.PauseInteraction();
        StartStory();
        UIManager.Instance.PauseInventory();
    }
    
    //Inkle
    //---------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------
    // Creates a new Story object with the compiled story which we can then play!
    int m_dialogNumber;
    bool waitingOnInput;
    public void NextDialog()
    {
        if (m_scriptableDialog.dialogs.Count -1> m_dialogNumber)
        {
            m_dialogNumber++;
        }
        else
        {
            Debug.Log("No more Dialogs");
        }
    }
    void StartStory()
    {
        InitSpeakers();
        story = new Story(m_scriptableDialog.dialogs[m_dialogNumber].jsonAsset.text);
        RefreshView();
    }
    void DialogUpdate()
    {
        if (story != null)
        {
            if(!waitingOnInput && Input.GetMouseButtonDown(0) && RevealPercentage != 1)
            {
                RevealPercentage = 1;
            }else if (!waitingOnInput && Input.GetMouseButtonDown(0))
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
                    //Dar el objeto en caso de que termine el diálogo y este no sea nulo
                    if (m_scriptableDialog.dialogs[m_dialogNumber].objectGivenAtEnd != null)
                    {
                        PuzzleManager.Instance.CheckObject(m_scriptableDialog.dialogs[m_dialogNumber].objectGivenAtEnd.ID);
                        InventoryUI.Instance.AddItem(m_scriptableDialog.dialogs[m_dialogNumber].objectGivenAtEnd);
                    }
                    if (m_scriptableDialog.dialogs[m_dialogNumber].newDialogAtEnd)
                    {
                        NextDialog();
                    }
                    HidePanel();
                }
            }


            // Force and update of the mesh to get valid information.
            m_dialogText.ForceMeshUpdate();

            int totalVisibleCharacters = m_dialogText.textInfo.characterCount; // Get # of Visible Character in text object
                                                                               //int counter = 0;
            int visibleCount = 0;
            visibleCount = (int)(totalVisibleCharacters * RevealPercentage);

            m_dialogText.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

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
        //Reset character appear variables
        m_dialogText.maxVisibleCharacters = 0;
        RevealPercentage = 0;


        // Read all the content until we can't continue any more
        if (story.canContinue)
        {
            RemoveDialogButtons();
            // Continue gets the next line of the story
            string rawText = story.Continue().Trim();
            // This removes any white space from the text.
            m_dialogText.text = ParseContent(rawText);
        }

        // Display all the choices, if there are any!
        else if (story.currentChoices.Count > 0)
        {
            ChangeSpeaker("SARA");
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate {
                    OnClickChoiceButton(choice);
                });
            }
            waitingOnInput = true;
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
    void InitSpeakers()
    {
        story = new Story(m_scriptableDialog.dialogs[m_dialogNumber].jsonAsset.text);

        // Continue gets the next line of the story
        string rawText = story.Continue().Trim();
        string parsedText = ParseContent(rawText); //This also changes the actualspeakery

    }
    void ChangeSpeaker(string speaker)
    {
        if (currentSpeaker != null)
        {
            currentSpeaker.gameObject.SetActive(false);
        }
    
        if (m_participantsList.ContainsKey(speaker) && speaker == m_participantsList[speaker].characterName)
        {
            currentSpeaker = m_NPCPortrait;
            m_NPCPortrait.sprite = m_participantsList[speaker].icon;
            m_nameText.text = m_participantsList[speaker].characterName;
        }
        else
        {
            currentSpeaker = m_playerPortrait;
            m_nameText.text = "SARA";
        }
            
        currentSpeaker.gameObject.SetActive(true);
    }
    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        waitingOnInput = false;
        RefreshView();
    }


    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(m_buttonPanel.transform, false);
        activeButtons.Add(choice.gameObject);

        // Gets the text from the button prefab
        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }
    // Destroys dialog buttons
    void RemoveDialogButtons()
    {
        foreach (GameObject actualButton in activeButtons)
        {
            Destroy(actualButton);
        }
        activeButtons.Clear();
    }
}
