using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 15;
    public float jumpSpeed = 7;

    private Rigidbody rb;
    private Collider col;

    private float movementX;
    private float movementZ;
    private float distanceToGround;

    private bool isJumping;
    private bool isDashing;

    private float lastDashTime;

    private float DASH_INTERVAL = 2f;
    private float DESTROY_SPEED = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        distanceToGround = col.bounds.size.y / 2;
        isJumping = false;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump()
    {
        if (IsGrounded())
        {
            isJumping = true;
        }
    }

    void OnDash()
    {
        if (Time.time - lastDashTime >= DASH_INTERVAL)
        {
            isDashing = true;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isJumping = false;
        }

        if (IsGrounded())
        {
            if (isDashing)
            {
                Vector3 movementVector = new Vector3(movementX, 0f, movementZ);
                rb.AddForce(movementVector * speed, ForceMode.Impulse);
                lastDashTime = Time.time;
                isDashing = false;
            }
            else
            {
                Vector3 movementVector = new Vector3(movementX, 0f, movementZ);
                rb.AddForce(movementVector * speed);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.magnitude >= DESTROY_SPEED)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
