using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input=UnityEngine.Input; 
using UnityEngine.Windows;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float JumpTime = 0.3f;
    [SerializeField] private TextMeshProUGUI shortcutText;
    
    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    // Define the shortcuts and their corresponding key combinations
    private Dictionary<string, KeyCode[]> shortcuts = new Dictionary<string, KeyCode[]>
    {
        { "Go to All", new KeyCode[] { KeyCode.LeftControl, KeyCode.B } },
        { "Copy", new KeyCode[] { KeyCode.LeftControl, KeyCode.C } },
        { "Paste", new KeyCode[] { KeyCode.LeftControl, KeyCode.V } }
        // Add more shortcuts as needed
    };

    private string currentShortcutName;
    private KeyCode[] currentShortcutKeys;


    private void Update() {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        if(isGrounded && Input.GetButtonDown("Jump")){
           isJumping = true;
           rb.velocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetButtonDown("Jump")){
            if(jumpTimer < JumpTime){
                rb.velocity = Vector2.up * jumpForce;

                jumpTimer += Time.deltaTime;
            }else{
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")){
            isJumping = false;
            jumpTimer = 0;
        }

         // Check if the correct shortcut is pressed
        if (IsCorrectShortcutPressed())
        {
            Jump();
            SetRandomShortcut(); // Set a new shortcut after a successful jump
        }
    }

     private bool IsCorrectShortcutPressed()
    {
        if (currentShortcutKeys == null)
        {
            Debug.LogWarning("currentShortcutKeys is null. No shortcut set.");
            return false;
        }

        foreach (var key in currentShortcutKeys)
        {
            if (!Input.GetKey(key))
            {
                Debug.Log($"Key {key} not pressed. Shortcut not activated.");
                return false;
            }
            else
            {
                Debug.Log($"Key {key} pressed.");
            }
        }

        Debug.Log("Correct shortcut pressed. Jumping!");
        return true;
    }

     private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        Debug.Log("Jump triggered by shortcut: " + currentShortcutName);
    }

    public void SetRandomShortcut()
    {
        if (shortcuts.Count == 0)
        {
            Debug.LogWarning("No shortcuts available to set.");
            return;
        }

        int index = Random.Range(0, shortcuts.Count);
        currentShortcutName = new List<string>(shortcuts.Keys)[index];
        currentShortcutKeys = shortcuts[currentShortcutName];

        if (currentShortcutKeys != null)
        {
            shortcutText.text = "Shortcut: " + currentShortcutName;
            Debug.Log("New shortcut set: " + currentShortcutName);
        }
        else
        {
            Debug.LogError("Failed to set shortcut keys. currentShortcutKeys is null.");
        }
    }

}
