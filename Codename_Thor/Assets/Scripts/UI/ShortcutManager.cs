using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    public TextMeshProUGUI shortcutPrompt;
    public PlayerMove playerMove;  // Reference to the PlayerController
// Dictionary of shortcut names and their corresponding key combinations
    private Dictionary<string, KeyCode[]> shortcuts = new Dictionary<string, KeyCode[]>
    {
        { "Copy", new KeyCode[] { KeyCode.LeftControl, KeyCode.C } },
        { "Paste", new KeyCode[] { KeyCode.LeftControl, KeyCode.V } },
        { "Save", new KeyCode[] { KeyCode.LeftControl, KeyCode.S } },
        { "Undo", new KeyCode[] { KeyCode.LeftControl, KeyCode.Z } },
        { "Go to All", new KeyCode[] { KeyCode.LeftControl, KeyCode.B } }
    };

    private string currentShortcutName;
    private KeyCode[] currentShortcutKeys;

    void Start()
    {
        SetRandomShortcut();
    }

    void SetRandomShortcut()
    {
        // Select a random shortcut from the dictionary
        int randomIndex = Random.Range(0, shortcuts.Count);
        currentShortcutName = new List<string>(shortcuts.Keys)[randomIndex];
        currentShortcutKeys = shortcuts[currentShortcutName];
        
        // Display the name of the shortcut
        shortcutPrompt.text = "Shortcut: " + currentShortcutName;
        Debug.Log("Shortcut: " + currentShortcutName + " | Keys: " + string.Join(" + ", currentShortcutKeys));
    }

    void Update()
    {
        CheckShortcutInput();
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
    }
}
