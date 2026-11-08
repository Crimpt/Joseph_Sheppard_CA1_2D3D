using NUnit.Framework;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hasBounce = false;
    private Rigidbody2D rb;

    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * (force));
        //Engine was spawning the projectile upright even though I set the rotation to z:90 in the inspector
        //So this code is needed to rotate it after the fact
        rb.SetRotation(90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Debris")||
            collision.gameObject.CompareTag("Enemy")||
            collision.gameObject.CompareTag("shot_container")||
            collision.gameObject.CompareTag("Ground")
        )
        {
            Destroy(gameObject);
        }
    }
}
