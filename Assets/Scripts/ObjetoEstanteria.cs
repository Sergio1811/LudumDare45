﻿using System.Collections;
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
    public List<GameObject> objetos = new List<GameObject>();
    bool firstTime = true;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            objetos.Add(transform.GetChild(i).gameObject);
        }
        rb = gameObject.GetComponent<Rigidbody>();
        maxTime = Random.Range(5, 10);
    }

    private void Update()
    {
        if(firstTime && objetos.Count > 0)
        {
            firstTime = false;
            Random.InitState(GameManager.Instance.random * 2);
            GameManager.Instance.random++;
            int random = Random.Range(0, objetos.Count);
            objetos[random].SetActive(true);

            foreach (GameObject go in objetos)
            {
                if (go != objetos[random])
                    go.SetActive(false);
            }
        }
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
