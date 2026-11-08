using UnityEngine;

public class increaseHealth : MonoBehaviour
{
    Vector2 directionY = Vector2.up;
    public float timeElapsed = 0f;
    public float speed = 0.46f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
