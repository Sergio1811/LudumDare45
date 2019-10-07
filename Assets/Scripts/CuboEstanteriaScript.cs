using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboEstanteriaScript : MonoBehaviour
{
    public float m_ExplosionForce;
    public float m_Duration;
    public float m_Force;
    public float m_MaxDistance;
    private List<PakageObjects> objetosEstanterias = new List<PakageObjects>();
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectFalling()
    {
        ObjetoEstanteria[] objEstant = GetComponentsInChildren<ObjetoEstanteria>();
        Vector3 l_Direction = ((GameManager.Instance.m_Player.transform.position - gameObject.transform.position).normalized / 2  + Vector3.up).normalized;

        for (int i = 0; i < objEstant.Length; i++)
        {
            GameManager.Instance.random++;
            Random.InitState(GameManager.Instance.random);
            objEstant[i].rb.AddForce(l_Direction * Random.Range(m_ExplosionForce * 0.75f, m_ExplosionForce * 1.35f), ForceMode.Impulse);
            objEstant[i].rb.transform.SetParent(null);
            objEstant[i].caido = true;
        }

        //StartCoroutine(GameManager.Instance.m_CameraShake.Shake(m_Duration, m_Force, m_MaxDistance));
    }
}
