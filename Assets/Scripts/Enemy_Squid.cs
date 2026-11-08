using UnityEngine;

public class Enemy_Squid : MonoBehaviour
{
    Rigidbody2D rb = new Rigidbody2D();
    public float speed = 7f;
    private float timePassed = 0.0f;
    private Vector2 directionRight = Vector2.right;
    public float health = 1.0f;
    public float agroRange = 5f;
    public Transform playerTransform;
    [SerializeField] private PlayerScript player;
    private bool isChasing;
    private float meleeCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerScript>();
        transform.Translate(directionRight * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //This code moves the enemy side to side after a certain amount of time
        timePassed += Time.deltaTime;
        meleeCoolDown += Time.deltaTime;

        //https://tinyurl.com/yzdxvnkv
        if(isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                if(transform.position.y > playerTransform.position.y)
                {transform.position += Vector3.down * speed * Time.deltaTime + Vector3.left * speed * Time.deltaTime;}
                else if(transform.position.y < playerTransform.position.y)
                { transform.position += Vector3.up * speed * Time.deltaTime + Vector3.left * speed * Time.deltaTime;}
                else
                {transform.position += Vector3.left * speed * Time.deltaTime;}
            }
            if(transform.position.x < playerTransform.position.x)
            {
                if(transform.position.y > playerTransform.position.y)
                {transform.position += Vector3.down * speed * Time.deltaTime + Vector3.right * speed * Time.deltaTime;}
                else if(transform.position.y < playerTransform.position.y)
                {transform.position += Vector3.up * speed * Time.deltaTime + Vector3.right * speed * Time.deltaTime;}
                else
                {transform.position += Vector3.right * speed * Time.deltaTime;}
            }
        }
        else
        {
            if(Vector2.Distance(transform.position,playerTransform.position) < agroRange)
            {isChasing = true;}
            if(timePassed > 0.5f)
            {
                directionRight *= -1;
                timePassed = 0;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile_Player"))
        {
            health--;
            gameObject.GetComponent<Renderer>().material.color = new Color(0+(health/2), 0, 0);
        }
        if(health <= 0){Destroy(gameObject);}
        if(collision.gameObject.CompareTag("Player"))
        {
            if(meleeCoolDown > 2.0f)
            {
                Debug.Log("Melee Cooldown expire");
                player.health--;
                meleeCoolDown = 0;
            }
        }
    }
}
