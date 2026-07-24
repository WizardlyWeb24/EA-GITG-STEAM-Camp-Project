using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;
    public Slider healthBar;
    public GameObject healthUI;
    private PlayerMovement playerMovement;
    public Animator animator;
    private GameManager gameManager;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private void Start()
    {
        currentHealth = maxHealth;

        gameManager = FindAnyObjectByType<GameManager>();

        playerMovement = GetComponent<PlayerMovement>();

        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (playerMovement != null)
        {
            playerMovement.PlayHitAnimation();
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if (gameObject.name == "Character 1")
        {
            gameManager.ShowEndingScreen("Player 2");
        }
        else if (gameObject.name == "Character 2")
        {
            gameManager.ShowEndingScreen("Player 1");
        }

        gameObject.SetActive(false);
    }

    public void ResetHealth()
    {
        gameObject.SetActive(true);

        if (playerMovement != null)
        {
            playerMovement.ResetPlayer();
        }


        currentHealth = maxHealth;

        transform.position = startingPosition;
        transform.rotation = startingRotation;

        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }

    public void ShowHealthBar()
    {
        if (healthUI != null)
        {
            healthUI.gameObject.SetActive(true);
        }
    }

    public void HideHealthBar()
    {
        if (healthUI != null)
        {
            healthUI.gameObject.SetActive(false);
        }
    }
}