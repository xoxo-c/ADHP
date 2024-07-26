using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool _doublejump = false;
    
    public float _doublejumpMult = 0.5f; 

    Vector3 velocity;
    bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        #endregion

        #region Player Controller
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //jump
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            _doublejump = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } else
        {
            if(Input.GetButtonDown("Jump") && _doublejump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity) * _doublejumpMult;
                _doublejump = false;
            }
        }
        #endregion

        #region Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        #endregion
    }
}
