using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class healthUI : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    public TextMeshProUGUI textbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Had to use this method because I struggled to get the normal way working.
        //https://tinyurl.com/ydxaz5ut
        player = GameObject.FindAnyObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //https://tinyurl.com/4hssta99
        textbox.text = "HP: "+player.health+"/"+player.maxHealth+"\nLives: "+player.lives;
    }
}
