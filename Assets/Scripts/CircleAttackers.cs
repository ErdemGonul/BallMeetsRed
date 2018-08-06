using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackers : MonoBehaviour {

    public Rigidbody2D velocity;
    public GameController controller;
    // Use this for initialization
    void Start()
    {
        velocity = GetComponent<Rigidbody2D>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        velocity.velocity = new Vector2(-2f, 0);

    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Players" ||collision.gameObject.tag=="shield")
        {
            if (!controller.hasShield)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                PlayerPrefs.SetInt("deathCause", 1);
                controller.lost();
            }
            else
            {
                controller.hasShield = false;
                collision.gameObject.SetActive(false);


            }
            Destroy(collision.gameObject);      
        } 
        Destroy(gameObject);
    }
}
