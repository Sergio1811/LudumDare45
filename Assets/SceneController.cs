using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int currentObject;
    public GameObject[] AllObjects;
    public Transform Transform;

 
    private void Start()
    {
        AllObjects[currentObject].SetActive(true);
    }
    private void Update()
    {
        this.gameObject.transform.Rotate(this.transform.up, 10);
    }

    public void NextObject()
    {
        if (currentObject < AllObjects.Length-1)
            currentObject++;
        else
            currentObject = 0;

        ChangeMesh();
    }
    public void PreviousObject()
    {
        if (currentObject > 0)
            currentObject++;
        else
            currentObject = AllObjects.Length-1;

        ChangeMesh();
    }

    public void ChangeMesh()
    {
        foreach (var item in AllObjects)
        {
            item.SetActive(false);
        }
        AllObjects[currentObject].SetActive(true);
      
    }
    
}
