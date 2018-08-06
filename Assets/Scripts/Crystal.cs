using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour {
    public GameObject player;
    public GameController controller;
    private void Start()
    {

        controller = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("Ball");
    }

    private void Update()
    {
        if (controller.gameContinue && controller.canMagnet)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 5)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Players")
        {
            Destroy(gameObject);
            GameObject.Find("GameController").GetComponent<GameController>().increaseScore(5);
        }
        else if (collision.gameObject.tag == "lineBottom" || collision.gameObject.tag == "lineTop")
        {
            Destroy(gameObject);
        }

    }
}
