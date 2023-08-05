using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBrackeys : MonoBehaviour
{

    //Variables 
    [SerializeField] private float speed;
    [SerializeField] public float jumpForce;
    private Vector3 playerMovementInput;
    // LayerMasks
    [SerializeField] public LayerMask FloorMask;
    //Components
    [SerializeField] private Rigidbody playerBody;
    public Transform cam;
    [SerializeField] public Transform feetTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Lock the Cursor and make it invisible (when the user clicks)
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        // Get the raw, normalized input for the player
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        //Move the player
        MovePlayer();

    }
    private void MovePlayer()
    {
        // Get the movement Vector ignoring camera rotation
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput) * speed;
        // Rotate player along with camera rotation
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        // Add the forces needed to move the player
        playerBody.AddForce(MoveVector.x, playerBody.velocity.y, MoveVector.z);

        // Jump script
        if (Input.GetKeyDown(KeyCode.Space))
            {
            // Is the player on the ground?
            if (Physics.CheckSphere(feetTransform.position, 0.1f, FloorMask))
            {
                // Jump
                playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        } 
    }

} 