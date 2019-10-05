using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carro : MonoBehaviour
{
    public GameObject carro;
    public List<HingeJoint> juntas;
    public GameObject gear;
    public int maxLevelPreciosion = 10;
    public int currentPrecision;
    // Start is called before the first frame update
    void Start()
    {
        ChangePrecision();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -carro.transform.forward * Time.deltaTime;
    }

    public void ChangePrecision()
    {
        if (currentPrecision == 0)
        {
            carro.GetComponent<HingeJoint>().connectedBody = gear.GetComponent<Rigidbody>();
            for (int i = juntas.Count - 1; i >= 0; i--)
            {
                juntas[i].gameObject.SetActive(false);
            }
        }
        else
        {
            carro.GetComponent<HingeJoint>().connectedBody = juntas[juntas.Count - 1].GetComponent<Rigidbody>();
            for (int i = juntas.Count - 1; i >= 0; i--)
            {
                if (i > juntas.Count - currentPrecision)
                    juntas[i].connectedBody = juntas[i - 1].GetComponent<Rigidbody>();
                else if (juntas.Count - currentPrecision == i)
                    juntas[i].connectedBody = gear.GetComponent<Rigidbody>();
                else
                    juntas[i].gameObject.SetActive(false);
            }
        };
    }
}
}
