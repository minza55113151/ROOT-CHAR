using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    public bool isCanMove = true;
    public int id;

    public Transform input; 
    public Transform output;

    public List<int> inputId;
    public List<string> inputName;
    public List<int> inputAmount;
    public List<int> currentInputAmount;

    public List<int> outputId;
    public List<string> outputName;
    public List<int> outputAmount;
    public List<int> currentOutputAmount;

    public List<GameObject> inputLabel;
    public List<GameObject> outputLabel;


    public float generateRate;
    float generateTime =  0f;
    private void OnValidate()
    {
        for (int i = 0; i < inputName.Count; i++)
        {
            inputLabel[i].GetComponent<TextMeshPro>().text = inputName[i];
        }
        for (int i = 0; i < outputName.Count; i++)
        {
            outputLabel[i].GetComponent<TextMeshPro>().text = outputName[i];
        }

    }
    private void Start()
    {
        for(int i = 0; i < input.childCount; i++)
        {
            Transform child = input.GetChild(i);
            inputId.Add(child.gameObject.GetInstanceID());
            child.GetComponent<IONode>().itemName = inputName[i];
            child.GetComponent<IONode>().indexInNode = i;
            child.GetComponent<IONode>().node = this;
            currentInputAmount.Add(0);
        }
        for(int i = 0; i < output.childCount; i++)
        {
            Transform child = output.GetChild(i);
            outputId.Add(child.gameObject.GetInstanceID());
            child.GetComponent<IONode>().itemName = outputName[i];
            child.GetComponent<IONode>().indexInNode = i;
            child.GetComponent<IONode>().node = this;
            currentOutputAmount.Add(0);
        }
    }
    private void Update()
    {
        Generate();
    }
    public void Generate()
    {
        generateTime += Time.deltaTime;
        if (generateTime < generateRate) return;
        generateTime -= generateRate;

        for (int i = 0; i < output.childCount; i++)
        {
            Transform child = output.GetChild(i);
            child.GetComponent<IONode>().Output();
        }
    }
    public void Input(int index)
    {
        currentInputAmount[index]++;
        if (currentInputAmount[index] >= inputAmount[index])
        {
            for(int i = 0; i < currentInputAmount.Count; i++)
            {
                if(currentInputAmount[i] < inputAmount[i])
                {
                    return;
                }
            }
            for(int i = 0; i < currentInputAmount.Count; i++)
            {
                currentInputAmount[i] -= inputAmount[i];
            }
            if(outputAmount.Count == 0)
            {
                //for quest
            }
            else
            {
                for(int i = 0; i < output.childCount; i++)
                {
                    Transform child = output.GetChild(i);
                    child.GetComponent<IONode>().Output();
                }
            }
        }
    }
    public void DragBox()
    {
        if (!isCanMove) return;
        PlayerController.instance.DragBox(transform);
        Reconnect();
    }
    public void Reconnect()
    {
        foreach(var inpId in inputId)
        {
            GameObject inp = PlayerController.instance.GameObjectDict[inpId];
            if (inp == null)
            {
                continue;
            }
            inp.GetComponent<IONode>().Reconnect();
        }
        foreach (var outId in outputId)
        {
            GameObject outp = PlayerController.instance.GameObjectDict[outId];
            if (outp == null)
            {
                continue;
            }
            outp.GetComponent<IONode>().Reconnect();
        }
    }



}
