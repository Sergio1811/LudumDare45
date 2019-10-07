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

    public SceneManagement m_SceneManager;

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

        if (collision.gameObject.tag == "Enemy" )
        {
            GameManager.Instance.sumPoints(-10);
            TextManager.Instance.UpdateText();
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
            GameManager.Instance.sumPoints((int)other.gameObject.GetComponent<ObjetoEstanteria>().puntuacion);
            TextManager.Instance.UpdateText();
            if(GameManager.Instance.points > numLayer * 20 && numLayer < 5)
            {
                if (numLayer > 0)
                    layers[numLayer - 1].SetActive(false);
                layers[numLayer].SetActive(true);
                numLayer++;
            }
        }
        else if(other.gameObject.CompareTag("Meta") && timeRellenado == 0)
        {
            m_SceneManager.ActivateCanvas();
            
            foreach (ObjectFall obj in estanterias)
                obj.Rellenar();
            timeRellenado = 15;

        }
    }

    
}
