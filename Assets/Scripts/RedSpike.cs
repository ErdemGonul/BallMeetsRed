using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpike : MonoBehaviour {
    public Rigidbody2D velocity;
    GameController controller;
    private void Awake()
    {
        velocity = GetComponent<Rigidbody2D>();

        velocity.velocity = new Vector2(0, 2f);
    }
    // Use this for initialization
    void Start () {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "lineBottom" || collision.gameObject.tag == "lineTop")
            velocity.velocity = -1 * velocity.velocity;
        else if (collision.gameObject.tag == "Players")
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
                GameController.findChildTechnique(collision.gameObject).SetActive(false);


            }
        }
    }
   
}
