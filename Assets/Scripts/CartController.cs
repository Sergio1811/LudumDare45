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
    public Transform frontWheels;
    public Transform backWheels;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = box.transform.position - new Vector3(0, 0.4f, 0);//SEGUIR AL COLLIDER

        if (Input.GetKey(KeyCode.W))
            speed = acceleration;

        float horizontalDir = Input.GetAxis("Horizontal");
        if (horizontalDir != 0)
        {
            int direction;

            if (horizontalDir > 0)
                direction = 1;
            else direction = -1;

            float absoluteDirection = Mathf.Abs(horizontalDir);
            Rotate(direction, absoluteDirection);
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
        speed = 0f;

        currentRotation = Mathf.Lerp(currentRotation, rotation, Time.deltaTime * 4f);
        rotation = 0f;

        cartModel.localEulerAngles = Vector3.Lerp(cartModel.localEulerAngles, new Vector3(0, (horizontalDir * 15), cartModel.localEulerAngles.z), 0.2f);
        /*
        #region Ruedas
        frontWheels.localEulerAngles = new Vector3(0, (horizontalDir * 15), frontWheels.localEulerAngles.z);
        frontWheels.localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
        backWheels.localEulerAngles += new Vector3(0, 0, box.velocity.magnitude / 2);
        #endregion*/
    }

    private void Rotate(int _direction, float _absoulteDir)
    {
        rotation = steering * _direction * _absoulteDir;
    }

    private void FixedUpdate()
    {
        box.AddForce(-cartModel.transform.right * currentSpeed, ForceMode.Acceleration);//forward acceleration
        box.AddForce(Vector3.down * gravity, ForceMode.Acceleration);//gravedad

        //giro
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotation, 0), Time.deltaTime * 5f);

        RaycastHit tocandoSuelo;
        RaycastHit proximoSuelo;
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out tocandoSuelo, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out proximoSuelo, 2.0f, layerMask);

        cartNormal.up = Vector3.Lerp(cartNormal.up, proximoSuelo.normal, Time.deltaTime * 8.0f);
        cartNormal.Rotate(0, transform.eulerAngles.y, 0);//girar en el aire?


    }
}

