using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    //Player Attributes
    public int speed = 3;
    public int lives = 2;
    private bool isMoving = false;
    public float jumpHeight = 2.4f;
    private int jumpCount = 0;
    public float gunCoolDown = 1f;
    public int health = 1;
    public int maxHealth = 5;
    private int trippleShotCout = 0;
    private bool hasTrippleShot = false;

    //Vectors 
    private Vector2 move;
    private Vector2 lastPosition;

    //Amiation
    private Animator animator;
    private bool isJumping = true;
    private int direction = 0;

    //Time
    private float timePassed = 0f;    

    //Misc
    public GameObject projectilePrefab;
    private AudioSource laserBlast;
    private AudioSource playerDamaged;
    private AudioSource pickUpGet;
    private Rigidbody2D rb;

    [SerializeField] private string newGameLevel = "Menu";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = rb.position;

        //https://tinyurl.com/tchsx7s2
        var aSources = GetComponents<AudioSource>();
        laserBlast = aSources[0];
        playerDamaged = aSources[1];
        pickUpGet = aSources[2];
    }

    private void Fire()
    {
        if(isMoving)
        {
            if(timePassed > gunCoolDown)
            {
                if(hasTrippleShot)
                {
                    if(trippleShotCout > 1)
                    {
                        //Code is formatted how the shots should look on screen for easier reading
                        laserBlast.Play();
                        GameObject go4 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,16));
                        Projectile pro4 = go4.GetComponent<Projectile>();
                        pro4.Launch(new Vector2(direction, 0.6f), 1200);

                        GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,8));
                        Projectile pro = go.GetComponent<Projectile>();
                        pro.Launch(new Vector2(direction, 0.3f), 1200);
                        laserBlast.Play();

                        GameObject go2 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,4));
                        Projectile pro2 = go2.GetComponent<Projectile>();
                        pro2.Launch(new Vector2(direction, 0.1f), 1200);
                        laserBlast.Play();

                        GameObject go5 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,-4));
                        Projectile pro5 = go5.GetComponent<Projectile>();
                        pro5.Launch(new Vector2(direction, -0.1f), 1200);
                        laserBlast.Play();

                        GameObject go3 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,-8));
                        Projectile pro3 = go3.GetComponent<Projectile>();
                        pro3.Launch(new Vector2(direction, -0.3f), 1200);
                        laserBlast.Play();

                        GameObject go6 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,-16));
                        Projectile pro6 = go6.GetComponent<Projectile>();
                        pro6.Launch(new Vector2(direction, -0.6f), 1200);
                        laserBlast.Play(); 
                        rb.AddForce(new Vector2(direction*-5,0),ForceMode2D.Impulse);

                    }
                    else
                    {
                        for(int i = 0; i <= trippleShotCout;i++)
                        {
                            GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,3));
                            Projectile pro = go.GetComponent<Projectile>();
                            pro.Launch(new Vector2(direction, 0.2f), 1200);

                            GameObject go2 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,0));
                            Projectile pro2 = go2.GetComponent<Projectile>();
                            pro2.Launch(new Vector2(direction, 0), 1200);
                            laserBlast.Play();

                            GameObject go3 = Instantiate(projectilePrefab, rb.transform.position, Quaternion.Euler(0,0,-3));
                            Projectile pro3 = go3.GetComponent<Projectile>();
                            pro3.Launch(new Vector2(direction, -0.2f), 1200);
                            laserBlast.Play();
                            rb.AddForce(new Vector2(direction*-3,0),ForceMode2D.Impulse);
                        }
                    }
                    
                }
                else
                {
                    GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.identity);
                    Projectile pro = go.GetComponent<Projectile>();
                    pro.Launch(new Vector2(direction, 0), 1200);
                }
                laserBlast.Play();
                timePassed = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        move = InputSystem.actions["Move"].ReadValue<Vector2>();
        timePassed += Time.deltaTime;
        if(health <=0)
        {
            // Destroy(gameObject);
            Debug.Log("Health is 0 or less");
            SceneManager.LoadSceneAsync("Menu");
        }
        if (InputSystem.actions["Jump"].IsPressed() && jumpCount < 1)
        {
            InputSystem.actions["Jump"].Reset();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight)), ForceMode2D.Impulse);
            jumpCount++;
            isJumping = true;
            animator.SetBool("isJumping",isJumping);
        }
        if(health <= 0){lives--;}
        if (InputSystem.actions["Attack"].IsPressed())
        {
            // InputSystem.actions["Attack"].Reset();
            if(hasTrippleShot)
            {
                for(int i = 0;i <= trippleShotCout;i++)
                {Fire();}
            }
            else{Fire();}
        }

        if(move.x != 0)
        {
            direction = move.x < 0 ? -1 : 1;
            animator.SetInteger("Direction", direction);
            isMoving = true;
            animator.SetBool("isMoving",isMoving);
        }
        else
        {
            direction = 0;
            animator.SetInteger("Direction", direction);
            isMoving = false;
        }
        if(lives == 0)
        {SceneManager.LoadScene("Menu");}
    }

    void FixedUpdate()
    {
        Vector2 position = rb.transform.position;
        position.x += (move.x*speed)*Time.fixedDeltaTime;
        rb.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") )
        {
            jumpCount = 0;
            isJumping = false;
            animator.SetBool("isJumping",isJumping);
        }
        if(collision.gameObject.CompareTag("Ship"))
        {
            SceneManager.LoadScene(newGameLevel);
        }
        if(collision.gameObject.CompareTag("shotSpeedUP"))
        {
            pickUpGet.Play();
            laserBlast.pitch *= 1.1f;
            gunCoolDown *= 0.70f;
            //30% increase in shot speed
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Health lost -1");
            playerDamaged.Play();
            // health--;
        }
        if(collision.gameObject.CompareTag("trippleShot"))
        {
            //Reduce fire rate by 20%
            if(trippleShotCout < 2)
            {
                trippleShotCout++;
                laserBlast.pitch *= 0.8f;
                hasTrippleShot = true;
                gunCoolDown *= 1.2f;
                pickUpGet.Play();
            }
        }
        if(collision.gameObject.CompareTag("Gravity_Low"))
        {
            // Physics.gravity = new Vector3(0, -2.0F, 0);
            rb.gravityScale = 0.3f;
            jumpHeight -= 1;
            pickUpGet.Play();
        }
        if(collision.gameObject.CompareTag("Player_Bounding_Box"))
        {health = 0;}
        if(collision.gameObject.CompareTag("increaseHealth"))
        {
            maxHealth++;
            health = maxHealth;
            pickUpGet.Play();
        }
        if(collision.gameObject.CompareTag("Enemy_Projectile"))
        {
            playerDamaged.Play();
            health--;
        }
    }
}
