using UnityEngine;

public class wreckingBall : MonoBehaviour {
    public LineRenderer line;
    public Rigidbody2D rigid;
    private Vector2 constSpeed;
    GameController controller;
    void Start () {
        line = GetComponent<LineRenderer>();
        rigid=GetComponent<Rigidbody2D>();
        constSpeed = new Vector2(3f, 3f);
        rigid.velocity = constSpeed;
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	void Update () {
       
       line.SetPosition(1, transform.position);
        if (rigid.velocity == Vector2.zero)
        {
            rigid.velocity = constSpeed;
        }

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Players")
        {
            if (!controller.hasShield)
            {
                Destroy(collision.gameObject);
                PlayerPrefs.SetInt("deathCause",1);
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

