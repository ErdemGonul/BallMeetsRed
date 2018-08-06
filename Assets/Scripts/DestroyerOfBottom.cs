using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOfBottom : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Destroy(collision.gameObject);
        if (collision.gameObject.tag == "Players")
        {
            PlayerPrefs.SetInt("deathCause", 2);
            GameObject.Find("GameController").GetComponent<GameController>().lost();
        }
    }
}
