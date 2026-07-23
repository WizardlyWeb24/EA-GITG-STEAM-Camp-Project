using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject projecttile;
    public string C;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C ))
        {
            GameObject fireball = Instantiate(projecttile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            rb.angularVelocity = transform.forward * 20;
        }
    }
}
