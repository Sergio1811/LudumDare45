using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public float m_ExplosionForce;
    public float m_Duration;
    public float m_Force;
    public float m_MaxDistance;
    private List<ObjetoEstanteria> objetosEstanterias = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias2 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias3 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias4 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias5 = new List<ObjetoEstanteria>();

    public List<ObjetoEstanteria> PoolObjetos = new List<ObjetoEstanteria>();

    public List<Transform> estanterias = new List<Transform>();

    private void Start()
    {
        LlenaEstanteria(objetosEstanterias, estanterias[0]);
        LlenaEstanteria(objetosEstanterias2, estanterias[1]);
        LlenaEstanteria(objetosEstanterias3, estanterias[2]);
        LlenaEstanteria(objetosEstanterias4, estanterias[3]);
        LlenaEstanteria(objetosEstanterias5, estanterias[4]);

    }

    private void LlenaEstanteria(List<ObjetoEstanteria> _list, Transform _estanteria)
    {
        float distance = 0;
        bool lleno = false;
        int random = Random.Range(0, PoolObjetos.Count);
        bool onlyOne = false;
        while (!lleno)
        {
            distance -= 0.5f;
            if (!onlyOne)
            {
                Random.InitState(Mathf.RoundToInt(Time.time));
                random = Random.Range(0, PoolObjetos.Count);
            }
            else onlyOne = false;

            _list.Add(PoolObjetos[random]);
            distance -= _list[_list.Count - 1].ancho;
            _list[_list.Count - 1].transform.position = new Vector3(_estanteria.position.x, _estanteria.position.y, _estanteria.position.z - distance);

            int i = 0;
            foreach (ObjetoEstanteria item in PoolObjetos)
            {
                if (item.ancho + distance >= 100)
                {
                    lleno = true;
                }
                else
                    i++;
            }

            if (i == 1)
                onlyOne = true;
        }
    }


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
            item.AddForce(l_Direction*Random.Range(m_ExplosionForce*0.8f,m_ExplosionForce*1.2f), ForceMode.Impulse);
            item.transform.SetParent(null);
        }


        StartCoroutine(GameManager.Instance.m_CameraShake.Shake(m_Duration,m_Force, m_MaxDistance));
    }
}
