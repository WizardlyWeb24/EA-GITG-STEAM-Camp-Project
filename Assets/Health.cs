using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    private float currentHealth;
    public Slider healthBar;

    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.value = 1f;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " took " + damage + " damage!");

        currentHealth -= damage;

        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
            Die();
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