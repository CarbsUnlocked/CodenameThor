using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public LogicScript logic;             // Reference to the LogicScript (for game over handling)
    public PlayerMove playerMove;         // Reference to the PlayerMove script
    public GameObject deathUIScreen;      // Reference to the death UI screen GameObject
    public AudioSource playerHitAudio;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerMove = GetComponent<PlayerMove>();  // Get the PlayerMove component attached to the player
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            playerHitAudio.Play();
            playerMove.LoseLife();  // Call LoseLife() from the PlayerMove script

            if (playerMove.lives <= 0)
            {
                ShowDeathUI();  // Show the death UI screen
            }
        }
    }

    void ShowDeathUI()
    {
        // Pause the game and show the death UI screen
        Time.timeScale = 0f;  // Pause the game
        deathUIScreen.SetActive(true);  // Activate the death UI screen
    }
}
