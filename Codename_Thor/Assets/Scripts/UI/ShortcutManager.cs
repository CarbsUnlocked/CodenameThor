using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    public TextMeshProUGUI shortcutPrompt;
    public PlayerMove playerMove;  // Reference to the PlayerController
    public TextMeshProUGUI wrongShortcutText;  // Reference to the Wrong Shortcut Text UI
    public AudioSource playerCorrectShortcutAudio;

    private string[] shortcutNames = { "CopyShortcut", "PasteShortcut", "SaveShortcut", "UndoShortcut", "CutLineShortcut", "GoToAllShortcut", "CommentSelectedShortcut" };
    private string currentShortcutName;

    private List<string> usedShortcuts = new List<string>();

    void Start()
    {
        SetRandomShortcut();
        wrongShortcutText.text = "";  // Initialize with no text
    }

    void SetRandomShortcut()
    {
        // Check if all shortcuts have been used
        if (usedShortcuts.Count >= shortcutNames.Length)
        {
            // Reset the usedShortcuts list or handle as needed
            Debug.Log("All shortcuts have been used. Resetting...");
            usedShortcuts.Clear();  // Clear the list to allow the same shortcuts again

            // Optionally, you could shuffle the shortcuts or end the game
            // return;  // If you want to stop adding shortcuts, uncomment this line
        }

        int safetyCounter = 0;
        const int maxSafetyCounter = 100; // To prevent infinite loops

        // Select a random shortcut that hasn't been used yet
        do
        {
            if (safetyCounter++ > maxSafetyCounter)
            {
                Debug.LogError("SetRandomShortcut() is stuck in an infinite loop!");
                return;
            }

            int randomIndex = Random.Range(0, shortcutNames.Length);
            currentShortcutName = shortcutNames[randomIndex];

        } while (usedShortcuts.Contains(currentShortcutName));

        // Display the name of the shortcut
        shortcutPrompt.text = "Shortcut: " + currentShortcutName.Replace("Shortcut", "");
        Debug.Log("Shortcut: " + currentShortcutName);
    }

    void Update()
    {
        if (Application.isFocused)
        {
            CheckShortcutInput();
        }
    }

    void CheckShortcutInput()
    {
        // Check if the player pressed the correct keys using Input Manager
        if (Input.GetButtonDown(currentShortcutName))
        {
            OnCorrectShortcut();
        }
    }

    void OnCorrectShortcut()
    {
        Debug.Log("Correct Shortcut: " + currentShortcutName);
        playerCorrectShortcutAudio.Play();
        playerMove.Jump();  // Make the player jump

        usedShortcuts.Add(currentShortcutName);  // Add to used shortcuts list

        wrongShortcutText.text = "";  // Clear any previous wrong message

        SetRandomShortcut();
    }

    void OnWrongShortcut()
    {
        Debug.Log("Wrong Shortcut!");
        playerMove.LoseLife();

        wrongShortcutText.text = "Wrong Shortcut!";  // Show wrong shortcut message

        // Optionally, you can clear the message after a short delay
        Invoke("ClearWrongShortcutText", 2f);  // Clear after 2 seconds
    }

    void ClearWrongShortcutText()
    {
        wrongShortcutText.text = "";  // Clear the text
    }
}
