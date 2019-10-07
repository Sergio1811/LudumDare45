using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCharacterController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    int currentpos = 0;
    int numLayer = 0;
    public GameObject[] layers;

    public CartController _cartController;
    bool hit = false;
    float hitTime = 3f;
    float timeRellenado = 0;
    ObjectFall[] estanterias;

    private void Start()
    {
        estanterias = GameObject.FindObjectsOfType<ObjectFall>();
    }
    void Update()
    {
        //transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        //transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        hitTime -= Time.deltaTime;

        if (timeRellenado > 0)
        {
            timeRellenado -= Time.deltaTime;
            if (timeRellenado < 0)
                timeRellenado = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ObjectFall>() != null)
            collision.gameObject.GetComponent<ObjectFall>().ObjectFalling();
        else if(collision.gameObject.GetComponent<CuboEstanteriaScript>() != null)
            collision.gameObject.GetComponent<CuboEstanteriaScript>().ObjectFalling();

        if (hitTime <= 0.0f)
        {
            _cartController.eugenioAnimator.SetTrigger("Hit");
            hitTime = 3f;
            hit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponentInChildren<Collider>().isTrigger = false;
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.transform.parent = this.gameObject.transform;
            GameManager.Instance.sumPoints(30);
            if(GameManager.Instance.points > numLayer * 50 && numLayer < 5)
            {
                if (numLayer > 0)
                    layers[numLayer - 1].SetActive(false);
                layers[numLayer].SetActive(true);
                numLayer++;
            }
        }
        else if(other.gameObject.CompareTag("Meta") && timeRellenado == 0)
        {
            timeRellenado = 15;
            foreach (ObjectFall obj in estanterias)
                obj.Rellenar();
        }
    }
}
