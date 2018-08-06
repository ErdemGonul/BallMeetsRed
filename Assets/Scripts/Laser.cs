using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public LineRenderer laser;
    public BoxCollider2D col;
    GameController controller;
    // Use this for initialization
    private void Awake()
    {

        laser = GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider2D>();
        Invoke("startAnimation", Random.Range(1f, 3f));
    }
    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void startAnimation()
    {
        StartCoroutine(laserAnimation());
    }
    IEnumerator laserAnimation()
    {
        while (true)
        {
           
                if (laser.enabled)
                {
                    laser.enabled = false;
                    col.enabled = false;
                }
                else
                {
                    laser.enabled = true;
                    col.enabled = true;
                }
                yield return new WaitForSeconds(0.8f);
            }
    }
	// Update is called once per frame
	void Update () {
		
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
