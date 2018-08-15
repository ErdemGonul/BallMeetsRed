
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameController controller;
    public Rigidbody2D rigidOfPlayer;
    public GuiHandler handler;
    public Material playerColorMaterial;
    public bool justOneCourotine = false;
    public GameObject notificationText;

    public Vector3 firstPos;
    public bool isBrake = false;
    public float limitSpeed=5;
    private void Awake()
    {
        MarketEvents.firstOpening();

        Time.timeScale = 1;

        SavePlayer x=MarketEvents.readDataFromFile();
        playerColorMaterial = Resources.Load<Material>("PlayerMaterials/" + x.color);

        if (playerColorMaterial == null)
        {
            playerColorMaterial = Resources.Load<Material>("PlayerMaterials/whitePlayer");
        }
        GetComponent<SpriteRenderer>().sharedMaterial = playerColorMaterial;
    }
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        rigidOfPlayer = gameObject.GetComponent<Rigidbody2D>();
        handler = GameObject.Find("OnClickEvents").GetComponent<GuiHandler>();
        firstPos = transform.position;
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && handler.gameStarted == true)
        {
           
            rigidOfPlayer.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        }
        if ( handler.gameStarted == true)
        {
            mover();
        }
       
    }
    public void mover()
    {
        
        rigidOfPlayer.AddForce(new Vector2(1 * 10, 0));
        rigidOfPlayer.velocity = new Vector2(Mathf.Clamp(rigidOfPlayer.velocity.x, -limitSpeed, limitSpeed),(Mathf.Clamp(rigidOfPlayer.velocity.y,-5f,4f)));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "lineBottom" || collision.gameObject.tag == "lineTop")
        {
            if (collision.gameObject.GetComponent<LineRenderer>().sharedMaterial.name == "redColor")
            {
                if (!controller.hasShield)
                {
                    Destroy(gameObject);
                    PlayerPrefs.SetInt("deathCause", 1);
                    controller.lost();
                   
                }
                else
                {
                    controller.hasShield = false;
                    findChildTechnique(gameObject, "shield").SetActive(false);


                }

            }
        }
        
    }

    public  static GameObject findChildTechnique(GameObject ballType, string tag)
    {
        Debug.Log("buraya girdi");
        foreach (Transform child in ballType.transform)
        {
            if (child.tag == tag)
            {

                return child.gameObject;


            }
        }
        return null;
    }
    public void changePlayer(GameObject ballType, Sprite newSprite)
    {
        ballType.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
