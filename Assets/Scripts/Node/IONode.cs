using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IONode : MonoBehaviour
{
    public enum Type
    {
        input,
        output,
    }
    public Type type;
    public int connected;
    public float scale;
    public float scaleTime;
    public string itemName;
    public int indexInNode;

    public int prevId;
    public int id;
    public int nextId;
    public LineRenderer lineRenderer;

    public Node node;

    GameObject itemContainer;
    Vector3 normalNode;
    Vector3 bigNode;
    bool isBig = false;
    private void Start()
    {
        id = gameObject.GetInstanceID();
        PlayerController.instance.GameObjectDict.Add(id, gameObject);
        PlayerController.instance.GameObjectDict.TryGetValue(id, out var s);
        //Debug.Log(id.ToString() + s.ToString());
        prevId = PlayerPrefs.GetInt("prev" + id.ToString(), 0);
        nextId = PlayerPrefs.GetInt("next" + id.ToString(), 0);
        if(nextId != 0)
        {
            Connect(PlayerController.instance.GameObjectDict[nextId].GetComponent<IONode>());
        }


        if(type == Type.output)
        {
            itemContainer = transform.Find("Container").gameObject;
        }
        
        
        normalNode = transform.localScale;
        bigNode = transform.localScale * scale;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (type == Type.input)
            {
                if (collision.gameObject.GetComponent<Item>().targetId == id)
                {
                    Destroy(collision.gameObject);
                    node.Input(indexInNode);
                }
            }
        }
    }
    public void Output()
    {
        if (nextId == 0) return;
        ItemManager.instance.CreateItem(id, nextId, itemName, itemContainer.transform);
    }
    private void OnMouseDrag()
    {
        PlayerController.instance.DragLine(transform);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PlayerController.instance.Connect(transform);
        }

        if (isBig) return;
        isBig = true;
        StartCoroutine(ScaleToBig());
    }
    private void OnMouseExit()
    {
        if(!isBig) return;
        isBig = false;
        StartCoroutine(ScaleToSmall());
    }
    private IEnumerator ScaleToBig()
    {
        for(float i = 0; i < scaleTime; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(normalNode, bigNode, i / scaleTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private IEnumerator ScaleToSmall()
    {
        for (float i = 0; i < scaleTime; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(bigNode, normalNode, i / scaleTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    
    //destroy old inp node
    public void ClearConnect()
    {
        ReContainer();
        if (type == Type.output)
        {
            if(lineRenderer != null)
                Destroy(lineRenderer.gameObject);
            nextId = 0;
            PlayerPrefs.SetInt("next" + id.ToString(), nextId);
        }
        else
        {
            prevId = 0;
            PlayerPrefs.SetInt("prev" + id.ToString(), prevId);
        }
    }
    public void Connect(IONode ioNode)
    {
        lineRenderer = Instantiate(PlayerController.instance.lineRendererPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        nextId = ioNode.id;
        PlayerPrefs.SetInt("next" + id.ToString(), nextId);
        ioNode.prevId = id;
        PlayerPrefs.SetInt("prev" + ioNode.id.ToString(), id);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0f));
        lineRenderer.SetPosition(1, new Vector3(ioNode.transform.position.x, ioNode.transform.position.y, 0f));

    }
    public void Reconnect()
    {
        ReContainer();
        if (type == Type.input)
        {
            if (prevId == 0) return;
            IONode prevNode = PlayerController.instance.GameObjectDict[prevId]?.GetComponent<IONode>();
            prevNode.Reconnect();
        }
        else
        {
            if (nextId == 0) return;
            GameObject nextNode = PlayerController.instance.GameObjectDict[nextId];
            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0f));
            lineRenderer.SetPosition(1, new Vector3(nextNode.transform.position.x, nextNode.transform.position.y, 0f));
        }
    }
    public void ReContainer()
    {
        if (itemContainer == null) return;
        for(int i = 0; i < itemContainer.transform.childCount; i++)
        {
            GameObject go = itemContainer.transform.GetChild(i).gameObject;
            Destroy(go);
        }
    }

}
