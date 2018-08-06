using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetup : MonoBehaviour
{
    public GameController controller;
    public GuiHandler handler;
    public LineRenderer lineRenderer;
    public List<GameObject> lineArray;
    public float x;
    public float y;
    public float lastY;
    public Material greenColor;
    public Material redColor;
    public int arraysCount;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 edgePoint;
    public GameObject linePart;
    public float randomX;
    public LineRenderer lineRendererS;
    public EdgeCollider2D colliderS;
    public BezierCurve2D curveDrawer;
    public BezierCurve2DCollider curveCollider;

    public GameObject curvesBotPrefab;
    public LineSetupTop lineTopSetup;
    public Vector3 zeroPoint;

    public float enemyObjectsRate;
    public float coinRate;
    public float crystalRate;
    public float circleRate;
    public int lastSpawnedCoinArraysCount=-10;
    // Use this for initialization
    public void Awake()
    {
        zeroPoint = new Vector3(0,0,0);
        lineTopSetup = GameObject.Find("LineSetupTop").GetComponent<LineSetupTop>();
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        handler = GameObject.Find("OnClickEvents").GetComponent<GuiHandler>();
        x = -12;
        y = Random.Range(-5, 0);
        lastY = y;

        GameObject linePart = Instantiate(curvesBotPrefab, zeroPoint, Quaternion.identity);

        lineRendererS = lineRendererSetup(linePart);

        colliderS = linePart.GetComponent<EdgeCollider2D>();
        curveDrawer = linePart.GetComponent<BezierCurve2D>();
        curveDrawer.drawQuadraticCurve(lineRendererS, 50, new Vector3(-17, 0, 0), new Vector3(-16, 0, 0), new Vector3(-15, 0, 0));
        curveCollider = linePart.GetComponent<BezierCurve2DCollider>();
        curveCollider.CreateColliderQuadraticCurve2D(colliderS, 50, new Vector3(-17, 0, 0), new Vector3(-16, 0, 0), new Vector3(-15, 0, 0));
        colliderS.edgeRadius = 0.1f;
    }
    public void Start()

    {
        for (int i = 0; i < 20; i++)
        {

            spawnAtStart();
           
        }
        StartCoroutine(onCoroutine());
    }
    IEnumerator onCoroutine()
    {
        while (true)
        { //variable that enables you to kill routine   bool isRedSpikeCreated = false;

            if (GameObject.FindGameObjectsWithTag("lineBottom").Length < 20)
            {
                spawnAtStart();

            }
            
            yield return new WaitForSeconds(1f);
        }
    }
    public void spawnAtStart()
    {
     

        lineTopSetup.lineSetupperTop();
           
            lineSetupBot();
            arraysCount = lineArray.Count - 1;
      
        float randVal = Random.value;
            if (Random.value < coinRate)
        {
            if (arraysCount - lastSpawnedCoinArraysCount > 5)
            {
                lastSpawnedCoinArraysCount = arraysCount;
                controller.spawnCoin(arraysCount, Random.Range(3, 8));
                
            }


        }
   
            if (Random.value < enemyObjectsRate )
                controller.spawnRedSpike(arraysCount);
            else if(Random.value < enemyObjectsRate )
            controller.spawnWreckingBall(arraysCount);
            else if (Random.value <crystalRate )
            controller.spawnCrystal(arraysCount);
            else if (Random.value< enemyObjectsRate )
            controller.spawnGreenSpike(arraysCount);
            else if (Random.value < circleRate &&  handler.gameStarted)
            controller.spawnCircleAttackers(arraysCount,Random.Range(1,2));
      
            
    }

    public void lineSetupBot()
    {
        startPoint = new Vector3(x, lastY, -5);
        x = x + lineTopSetup.randomX;
        y = Random.Range(-4f, -2f);
        lastY = y;
        endPoint = new Vector3(x, y, -5);
        y = Random.Range(-4f, -2f);
        edgePoint = new Vector3(x - 1, y, -5);
        GameObject linePart = Instantiate(curvesBotPrefab, zeroPoint, Quaternion.identity);

        lineRendererS = lineRendererSetup(linePart);
        
        colliderS = linePart.GetComponent<EdgeCollider2D>();
        curveDrawer = linePart.GetComponent<BezierCurve2D>();
        //linePart.transform.position = (startPoint + endPoint) / 2;
        curveDrawer.drawQuadraticCurve(lineRendererS, 50, startPoint - new Vector3(0.0f, 0, 0), edgePoint, endPoint);
        curveCollider = linePart.GetComponent<BezierCurve2DCollider>();
        curveCollider.CreateColliderQuadraticCurve2D(colliderS, 50, startPoint, edgePoint, endPoint);

        Line forVariables = linePart.GetComponent<Line>();
        forVariables.startPoint = startPoint;
        forVariables.edgePoint = edgePoint;
        forVariables.endPoint = endPoint;
        colliderS.edgeRadius = 0.08f;
        if (Random.value < 0.3f)
        {
            //colliderS.isTrigger = true;
            lineRendererS.material = redColor;
            
        }
        if (Random.value < 0.1f)
        {

            lineRendererS.GetComponent<Renderer>().enabled = false;
            linePart.layer = 9;//unvisible terrain
        }
        lineArray.Add(linePart);
    }
    public LineRenderer lineRendererSetup(GameObject lineObject)
    {
        lineObject.transform.parent = gameObject.transform;
        
        LineRenderer linePartRenderer = lineObject.GetComponent<LineRenderer>();
        linePartRenderer.numCornerVertices = 55;
        linePartRenderer.numCapVertices = 55;
        linePartRenderer.positionCount = 50;
        linePartRenderer.sortingOrder = 2;
      
        linePartRenderer.startWidth = 0.15f;
        linePartRenderer.endWidth = 0.15f;
        //başta 0.1di
        linePartRenderer.material = greenColor;
        return linePartRenderer;
    }

}


    



