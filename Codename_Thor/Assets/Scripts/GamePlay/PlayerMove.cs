using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves forward
    public float jumpForce = 5f;  // Force applied when the player jumps
    public int lives = 3;         // Number of lives the player starts with
    public Image[] lifeImages;    // Array of Image components representing lives
    // public Animator animator;     // Reference to the Animator component

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;  // Optional: Adjust gravity for better jump behavior

        UpdateLivesUI();  // Initialize the lives UI
    }

    void Update()
    {
        // Keep the player moving forward
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        // Apply a vertical force to make the player jump
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Trigger the jump animation
            // animator.SetTrigger("JumpTrigger");
        }
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives remaining: " + lives);
        UpdateLivesUI();  // Update the UI after losing a life

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        // Update the sprite visibility based on remaining lives
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lives)
            {
                lifeImages[i].enabled = true;  // Show the life icon
            }
            else
            {
                lifeImages[i].enabled = false; // Hide the life icon
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;  // Pause the game
    }

    bool IsGrounded()
    {
        // Cast a ray downwards from the player's position to check if the player is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        // Return true if the ray hit something (i.e., the ground)
        return hit.collider != null;
    }
}
