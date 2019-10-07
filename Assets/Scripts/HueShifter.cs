using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HueShifter : MonoBehaviour
{
    public float Speed = 1;
    private Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
       rend.material.SetColor("_EmissiveColor", Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 0.2f)));
    }
}
