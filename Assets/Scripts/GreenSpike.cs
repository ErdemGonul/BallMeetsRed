using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GreenSpike : MonoBehaviour
{
    public bool toRight=true;
    public Rigidbody2D velocity;
    public Vector3 firstPos;
    GameController controller;
    // Use this for initialization
    void Start()
    {
        firstPos = transform.position;
        velocity = GetComponent<Rigidbody2D>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        velocity.velocity = new Vector2(2f,0 );
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if((firstPos.x + 5 <= transform.position.x) && toRight==true)
        {
            toRight = false;
            velocity.velocity = -velocity.velocity;
        }
        else if((firstPos.x - 5 > transform.position.x) && toRight==false)
        {
            toRight = true;
            velocity.velocity = -velocity.velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
       if (collision.gameObject.tag == "Players")
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
