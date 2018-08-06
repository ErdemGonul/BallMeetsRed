using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollRectComponentSolver : MonoBehaviour {
    public bool isCollision = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isCollision == false)
        {
            GetComponent<Button>().enabled = false;
            GameController.findChildTechnique(gameObject).GetComponent<SpriteRenderer>().enabled = false;
            isCollision = false;
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollision = true;
        GetComponent<Button>().enabled = true;

        GameController.findChildTechnique(gameObject).GetComponent<SpriteRenderer>().enabled = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollision = false;
        GameController.findChildTechnique(gameObject).GetComponent<SpriteRenderer>().enabled = false;

        GetComponent<Button>().enabled = false;
    }

 
}
