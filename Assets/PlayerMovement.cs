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

    public AudioSource musicSource;
    public AudioSource footstepSource;
    public AudioSource sfxSource;

    public AudioClip walkClip;
    public AudioClip fireballClip;
    public AudioClip iceballClip;
    public AudioClip windballClip;

    public GameObject fireballPrefab;
    public GameObject iceballPrefab;
    public GameObject windballPrefab;

    public Transform spellSpawnPoint;

    void PlayFootsteps()
    {
        if (footstepSource.clip != walkClip)
        {
            footstepSource.clip = walkClip;
            footstepSource.loop = true;
        }

        if (!footstepSource.isPlaying)
            footstepSource.Play();
    }

    void StopFootsteps()
    {
        footstepSource.Stop();
    }
    void SpawnFireball()
    {
        if (fireballPrefab != null && spellSpawnPoint != null)
        {
            Instantiate(
                fireballPrefab,
                spellSpawnPoint.position,
                spellSpawnPoint.rotation
            );
        }
    }
    void SpawnIceball()
    {
        Instantiate(
            iceballPrefab,
            spellSpawnPoint.position,
            spellSpawnPoint.rotation
        );
    }

    void SpawnWindball()
    {
        Instantiate(
            windballPrefab,
            spellSpawnPoint.position,
            spellSpawnPoint.rotation
        );
    }
    void CastSpell1()
    {
        Debug.Log("CastSpell called");

        if (sfxSource == null)
        {
            Debug.LogError("No SFX AudioSource assigned!");
            return;
        }

        if (fireballClip == null)
        {
            Debug.LogError("No fireball clip assigned!");
            return;
        }

        Debug.Log("Playing: " + fireballClip.name);

        AudioSource.PlayClipAtPoint(fireballClip, transform.position);
    }

    void CastSpell2()
    {
        Debug.Log("CastSpell called");

        if (sfxSource == null)
        {
            Debug.LogError("No SFX AudioSource assigned!");
            return;
        }

        if (fireballClip == null)
        {
            Debug.LogError("No iceball clip assigned!");
            return;
        }

        Debug.Log("Playing: " + iceballClip.name);

        AudioSource.PlayClipAtPoint(iceballClip, transform.position);
    }

    void CastSpell3()
    {
        Debug.Log("CastSpell called");

        if (sfxSource == null)
        {
            Debug.LogError("No SFX AudioSource assigned!");
            return;
        }

        if (windballClip == null)
        {
            Debug.LogError("No iceball clip assigned!");
            return;
        }

        Debug.Log("Playing: " + iceballClip.name);

        AudioSource.PlayClipAtPoint(windballClip, transform.position);
    }
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

    public void PlayHitAnimation()
    {
        playerAnim.SetTrigger("Hit");
    }

    private void OnDisable()
    {
        moveAction.action.Disable();

        if (attack1Action != null)
            attack1Action.action.Disable();

        if (attack2Action != null)
            attack2Action.action.Disable();

        if (attack3Action != null)
            attack3Action.action.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude > 0.01f)
            PlayFootsteps();
        else
            StopFootsteps();

        if (attack1Action.action.WasPressedThisFrame())
            Attack1();

        if (attack2Action.action.WasPressedThisFrame())
            Attack2();

        if (attack3Action.action.WasPressedThisFrame())
            Attack3();
    }

    // Left Mouse Button
    private void Attack1()
    {
        if (!canAttack)
            return;

        Debug.Log("Attack1 Pressed");

        CastSpell1();
        SpawnFireball();

        playerAnim.SetTrigger("Attack1");

        StartCoroutine(Cooldown());
    }

    // Right Mouse Button
    private void Attack2()
    {
        if (!canAttack)
            return;

        Debug.Log("Attack2 Pressed");


        CastSpell2();
        SpawnIceball();
        playerAnim.SetTrigger("Attack2");
        StartCoroutine(Cooldown());
    }

    // Spacebar
    private void Attack3()
    {
        if (!canAttack)
            return;

        Debug.Log("Attack3 Pressed");

        CastSpell3();
        SpawnWindball();
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

        UpdateMovementAnimations();
    }

    private void UpdateMovementAnimations()
    {
        // Reset all movement animation bools
        playerAnim.SetBool("forwardWalk", false);
        playerAnim.SetBool("forwardRight", false);
        playerAnim.SetBool("forwardLeft", false);
        playerAnim.SetBool("right", false);
        playerAnim.SetBool("left", false);
        playerAnim.SetBool("backwardWalk", false);
        playerAnim.SetBool("backwardRight", false);
        playerAnim.SetBool("backwardLeft", false);

        // Deadzone to prevent jitter
        float deadzone = 0.1f;

        bool forward = moveInput.y > deadzone;
        bool backward = moveInput.y < -deadzone;
        bool right = moveInput.x > deadzone;
        bool left = moveInput.x < -deadzone;

        if (forward)
        {
            if (right)
                playerAnim.SetBool("forwardRight", true);
            else if (left)
                playerAnim.SetBool("forwardLeft", true);
            else
                playerAnim.SetBool("forwardWalk", true);
        }
        else if (backward)
        {
            if (right)
                playerAnim.SetBool("backwardRight", true);
            else if (left)
                playerAnim.SetBool("backwardLeft", true);
            else
                playerAnim.SetBool("backwardWalk", true);
        }
        else
        {
            if (right)
                playerAnim.SetBool("right", true);
            else if (left)
                playerAnim.SetBool("left", true);
        }
    }

    private IEnumerator Cooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}