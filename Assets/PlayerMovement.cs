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
    public InputActionReference moveAction;
    public InputActionReference attack1Action;
    public InputActionReference attack2Action;
    public InputActionReference attack3Action;

    private Vector2 moveInput;


    private bool canAttack = true;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();

        if (attack1Action != null)
            attack1Action.action.Enable();

        if (attack2Action != null)
            attack2Action.action.Enable();

        if (attack3Action != null)
            attack3Action.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();

        attack1Action.action.Disable();
        attack2Action.action.Disable();
        attack3Action.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        if (attack1Action.action.WasPressedThisFrame())
        {
            Attack1();
        }

        if (attack2Action.action.WasPressedThisFrame())
        {
            Attack2();
        }

        if (attack3Action.action.WasPressedThisFrame())
        {
            Attack3();
        }
    }

    // Left Mouse Button
    private void Attack1()
    {
        if (!canAttack)
            return;

        Debug.Log("Attack1 Pressed");

        playerAnim.SetTrigger("Attack1");
        StartCoroutine(Cooldown());
    }

    private void Attack2()
    {
        if (!canAttack)
            return;

        Debug.Log("Attack2 Pressed");

        playerAnim.SetTrigger("Attack2");
        StartCoroutine(Cooldown());
    }

    private void Attack3()
    {
        if (!canAttack)
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