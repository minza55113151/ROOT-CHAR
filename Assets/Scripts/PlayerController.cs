using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject lineRendererPrefab;
    public LineRenderer lineRenderer;
    public bool isDragLine = false;

    public Dictionary<int, GameObject> GameObjectDict;

    public bool isDragBox = false;

    bool isConnected = false;

    Transform node;
    Vector3 nodePos;
    Vector3 startMousePos;
    private void Awake()
    {
        instance = this;
        GameObjectDict = new Dictionary<int, GameObject>();
        PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
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

    public void DragLine(Transform nodeTransfrom)
    {
        isDragLine = true;
        lineRenderer.enabled = true;
        Vector3 mousePos = GetMousePosition();
        lineRenderer.SetPosition(0, new Vector3(nodeTransfrom.position.x, nodeTransfrom.position.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        this.node = nodeTransfrom;
    }
    private void MouseUp()
    {
        isDragLine = false;
        node = null;

        Destroy(lineRenderer?.gameObject);
        lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        
        isDragBox = false;

        isConnected = false;
    }
    public void Connect(Transform nodeTransform)
    {
        Connect(node, nodeTransform);
    }

    public void Connect(Transform node1, Transform node2)
    {
        if (node1 == null || node2 == null) return;
        if (node1 == node2) return;
        if (node1.parent == node2.parent) return;
        IONode n1 = node1.GetComponent<IONode>();
        IONode n2 = node2.GetComponent<IONode>();
        if (n1.type == n2.type) return;
        if (n1.itemName != n2.itemName) return;
        n1.ClearConnect();
        n2.ClearConnect();
        if (n1.type == IONode.Type.output)
        {
            n1.Connect(n2);
        }
        else
        {
            n2.Connect(n1);
        }

        isConnected = true;
    }
}
