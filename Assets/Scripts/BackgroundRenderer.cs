using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRenderer : MonoBehaviour {

    public Material material;
    Vector2 offset;
    public Camera cameraMain;
    public float xVelocity, yVelocity;
    public Vector3 oldPosOfCamera;
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        cameraMain = Camera.main;
        material = GetComponent<Renderer>().material;
        oldPosOfCamera = cameraMain.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        offset = new Vector2(oldPosOfCamera.x-cameraMain.transform.position.x,oldPosOfCamera.y-cameraMain.transform.position.y)/4;
        material.mainTextureOffset += offset * Time.deltaTime;
        oldPosOfCamera = cameraMain.transform.position;
    }
}
