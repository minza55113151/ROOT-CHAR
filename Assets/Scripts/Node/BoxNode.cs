using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxNode : MonoBehaviour
{
    private void OnMouseDrag()
    {
        transform.parent.GetComponent<Node>().DragBox();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlayClickBox();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlayExitBox();
        }
    }
}
