using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public float m_ExplosionForce;

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
            item.AddForce(l_Direction*m_ExplosionForce);
        }
    }
}
