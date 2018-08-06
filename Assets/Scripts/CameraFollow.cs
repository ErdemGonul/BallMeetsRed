using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    GameObject cameraBlocker;
    public GameController controller;
    public float oldX;
    private readonly Vector3 vector3= new Vector3(20f, 0, 0);
    // Use this for initialization
    void Start () {
        cameraBlocker = GameObject.Find("CameraBlocker");
        
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        //player = controller.ball;
    }
	// Update is called once per frame
	void Update () {
        if (controller.gameContinue)
        {

            transform.position = new Vector3(controller.ball.transform.position.x + 5f, 0, transform.position.z);

            if (transform.position.x > oldX)
            {
                cameraBlocker.transform.position = transform.position - vector3;
                oldX = transform.position.x;
            }
        }
    }

}
