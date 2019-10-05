using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    public GameObject carro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -carro.transform.forward * Time.deltaTime;
    }
}
