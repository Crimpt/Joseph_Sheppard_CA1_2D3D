using UnityEngine;

public class trippleShot : MonoBehaviour
{
    Rigidbody2D rb;
    private float timeElapsed = 0f;
    Vector2 directionY = Vector2.up;
    private float speed = 0.46f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionY * speed * Time.deltaTime/2);
        timeElapsed++;
        if(timeElapsed >= 60)
        {
            directionY *= -1;
            timeElapsed = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}