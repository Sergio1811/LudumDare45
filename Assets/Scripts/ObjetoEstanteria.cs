using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoEstanteria : MonoBehaviour
{
    public float ancho;
    public float hondo;
    public float alto;
    public float puntuacion;
    public bool caido = false;
    float currentTime = 0;
    float maxTime;
    public Rigidbody rb;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        maxTime = Random.Range(20, 60);
    }

    private void Update()
    {
        if(caido)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
                PequenoHastaMorir();
        }
    }

    public void PequenoHastaMorir()
    {
        if(gameObject.transform.localScale.x < 0.05f)
            Destroy(gameObject);
        gameObject.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
    }
}
