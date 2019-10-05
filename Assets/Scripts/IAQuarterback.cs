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

    public Transform playerT;

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

                if(Physics.Raycast(this.transform.position, (playerT.position-this.transform.position), out l_Hit, m_LayerMask))
                {
                    print("Detectado");
                    if(l_Hit.transform.tag =="Player")
                    {
                        print("player");
                        m_PlayerPosition = GameManager.Instance.m_Player.transform.position;
                       // ChangeState(m_CurrentState, State.Attack);
                        StartCoroutine(WaitSeconds(2));
                    }
                }

                if(Vector3.Distance(m_NextDestino, m_NMAgent.transform.position)<1.0f)
                {
                    NearerPointToBoth();
                    m_NMAgent.SetDestination(m_NextDestino);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    m_PlayerPosition = GameManager.Instance.m_Player.transform.position;
                    StartCoroutine(WaitSeconds(0.2f));
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
                m_NMAgent.speed *= 10;
                m_NMAgent.angularSpeed *= 5;
                m_NMAgent.acceleration *= 3;
                m_NMAgent.SetDestination(m_PlayerPosition);
                break;
            case State.Dead:
                m_NMAgent.isStopped=true;
                //CHANGEANIMATION
                break;

        }
        m_CurrentState = l_NextState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && m_CurrentState==State.Attack)
        {
            //SOMETHINGHAPPEN
            ChangeState(m_CurrentState, State.Dead);
        }
        else if( m_CurrentState==State.Attack)
        {
            ChangeState(m_CurrentState, State.Dead);
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        print(Time.time);
        yield return new WaitForSeconds(seconds);
        print(Time.time);
        ChangeState(m_CurrentState, State.Attack);
    }

    public void NearerPointToBoth()
    {
        float l_MinDistance=1000;
        float l_Distance =0;
        foreach (var item in m_PointsOfMov)
        {
            l_Distance =Vector3.Distance(item.transform.position, this.transform.position)+Vector3.Distance(item.transform.position, GameManager.Instance.transform.position);

            if (l_Distance < l_MinDistance && Vector3.Distance(item.transform.position, this.transform.position)>1.0f)
            {
                l_MinDistance = l_Distance;
                m_NextDestino = item.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, (GameManager.Instance.m_Player.transform.position - this.transform.position));
    }
}
