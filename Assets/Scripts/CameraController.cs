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
    public float maxDistanceReal;
    public float minDistanceReal;


    private float currentSpeed = 50;
    private float limitSpeed = 1.5f;
    public float acceleratePerSecond = 10f;

    Vector3 m_DesiredPosition;
    public float m_OffsetOnCollision;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        maxDistanceReal = maxDistance;
        minDistanceReal = minDistance;
        currentSpeed = 100;
        myCamera.transform.forward = (looker.position - gameObject.transform.position).normalized;
        transformPositionCamera.position = transform.position;
        goToPosition.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //CameraColliding();
    }

    private void FixedUpdate()
    {
        float distance = (player.gameObject.transform.position - myCamera.transform.position).magnitude;
        if (!player.girando && !going && (distance <= maxDistance && distance >= minDistance))
        {
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, transformPositionCamera.position, 5 * Time.deltaTime);
        }
        else if (player.girando || distance < maxDistance || distance > minDistance)
        {
            myCamera.transform.position -= (player.gameObject.transform.position - myCamera.transform.position).normalized * Time.deltaTime;

            if (distance > maxDistance || distance < minDistance)
            {
                if (distance < minDistance)
                {
                    distance = minDistance;
                    myCamera.transform.position = player.gameObject.transform.position + (myCamera.transform.position - player.gameObject.transform.position).normalized * minDistance;
                }
                else if (distance > maxDistance)
                {
                    distance = maxDistance;
                    myCamera.transform.position = player.gameObject.transform.position + (myCamera.transform.position - player.gameObject.transform.position).normalized * maxDistance;
                }
            }

        }
        myCamera.transform.forward = (looker.position - gameObject.transform.position).normalized;
    }


    public void CameraColliding()
    {
        RaycastHit l_RaycastHit;
        Vector3 l_Direction = looker.transform.position - myCamera.transform.position;
        Ray l_Ray = new Ray(goToPosition.position, l_Direction);
        if (Physics.Raycast(l_Ray, out l_RaycastHit, layerMask))
        {
            print("Ray");
            if (l_RaycastHit.collider.gameObject.tag != "Player")
            {
                transformPositionCamera.position = l_RaycastHit.point + l_Direction.normalized * 1f;
                float distance = (looker.position - l_RaycastHit.point + l_Direction.normalized * 1f).magnitude;

                if (distance > 2)
                    maxDistance = distance;

            }
            else
            {
                minDistance = minDistanceReal;
                maxDistance = maxDistanceReal;
                transformPositionCamera.position = goToPosition.position;
            }


        }
        else maxDistance = maxDistanceReal;


    }
}
