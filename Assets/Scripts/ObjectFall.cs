using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    public float m_ExplosionForce;
    public float m_Duration;
    public float m_Force;
    public float m_MaxDistance;
    private List<List<PakageObjects>> objetosEstanterias = new List<List<PakageObjects>>();
    /*private List<ObjetoEstanteria> objetosEstanterias2 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias3 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias4 = new List<ObjetoEstanteria>();
    private List<ObjetoEstanteria> objetosEstanterias5 = new List<ObjetoEstanteria>();*/

    public List<PakageObjects> PoolObjetos = new List<PakageObjects>();

    public List<Transform> estanterias = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            estanterias.Add(gameObject.transform.GetChild(i));
        }
    }

    private void Start()
    {

        for (int i = 0; i < estanterias.Count; i++)
        {
            objetosEstanterias.Add(new List<PakageObjects>());
            if(PoolObjetos.Count > 0)
                LlenaEstanteria(objetosEstanterias[objetosEstanterias.Count - 1], estanterias[i]);
        }

        /*LlenaEstanteria(objetosEstanterias, estanterias[0]);
        LlenaEstanteria(objetosEstanterias2, estanterias[1]);
        LlenaEstanteria(objetosEstanterias3, estanterias[2]);
        LlenaEstanteria(objetosEstanterias4, estanterias[3]);
        LlenaEstanteria(objetosEstanterias5, estanterias[4]);*/

    }

    private void LlenaEstanteria(List<PakageObjects> _list, Transform _estanteria)
    {
        float distance = 0;
        bool lleno = false;
        int random = Random.Range(0, PoolObjetos.Count);
        bool onlyOne = false;
        int count = 0;
        while (!lleno)
        {
            count++;
            distance += 3.5f;
            if (!onlyOne)
            {
                Random.InitState(Mathf.RoundToInt(Time.time));
                random = Random.Range(0, PoolObjetos.Count);
            }
            else onlyOne = false;

            _list.Add(Instantiate(PoolObjetos[random], _estanteria));
            distance += _list[_list.Count - 1].transform.GetChild(0).GetComponent<ObjetoEstanteria>().ancho * 0.001f;
            _list[_list.Count - 1].transform.localPosition = new Vector3(0,0, distance*0.1f);
            //_list[_list.Count - 1].transform.parent = _estanteria;

            distance += 0.5f;
            int i = 0;
            foreach (PakageObjects item in PoolObjetos)
            {
                if (item.transform.GetChild(0).GetComponent<ObjetoEstanteria>().ancho * 0.01 + distance + 3.5f >= 10)
                {
                    lleno = true;
                    break;
                }
                else
                    i++;
            }


            if (i == 1)
                onlyOne = true;

            if (count > 10)
                break;
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
        ObjetoEstanteria[] objEstant = GetComponentsInChildren<ObjetoEstanteria>();
        Vector3 l_Direction = (GameManager.Instance.m_Player.transform.position - this.transform.position).normalized;

        for (int i = 0; i < objEstant.Length; i++)
        {
            objEstant[i].rb.AddForce(l_Direction * Random.Range(m_ExplosionForce * 0.8f, m_ExplosionForce * 1.2f), ForceMode.Impulse);
            objEstant[i].rb.transform.SetParent(null);
            objEstant[i].caido = true;
        }

        StartCoroutine(GameManager.Instance.m_CameraShake.Shake(m_Duration,m_Force, m_MaxDistance));
    }
}
