using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 25f;
    public float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);

        Destroy(gameObject);
    }
}