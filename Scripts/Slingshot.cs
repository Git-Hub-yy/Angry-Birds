using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Slingshot : MonoBehaviour
{

    public static Slingshot Instance{ get; private set; }
    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;
    private Transform leftPoint;
    private Transform rightPoint;
    private Transform centerPoint;
    private bool isDrawing = false;
    private Transform birdTransform;
    private float birdRadius;
    private void Awake()
    {
        Instance = this;
        leftLineRenderer = transform.Find("LeftLineRenderer").GetComponent<LineRenderer>();
        rightLineRenderer = transform.Find("RightLineRenderer").GetComponent<LineRenderer>();
        leftPoint = transform.Find("LeftPoint");
        rightPoint = transform.Find("RightPoint");
        centerPoint = transform.Find("CenterPoint");
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing) { Draw(); }
    }
    public void StartDraw(Transform birdTransform,float birdRadius)
    {
        isDrawing = true;
        this.birdTransform = birdTransform;
        this.birdRadius = birdRadius;
        ShowLine();
       
    }

 
    public void EndDraw()
    {
        isDrawing = false;
        HideLine();
    }
    private void Draw()
    {
        Vector3 birdPosition = birdTransform.position;
        birdPosition = birdPosition + (birdPosition - centerPoint.position).normalized * birdRadius;
        leftLineRenderer.SetPosition(0, birdPosition);
        leftLineRenderer.SetPosition(1, leftPoint.position);
        rightLineRenderer.SetPosition(0, birdPosition);
        rightLineRenderer.SetPosition(1, rightPoint.position);
    }
    private void HideLine()
    {
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;
    }
    private void ShowLine()
    {
        leftLineRenderer.enabled = true;
        rightLineRenderer.enabled = true;
    }
    public Vector3 getCenterPosition()
    {
        return centerPoint.position;
    }

}
