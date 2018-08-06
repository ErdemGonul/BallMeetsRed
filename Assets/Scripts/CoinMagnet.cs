using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {

    public GameObject player;
    public GameController controller;
	// Use this for initialization
	void Start () {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Players");
	}
	
	// Update is called once per frame
	void Update () {
       
        try
        {
            if (controller.canMagnet)
                transform.position = player.transform.position + new Vector3(0.7f, -0.06f, 0f);
        }
        catch (System.Exception )
        {
            Destroy(gameObject);
        }
	}
}
