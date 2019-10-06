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
        minDistance = (looker.position - transformPositionCamera.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        CameraColliding();
    }

    private void FixedUpdate()
    {
        float distance = (player.gameObject.transform.position - myCamera.transform.position).magnitude;
        if (!player.girando && !going && (distance < maxDistance && distance > minDistance))
        {
            currentSpeed = 22;
            myCamera.transform.position = Vector3.MoveTowards(myCamera.transform.position, transformPositionCamera.transform.position, currentSpeed * Time.deltaTime);
        }
        else if(player.girando || distance < maxDistance || distance > minDistance)
        {
            if(!going)
                currentSpeed = 0;
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
                    currentSpeed = 40;
                    limitSpeed = 5f * (player.gameObject.transform.position - myCamera.transform.position).magnitude;

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

    public void CameraColliding()
    {
        RaycastHit l_RaycastHit;
        Vector3 l_Direction = looker.transform.position - myCamera.transform.position;
        Ray l_Ray = new Ray(myCamera.transform.position, l_Direction);
        if (Physics.Raycast(l_Ray, out l_RaycastHit, layerMask))
        {
            print("Ray");
            if (l_RaycastHit.collider.gameObject.tag != "Player")
            {
                myCamera.transform.position = l_RaycastHit.point + l_Direction.normalized * 0.2f;
                goToPosition.position = l_RaycastHit.point + l_Direction.normalized * 0.2f;
                maxDistance = (looker.position - l_RaycastHit.point + l_Direction.normalized * 0.2f).magnitude;
                CameraColliding();
            }
            else
                maxDistance = maxDistanceReal;
        }
 

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position,(looker.transform.position - myCamera.transform.position));
    }
}
