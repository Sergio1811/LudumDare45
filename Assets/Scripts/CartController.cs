using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{

    public Transform cartModel;
    public Rigidbody box;
    public Transform cartNormal;

    float speed;
    float currentSpeed;
    float rotation;
    float currentRotation;

    [Header("Parameters")]
    public float steering = 80f;
    public float acceleration = 30f;
    public float gravity = 10f;
    public LayerMask layerMask;

    [Header("Model Parts")]
    public Transform ruedasDelanteras;
    public Transform ruedasTraseras;

    public bool girando = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = box.transform.position;//SEGUIR AL COLLIDER
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = box.transform.position;//SEGUIR AL COLLIDER

        if (Input.GetKey(KeyCode.W))
        {
            speed = acceleration;


            for (int i = 0; i < 2; i++)
            {
                ruedasDelanteras.GetChild(i).localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
                ruedasTraseras.GetChild(i).localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed -= acceleration / 2;


            for (int i = 0; i < 2; i++)
            {
                ruedasDelanteras.GetChild(i).localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
                ruedasTraseras.GetChild(i).localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
            }
        }

        float horizontalDir = Input.GetAxis("Horizontal");
        if (horizontalDir != 0)
        {
            int direction = 0;
            girando = true;

            if (horizontalDir > 0)
                direction = 1;
            else if (horizontalDir < 0)
                direction = -1;

            float absoluteDirection = Mathf.Abs(horizontalDir);
            Rotate(direction, absoluteDirection);
        }
        else
            girando = false;

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
        speed = 0f;

        currentRotation = Mathf.Lerp(currentRotation, rotation, Time.deltaTime * 4f);
        rotation = 0f;

        
        ruedasDelanteras.localEulerAngles = new Vector3(-(horizontalDir * 20), 0, ruedasDelanteras.localEulerAngles.z);//gira rueda
        ruedasTraseras.localEulerAngles = new Vector3(-(horizontalDir * 10), 0, ruedasTraseras.localEulerAngles.z);//gira rueda

    }

    private void Rotate(int _direction, float _absoulteDir)
    {
        rotation = (steering * _direction) * _absoulteDir;
    }

    private void FixedUpdate()
    {
        box.AddForce(cartModel.transform.up * currentSpeed, ForceMode.Acceleration);//forward acceleration
        //box.AddForce(Vector3.down * gravity, ForceMode.Acceleration);//gravedad

        //giro
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotation, 0), Time.deltaTime * 5f);
        box.transform.eulerAngles = transform.eulerAngles;

        RaycastHit proximoSuelo;
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out proximoSuelo, 2.0f, layerMask);



    }
}

