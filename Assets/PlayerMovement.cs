using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    public float attackCooldown = 0.8f;

    private Rigidbody rb;
    private Animator playerAnim;
    private Vector2 moveInput;

    private bool canAttack = true;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Left Mouse Button
    public void OnAttack1(InputValue value)
    {
        if (!value.isPressed || !canAttack)
            return;

        Debug.Log("Attack1 Pressed");

        playerAnim.SetTrigger("Attack1");
        StartCoroutine(Cooldown());
    }

    // Right Mouse Button
    public void OnAttack2(InputValue value)
    {
        if (!value.isPressed || !canAttack)
            return;

        Debug.Log("Attack2 Pressed");

        playerAnim.SetTrigger("Attack2");
        StartCoroutine(Cooldown());
    }

    // Spacebar
    public void OnAttack3(InputValue value)
    {
        if (!value.isPressed || !canAttack)
            return;

        Debug.Log("Attack3 Pressed");

        playerAnim.SetTrigger("Attack3");
        StartCoroutine(Cooldown());
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
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }

    private IEnumerator Cooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}