using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class pickUpSpawner : MonoBehaviour
{
    public GameObject pickUp1;
    public GameObject pickUp2;
    public GameObject pickUp3;
    // public int maxUses = 1;
    // private int useCount = 0;
    
    int num = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        num = Random.Range(1,4);
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile_Player"))
        {
            Vector2 postion = GetComponent<Rigidbody2D>().transform.position;
            if(num == 1)
            {Instantiate(pickUp1,postion,Quaternion.identity);}
            else if (num == 2)
            {Instantiate(pickUp2,postion,Quaternion.identity);}
            else if (num == 3)
            {Instantiate(pickUp3,postion,Quaternion.identity);} 
            Destroy(gameObject);
        }
    }
}