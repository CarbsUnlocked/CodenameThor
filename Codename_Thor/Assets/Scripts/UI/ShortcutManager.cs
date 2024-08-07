using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    public TextMeshProUGUI shortcutPrompt;
    public PlayerMove playerMove;  // Reference to the PlayerController
    private string[] shortcuts = { "Ctrl+C", "Ctrl+V", "Ctrl+S", "Ctrl+Z" };
    private string currentShortcut;

    void Start()
    {
        SetRandomShortcut();
    }

    void SetRandomShortcut()
    {
        currentShortcut = shortcuts[Random.Range(0, shortcuts.Length)];
        shortcutPrompt.text = "Press: " + currentShortcut;
        Debug.Log("Current Shortcut: " + currentShortcut);
    }

    void Update()
    {
        CheckShortcutInput();
    }

    void CheckShortcutInput()
    {
        switch (currentShortcut)
        {
            case "Ctrl+C":
                if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                {
                    OnCorrectShortcut();
                }
                break;
            case "Ctrl+V":
                if (Input.GetKeyDown(KeyCode.V) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                {
                    OnCorrectShortcut();
                }
                break;
            case "Ctrl+S":
                if (Input.GetKeyDown(KeyCode.S) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                {
                    OnCorrectShortcut();
                }
                break;
            case "Ctrl+Z":
                if (Input.GetKeyDown(KeyCode.Z) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
                {
                    OnCorrectShortcut();
                }
                break;
        }
    }

    void OnCorrectShortcut()
    {
        Debug.Log("Correct Shortcut: " + currentShortcut);
        playerMove.Jump();
        SetRandomShortcut();
    }
}
