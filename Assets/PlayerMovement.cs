using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // Forward/back movement
        Vector3 movement = transform.forward * moveInput.y;

        rb.MovePosition(
            rb.position + movement * moveSpeed * Time.fixedDeltaTime
        );

        // Left/right rotation
        float turn = moveInput.x * rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0f, turn, 0f)
        );
    }
}