using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hello everyone!

//--Introduction
//With the tutorial code, there are some little but impactful problems, which are;
//1. If you push horizontal and vertical keys simultaneously, the speed value increases fast as twice! Which leads to fast acceleration when going diagonal than only horizontally or vertically!
//. If you don't use invisible walls but transform the position, 
//the velocity should be zeroed by the player by going in another direction for some time!
// Instead, when you hit a wall, your momentum should be zeroed immediately.
// That's why I didn't use the transform method.
// It's better to put two more walls and untick the Mesh Renderer component to make them invisible.

//--Solution:
//First, we need to know what is velocity and the Clamp method.
//playerRb.Velocity is the player's momentum based on Vector3(x, y, z). So every direction has its velocity. 
//When the game is in play mode, click Player, and you can track the Velocity under the Rigidbody component for better understanding.

//playerRb.velocity.magnitude is a way to set (x, y, z) at once, all together. It returns the length of the velocity.

//ClampMagnitude method, just as it says. For example, If it goes more than 6, clamp it to the float value we want.

//Here I am sharing my code which works well enough. If any questions, leave a comment. You can download my project from GitHub if you want to: https://github.com/ArdaIskender/Lab-3-Player-Control
//And special thanks for the explanation to https://www.youtube.com/watch?v=7NMsVub5NZM

//--Code


public class PlayerBoundariesAdvanced : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxSpeed = 6f;
    private Rigidbody playerRb;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        } 

//if the user presses both axis at the same time, reduce the speed 
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