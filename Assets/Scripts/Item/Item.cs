using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed;
    public int id;
    public int targetId;

    TextMeshPro text;
    Rigidbody2D rb;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetItem(int id, int targetId, string name)
    {
        this.id = id;
        this.targetId = targetId;
        text.text = name;
        Vector3 pos1 = PlayerController.instance.GameObjectDict[id].transform.position;
        Vector3 pos2 = PlayerController.instance.GameObjectDict[targetId].transform.position;
        Vector2 moveDirection = (pos2 - pos1).normalized;
        rb.velocity = moveDirection * speed;
    }
}
