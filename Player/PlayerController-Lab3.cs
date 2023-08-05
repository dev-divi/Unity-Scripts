using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//there was some official documentation that said that 
// i should be using fixedupdate method for rigidbody 

public class PlayerControllerLab3 : MonoBehaviour
{
    public float floatForce = 50;//jump float
    public float levitation = 0; 
    private float gravityModifier = 1.5f; //jump, moon gravity basically 

    public float speed = 10.0f; 
    public bool isOnGround = true; 

    private Rigidbody playerRb; //define component 

    //animation
    private Animator animator; 

    // rotation code 
    public float rotationSpeed = 10; 

    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>(); //get component 
        Physics.gravity *= gravityModifier;

        m_EulerAngleVelocity = new Vector3(0, 100, 0);

    }

    // Update is called once per frame
    void Update()
    {
    
        //PLAYER BOUNDARIES 
        //up down zeta 
        if (transform.position.z >= 70f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -35f);
        }
        else if (transform.position.z <= -35f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 70f);
        }
        //horizontal
        if (transform.position.x >= 60f) {
            transform.position = new Vector3(-60f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -60f)
        {
            transform.position = new Vector3(60f, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= 60f) {
            transform.position = new Vector3(transform.position.x, 13, transform.position.z);
        }
        else if (transform.position.x <= 12)
        {
            transform.position = new Vector3(transform.position.x, 13, transform.position.z);
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical"); 
        
        //playerRb.AddForce(Vector3.forward * speed * verticalInput);      //this is going up on the z axis, not actually ascending on Y 
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);
          
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput); //testing 
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed; 
        movementDirection.Normalize(); 

        //ySpeed += Physics.gravity.y * Time.deltaTime; 

        playerRb.AddForce(speed * movementDirection); //huh... this worked.. 
        //what would happen if i multiplied by magnitude instead of speed. 

        if (Input.GetKey(KeyCode.Space)) {
            playerRb.AddForce(Vector3.up * floatForce);
            isOnGround = false; 
        }
        if(movementDirection != Vector3.zero) {
            animator.SetBool("IsRunning", true);
            
            //Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); 

            //rigidbody rotation code from unity docs 
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
        else {
            animator.SetBool("IsRunning", false);
        }


       
    }

    //mayb3e i can 
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * levitation, ForceMode.Impulse);
        }

    }
}
