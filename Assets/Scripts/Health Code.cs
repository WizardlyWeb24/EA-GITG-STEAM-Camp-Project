using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;

    public float maxHealth = 100;
    public float currentHealth = 100;

    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }
}
