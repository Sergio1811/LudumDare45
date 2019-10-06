using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAQuarterback : MonoBehaviour
{
    public enum State {Idle, Searching, Attack, Dead};
    State m_CurrentState = State.Idle;
    NavMeshAgent m_NMAgent;
    Vector3 m_PlayerPosition;
    Vector3 m_NextDestino;
    public GameObject[] m_PointsOfMov;
    public LayerMask m_LayerMask;
    public Animator myAnimator;
    public Transform copyPlayerPos;
   //    public Transform playerT;

    void Start()
    {
        m_NMAgent = this.GetComponent<NavMeshAgent>();
    }

   
    void Update()
    {
        print(m_CurrentState);
        switch (m_CurrentState)
        {
            case State.Idle:
                ChangeState(m_CurrentState, State.Searching);
                break;

            case State.Searching:

                RaycastHit l_Hit;
                Ray ray = new Ray(gameObject.transform.position, (copyPlayerPos.position - gameObject.transform.position).normalized);
                Debug.DrawRay(gameObject.transform.position, copyPlayerPos.position - gameObject.transform.position);

                if (Physics.Raycast(ray, out l_Hit, m_LayerMask))
                {
                    print("Detectado");
                    print(l_Hit.transform.name);
                    if(l_Hit.transform.tag == "Player")
                    {
                        print("player");
                        m_PlayerPosition = GameManager.Instance.m_Player.transform.position;
                       // ChangeState(m_CurrentState, State.Attack);
                        StartCoroutine(WaitSeconds(0.2f));
                    }
                }

                if(Vector3.Distance(m_NextDestino, m_NMAgent.transform.position)<1.0f)
                {
                    NearerPointToBoth();
                    m_NMAgent.SetDestination(m_NextDestino);
                }

                break;
            case State.Attack:
                 
                break;
            case State.Dead:

                break;

        }
    }

    public void ChangeState(State l_CurrentState, State l_NextState)
    {
        switch (l_CurrentState)
        {
            case State.Idle:

                break;
            case State.Searching:
                break;
            case State.Attack:
                break;
            case State.Dead:
                //DO NOTHING
                break;

        }

        switch (l_NextState)
        {
            case State.Searching:
                NearerPointToBoth();
                m_NMAgent.SetDestination(m_NextDestino);
                break;
            case State.Attack:
                m_NMAgent.speed *= 5;
                m_NMAgent.angularSpeed *= 5;
                m_NMAgent.acceleration *= 2;
                m_NMAgent.SetDestination(m_PlayerPosition);
                break;
            case State.Dead:
                m_NMAgent.isStopped=true;
                m_NMAgent.velocity = Vector3.zero;
                this.gameObject.GetComponent<Collider>().enabled = false;
                myAnimator.SetBool("Dead", true);
                break;

        }
        m_CurrentState = l_NextState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Suelo" && m_CurrentState==State.Attack)
        {
            //SOMETHINGHAPPEN
            ChangeState(m_CurrentState, State.Dead);
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ChangeState(m_CurrentState, State.Attack);
    }

    public void NearerPointToBoth()
    {
        float l_MinDistance=1000;
        float l_Distance =0;
        foreach (var item in m_PointsOfMov)
        {
            l_Distance =Vector3.Distance(item.transform.position, this.transform.position)+Vector3.Distance(item.transform.position, GameManager.Instance.m_Player.transform.position);

            if (l_Distance < l_MinDistance && Vector3.Distance(item.transform.position, this.transform.position)>1.0f)
            {
                l_MinDistance = l_Distance;
                m_NextDestino = item.transform.position;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, (copyPlayerPos.position - this.transform.position));
    }*/
}
