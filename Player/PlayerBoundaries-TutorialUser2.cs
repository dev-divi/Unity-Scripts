using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//from that same person on Github 

public class PlayerBoundariesAdvanced2 : MonoBehaviour
{
    private Rigidbody playerRb;

    public float moveSpeed = 2f;
    public float maxSpeed = 6f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();  
    }

    void Update()
    {
        MovePlayer();
    }

    // Moves the player based on arrow key input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Limit the velocity of the player
        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }

        // When user presses two axis at the same time reduce the speed
        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
        {
            moveSpeed = 1.5f;
            playerRb.AddForce(Vector3.forward * moveSpeed * verticalInput);
            playerRb.AddForce(Vector3.right * moveSpeed * horizontalInput);
        }
        else
        {
            moveSpeed = 2f;
            playerRb.AddForce(Vector3.forward * moveSpeed * verticalInput);
            playerRb.AddForce(Vector3.right * moveSpeed * horizontalInput);
        }
    }

}