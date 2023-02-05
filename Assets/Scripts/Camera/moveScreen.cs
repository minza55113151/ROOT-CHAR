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
    public float changeColorRate;
    float changeColorTime = 0f;

    float distance;
    float movingTime;
    float movingTimeCount = 0f;

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
        Coloring();
        movingBackground();
    }
    private void movingBackground()
    {
        distance = 0.435f;
        movingTime = 1f;
        transform.Translate(Vector3.down * distance * movingTime * Time.deltaTime);
        
        movingTimeCount += Time.deltaTime;
        if (movingTimeCount < movingTime) return;
        movingTimeCount -= movingTime;

        transform.Translate(Vector2.up * distance);
    }
    private void Repair()
    {
    }
    
    private void Coloring()
    {
        changeColorTime += Time.deltaTime;
        if (changeColorTime < changeColorRate) return;
        changeColorTime -= changeColorRate;

        int color = PlayerPrefs.GetInt("color", 0);
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            sr.color = Color.HSVToRGB(color/360f, 0.5f, 1f);
        }
        color = (color + 1) % 360;
        PlayerPrefs.SetInt("color", color);
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (PlayerController.instance == null) return;
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
