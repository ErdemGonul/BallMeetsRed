using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetupTop : MonoBehaviour {

    public LineRenderer lineRenderer;
    public List<GameObject> lineArray;
    public float x;
    public float y;
    public float lastY;
    public Material greenColor;
    public Material redColor;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 edgePoint;
    public GameObject linePart;
    public float randomX;
    public LineRenderer lineRendererS;
    public EdgeCollider2D colliderS;
    public BezierCurve2D curveDrawer;
    public BezierCurve2DCollider curveCollider;
    public Vector3 zeroPoint;
    public GameObject curvesTopPrefab;
    // Use this for initialization
    private void Awake()
    {
        zeroPoint = new Vector3(0, 0, 0);
        x = -12;
        y = Random.Range(5, 0);
        lastY = y;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void lineSetupperTop()
    {
        startPoint = new Vector3(x, lastY, -5);
        randomX = Random.Range(8f, 10f);
        x = x + randomX;
        y = Random.Range(4f, 2f);
        lastY = y;
        endPoint = new Vector3(x, y, -5);
        y = Random.Range(4f, 2f);
        edgePoint = new Vector3(x - 1, y, -5);
        GameObject linePart=Instantiate(curvesTopPrefab, zeroPoint, Quaternion.identity);
        lineRendererS = lineRendererSetup(linePart);
        
        colliderS = linePart.GetComponent<EdgeCollider2D>();
        curveDrawer = linePart.GetComponent<BezierCurve2D>();
        curveDrawer.drawQuadraticCurve(lineRendererS, 50, startPoint, edgePoint, endPoint);
        curveCollider = linePart.GetComponent<BezierCurve2DCollider>();
        curveCollider.CreateColliderQuadraticCurve2D(colliderS, 50, startPoint, edgePoint, endPoint);
        colliderS.edgeRadius = 0.08f;
        
        //0.08di
        Line forVariables=linePart.GetComponent<Line>();
        forVariables.startPoint = startPoint;
        forVariables.endPoint = endPoint;
        if (Random.value < 0.3f)
        {
            lineRendererS.material = redColor;
        }
       else if (Random.value > 0.3f && Random.value <0.5)
        {

            lineRendererS.GetComponent<Renderer>().enabled = false;
            linePart.layer = 9;
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
       
        linePartRenderer.material = greenColor;
        return linePartRenderer;
    }
}
