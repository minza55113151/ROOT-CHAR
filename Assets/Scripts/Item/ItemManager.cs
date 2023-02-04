using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public GameObject itemPrefab;
    private void Awake()
    {
        instance = this;
    }
    
    public void CreateItem(int id, int targetId, string name, Transform parent)
    {
        GameObject item = Instantiate(itemPrefab, parent);
        item.GetComponent<Item>().SetItem(id, targetId, name);
    }

}
