using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxNode : MonoBehaviour
{
    private void OnMouseDrag()
    {
        PlayerController.instance.DragBox(transform.parent);
    }
}
