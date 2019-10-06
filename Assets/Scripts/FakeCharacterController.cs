using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCharacterController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    int currentpos = 0;
    void Update()
    {
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        //transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);


    }

    private void OnCollisionEnter(Collision collision)
    {
        print("colliding");
        if (collision.gameObject.GetComponent<ObjectFall>() != null)
            collision.gameObject.GetComponent<ObjectFall>().ObjectFalling();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject);
            currentpos++;

        }
    }
}
