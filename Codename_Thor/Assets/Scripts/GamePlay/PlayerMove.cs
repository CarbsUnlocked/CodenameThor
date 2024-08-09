using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves forward
    public float jumpForce = 5f;  // Force applied when the player jumps
    public int lives = 3;         // Number of lives the player starts with

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;  // Optional: Adjust gravity for better jump behavior
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
        }
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0;  // Pause the game
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground
        // This can be done by checking if the player's collider is in contact with the ground
        return rb.velocity.y == 0;
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Obstacle"))
    //     {
    //         // Handle game over logic
    //         Time.timeScale = 0;
    //     }
    // }

}
