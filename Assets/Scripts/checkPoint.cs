using Unity.VisualScripting;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    public GameObject cPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
        if(player == null)
        {
            Debug.Log("Useless unity cant find playerscript again");
        }
        // player = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //https://tinyurl.com/yjm2hasb
        if(player.health <= 0)
        {
            player.transform.position = cPoint.transform.position;
            player.health = player.maxHealth;
            player.lives--;
        }
    }
}

