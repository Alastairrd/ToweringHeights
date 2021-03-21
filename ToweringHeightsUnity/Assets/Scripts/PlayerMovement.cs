using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float inputX;   
    //public float inputZ;
    public float moveSpeed = 5f;
    public CharacterController controller;
    public float gravity = -12f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    


    // Update is called once per frame
    void Update()
    {
        //Checks whether grounded / stabilises y velocity.
        isGrounded = (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Movement input stored
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        //Player movement stored as a vector3 and then smoothed
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = 8f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = 5f;
        }
            Vector3 move = transform.right * inputX + transform.forward * inputZ;
            controller.Move(move * moveSpeed * Time.deltaTime);
        

        //Jumping, checks if grounded and then smooths
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Constant vertical movement based on gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }
}
