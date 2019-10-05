using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSphere : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject root;
    public GameObject cart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // transform.position += transform.forward * speed * Time.deltaTime;

        CheckInputs();

    }


    private void CheckInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * speed * Time.deltaTime * 2;
            transform.Rotate(Vector3.up, 10 * Time.deltaTime);
            //transform.RotateAround(cart.transform.position, Vector3.up, -20 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * speed * Time.deltaTime * 2;
            transform.Rotate(Vector3.up, -10 * Time.deltaTime);
            // transform.RotateAround(shoppingCart.transform.position, Vector3.up, 400 * Time.deltaTime);
        }
    }
}
