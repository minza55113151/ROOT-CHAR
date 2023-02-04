using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    public float scrollSpeed;
    public float minZoom;
    public float maxZoom;

    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * scrollSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, 10);
        }
    }
}
