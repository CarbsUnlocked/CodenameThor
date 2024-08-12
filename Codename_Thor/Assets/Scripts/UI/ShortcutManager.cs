using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    public TextMeshProUGUI shortcutPrompt;
    public GameObject winMenu;  // Reference to the Win Menu UI
    public TextMeshProUGUI wrongShortcutText;  // Reference to the Wrong Shortcut Text UI
    public PlayerMove playerMove;  // Reference to the PlayerController
// Dictionary of shortcut names and their corresponding key combinations
    private Dictionary<string, KeyCode[]> shortcuts = new Dictionary<string, KeyCode[]>
    {
        { "Copy", new KeyCode[] { KeyCode.LeftControl, KeyCode.C } },
        { "Paste", new KeyCode[] { KeyCode.LeftControl, KeyCode.V } },
        { "Save", new KeyCode[] { KeyCode.LeftControl, KeyCode.S } },
        { "Undo", new KeyCode[] { KeyCode.LeftControl, KeyCode.Z } },
        
    };

    private string currentShortcutName;
    private KeyCode[] currentShortcutKeys;
    private List<string> usedShortcuts = new List<string>();

    void Start()
    {
        SetRandomShortcut();
    }

    void SetRandomShortcut()
    {
         // Check if all shortcuts have been used
        if (usedShortcuts.Count >= shortcuts.Count)
        {
            WinGame();
            return;
        }

        // Select a random shortcut that hasn't been used yet
        do
        {
            int randomIndex = Random.Range(0, shortcuts.Count);
            currentShortcutName = new List<string>(shortcuts.Keys)[randomIndex];
        } while (usedShortcuts.Contains(currentShortcutName));

        currentShortcutKeys = shortcuts[currentShortcutName];

        // Display the name of the shortcut
        shortcutPrompt.text = "Shortcut: " + currentShortcutName;
        Debug.Log("Shortcut: " + currentShortcutName + " | Keys: " + string.Join(" + ", currentShortcutKeys));
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
        // Check if the player pressed the correct keys
        if (currentShortcutKeys.Length == 2 &&
            Input.GetKey(currentShortcutKeys[0]) &&
            Input.GetKeyDown(currentShortcutKeys[1]))
        {
            OnCorrectShortcut();
        }
        else if (Input.anyKeyDown)
        {
            OnWrongShortcut();
        }
    }

    void OnCorrectShortcut()
    {
        Debug.Log("Correct Shortcut: " + currentShortcutName);
        playerMove.Jump();  // Make the player jump
        SetRandomShortcut();
    }

    void OnWrongShortcut()
    {
        Debug.Log("Wrong Shortcut!");
        playerMove.LoseLife();

        wrongShortcutText.text = "Wrong Shortcut!";  // Show wrong shortcut message

        
        Invoke("ClearWrongShortcutText", 2f);  // Clear after 2 seconds
    }

    void ClearWrongShortcutText()
    {
        wrongShortcutText.text = "";  // Clear the text
    }

     void WinGame()
    {
        Debug.Log("You Win!");
        Time.timeScale = 0;  // Pause the game
        winMenu.SetActive(true);  // Show the win menu
    }
}
