using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;
    public Slider healthBar;
    private PlayerMovement playerMovement;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;

        playerMovement = GetComponent<PlayerMovement>();
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

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Destroy(gameObject, 3f);
    }
}