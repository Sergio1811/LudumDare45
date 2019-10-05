using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform transformPositionCamera;
    public Camera myCamera;
    public Transform positionToTransforms;
    public List<Transform> transformsCamera = new List<Transform>();
    public CartController player;
    public Transform goToPosition;
    public Transform looker;
    private bool going = false;

    public float minDistance = 4;
    public float maxDistance = 8;

    private float currentSpeed = 50;
    private float limitSpeed = 1.5f;
    public float acceleratePerSecond = 10f;

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        minDistance = (looker.position - transformPositionCamera.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!player.girando && !going)
        {
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, transformPositionCamera.transform.position, currentSpeed * Time.deltaTime);
        }
        else if(player.girando)
        {
            if(!going)
                currentSpeed = 0;
            float distance = (player.gameObject.transform.position - myCamera.transform.position).magnitude;
            limitSpeed = 2f * (player.gameObject.transform.position - myCamera.transform.position).magnitude;

            if (distance > maxDistance || distance < minDistance)
            {
                if (distance < minDistance)
                {
                    distance = minDistance;
                    limitSpeed = 2f * (player.gameObject.transform.position - myCamera.transform.position).magnitude;
                }
                else if (distance > maxDistance)
                {
                    distance = maxDistance;
                    currentSpeed = 20;
                    limitSpeed = 3.5f * (player.gameObject.transform.position - myCamera.transform.position).magnitude;

                }
            }
            goToPosition.position = (transformPositionCamera.transform.position - player.gameObject.transform.position).normalized * distance + player.gameObject.transform.position; //cambiar chequeando la posición
            going = true;
            //limitSpeed = 3.5f * (player.gameObject.transform.position - myCamera.transform.position).magnitude;


        }
        if (going)
        {
            currentSpeed += acceleratePerSecond * Time.deltaTime;
            if (currentSpeed > limitSpeed)
                currentSpeed = limitSpeed;
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, goToPosition.transform.position, currentSpeed * Time.deltaTime);
            if ((myCamera.transform.position - goToPosition.transform.position).magnitude < 0.05f)
            {
                going = false;
            }
        }
        myCamera.transform.forward = (looker.position - gameObject.transform.position).normalized;
    }

    private bool CanGoToTransform(Transform _trans)
    {
        bool viable = true;

        positionToTransforms = _trans;
        positionToTransforms.transform.forward = (player.gameObject.transform.position - positionToTransforms.transform.position).normalized;
        foreach (Transform item in transformsCamera)
        {

        }

        return viable;
    }
}
