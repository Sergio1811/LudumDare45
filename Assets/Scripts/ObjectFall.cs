using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public float m_ExplosionForce;
    public float m_Duration;
    public float m_Force;
    public float m_MaxDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ObjectFalling();
        }
    }

    public void ObjectFalling()
    {
        Rigidbody[] l_RigidBodies = GetComponentsInChildren<Rigidbody>();
        Vector3 l_Direction = (GameManager.Instance.m_Player.transform.position - this.transform.position).normalized;
        print(l_Direction); 
        foreach (Rigidbody item in l_RigidBodies)
        {
            item.AddForce(l_Direction*m_ExplosionForce, ForceMode.Impulse);
        }


        StartCoroutine(GameManager.Instance.m_CameraShake.Shake(m_Duration,m_Force, m_MaxDistance));
    }
}
