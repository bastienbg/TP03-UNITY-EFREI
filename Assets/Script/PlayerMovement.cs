using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 7f;  
    private Rigidbody rb;         
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();

        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
        }

        
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("Attack");
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;  
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

