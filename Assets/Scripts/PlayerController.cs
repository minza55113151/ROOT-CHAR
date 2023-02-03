using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public LineRenderer lineRenderer;
    bool isDragLine = false;


    bool isDragBox = false;
    Vector3 nodePos;
    Vector3 startMousePos;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        lineRenderer.positionCount = 2;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }
    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void DragBox(Transform nodeTransform)
    {
        if (isDragLine) return;
        if(isDragBox == true)
        {
            Vector3 mousePos = GetMousePosition();
            Vector3 moveVec = mousePos - startMousePos;
            nodeTransform.position = nodePos + moveVec;
        }
        else
        {
            isDragBox = true;
            this.nodePos = nodeTransform.position;
            startMousePos = GetMousePosition();
        }

    }
    public void DragLine(Vector3 nodePos)
    {
        isDragLine = true;
        lineRenderer.enabled = true;
        Vector3 mousePos = GetMousePosition();
        lineRenderer.SetPosition(0, new Vector3(nodePos.x, nodePos.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
    }
    private void MouseUp()
    {
        Debug.Log("mouseup");
        isDragLine = false;
        lineRenderer.enabled = false;
        isDragBox = false;
    }
}
