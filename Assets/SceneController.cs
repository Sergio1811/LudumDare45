using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int currentObject;
    public GameObject[] AllObjects;
    public Transform Transform;

    public float m_rotationSpeed;
    public float minFov;
    public float maxFov;
    public float FovSpeed;


    public Camera m_MyCamera;


 
    private void Start()
    {
        AllObjects[currentObject].SetActive(true);
    }
    private void Update()
    {
        this.gameObject.transform.Rotate(this.transform.up, m_rotationSpeed);
       // this.gameObject.transform.Rotate(this.transform.forward, m_rotationSpeed);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextObject();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousObject();

        if (Input.GetKey(KeyCode.Z))
           m_MyCamera.fieldOfView = Mathf.Clamp (m_MyCamera.fieldOfView - FovSpeed*Time.deltaTime, minFov, maxFov);
        if(Input.GetKeyDown(KeyCode.R)) m_MyCamera.fieldOfView = maxFov;
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
            currentObject--;
        else
        {
            currentObject = AllObjects.Length - 1;
        }

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
