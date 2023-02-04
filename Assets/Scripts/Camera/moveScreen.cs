using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScreen : MonoBehaviour
{
    public float speed = 10f;
    Camera cam;
    bool isDrag = false;
    Vector3 startPos;
    Vector3 startMousePos;
    private void Start()
    {
        cam = Camera.main;
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

    private void OnMouseDrag()
    {
        if (PlayerController.instance.isDragBox || PlayerController.instance.isDragLine) return;
        if (isDrag)
        {
            Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x - 0.5f, mousePos.y - 0.5f, mousePos.z);
            //Debug.Log(mousePos);
            Vector3 moveVec = (mousePos - startMousePos) ;
            moveVec = new Vector3(moveVec.x * 8f, moveVec.y * 4.5f, moveVec.z);
            cam.transform.position = startPos - moveVec * cam.orthographicSize / 2.1f;
        }
        else
        {
            isDrag = true;
            startPos = cam.transform.position;
            startMousePos = cam.ScreenToViewportPoint(Input.mousePosition);
            startMousePos = new Vector3(startMousePos.x - 0.5f, startMousePos.y - 0.5f, startMousePos.z);
        }
    }
    private void MouseUp()
    {
        isDrag = false;
    }
}
