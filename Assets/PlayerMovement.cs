using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;
    private Animator playerAnim;
    public InputActionReference moveAction;

    private Vector2 moveInput;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
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

        // Animation
        if (moveInput != Vector2.zero)
        {
            // TRUE = Player is walking
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            // FALSE = Player is NOT walking
            playerAnim.SetBool("isWalking", false);
        }
    }
}