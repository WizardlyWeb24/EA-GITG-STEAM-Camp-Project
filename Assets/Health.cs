using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;
    public Slider healthBar;
    private PlayerMovement playerMovement;
    public Animator animator;
    private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;

        gameManager = FindAnyObjectByType<GameManager>();
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

        if (gameManager != null)
        {
            if (gameObject.name == "Player1")
            {
                gameManager.ShowEndingScreen("Player 2");
            }
            else
            {
                gameManager.ShowEndingScreen("Player 1");
            }
        }

        Destroy(gameObject, 3f);
    }
}