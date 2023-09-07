using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 5f;

    [SerializeField] public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] public Camera cam;
    Vector2 movement;
    private Vector2 lastMovement = Vector2.zero;
    // Update is called once per frame
    void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        bool isMoving = (movement.x != 0f || movement.y != 0f);
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
            {
                lastMovement = movement.normalized; // Normalize for direction
            }
    }  

    private void SetIdleDirection()
    {
        float idleDirectionFloat = 0.0f; // Default to "None"
    
        if (lastMovement.x > 0)
            {
                idleDirectionFloat = 2.0f; // Right
                Debug.Log("Right");
            }
        else if (lastMovement.x < 0)
            {
                idleDirectionFloat = 1.0f; // Left
                Debug.Log("Left");
            }
        else if (lastMovement.y > 0)
            {
                idleDirectionFloat = 3.0f; // Up
                Debug.Log("Up");
            }
        else if (lastMovement.y < 0)
            {
                idleDirectionFloat = 4.0f; // Down
                Debug.Log("Down");
            }

        // Set the "IdleDirectionFloat" parameter in the Animator
        animator.SetFloat("idleDirectionFloat", idleDirectionFloat);
    }

    void FixedUpdate()
    {
         //movement
         rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

         SetIdleDirection();
    }
}
