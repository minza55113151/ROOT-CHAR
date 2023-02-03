using System.Collections;
using System.Collections.Generic;
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

    Vector3 normalNode;
    Vector3 bigNode;
    bool isBig = false;
    private void Start()
    {
        normalNode = transform.localScale;
        bigNode = transform.localScale * scale;
    }
    private void OnMouseDrag()
    {
        PlayerController.instance.DragLine(transform.position);
    }
    private void OnMouseOver()
    {
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

}
