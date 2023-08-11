using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed; 
    public Vector2 directionToMove;
    [Header("Lifetime Stuff")]
    public float lifeTime;
    private float lifeTimeSeconds;
    public  Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeSeconds = lifeTime;
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if (lifeTimeSeconds <= 0)
        {
            Destroy(gameObject);
        } 
        
    }
    public void Launch (Vector2 initialVel)
    {
        myRigidbody.velocity = initialVel * moveSpeed;
    }
    public void OnTriggerEnter2D(Collider2D other) {
       Destroy(gameObject); 
    }
}
