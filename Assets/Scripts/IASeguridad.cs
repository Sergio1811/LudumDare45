using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IASeguridad : MonoBehaviour
{

    public enum State { Idle, Searching, Pursue, Attack, Dead };
    State m_CurrentState = State.Idle;
    NavMeshAgent m_NMAgent;
    Vector3 m_PlayerPosition;
    Vector3 m_NextDestino;
    public GameObject[] m_PointsOfMov;
    public LayerMask m_LayerMask;
    Animator m_Animator;
    public float m_CloseEnough;
    public float m_FarEnough;

    //    public Transform playerT;

    void Start()
    {
        m_NMAgent = this.GetComponent<NavMeshAgent>();
        m_Animator = this.GetComponent<Animator>();
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

                if (Physics.Raycast(gameObject.transform.position + (GameManager.Instance.m_Player.transform.position - gameObject.transform.position).normalized, (GameManager.Instance.m_Player.transform.position - gameObject.transform.position), out l_Hit, m_LayerMask))
                {
                    print("Detectado");
                    print(l_Hit.transform.name);
                    if (l_Hit.transform.tag == "Player")
                    {
                        print("player");
                        m_PlayerPosition = GameManager.Instance.m_Player.transform.position;
                        // ChangeState(m_CurrentState, State.Attack);
                        StartCoroutine(WaitSeconds(0.1f));
                    }
                }

                if (Vector3.Distance(m_NextDestino, m_NMAgent.transform.position) < 1.0f)
                {
                    NearerPointToBoth();
                    m_NMAgent.SetDestination(m_NextDestino);
                }

                break;
            case State.Pursue:
                if(Vector3.Distance(GameManager.Instance.m_Player.transform.position,this.transform.position)<m_CloseEnough)
                {
                    ChangeState(m_CurrentState, State.Attack);
                }
                break;
            case State.Attack:
                // m_Animator.GetCurrentAnimatorStateInfo(0).IsName("")!="Attack"
                ChangeState(m_CurrentState,State.Pursue);
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
            case State.Pursue:
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
            case State.Pursue:
                m_NMAgent.SetDestination(GameManager.Instance.m_Player.transform.position);
                break;
            case State.Attack:
                //Animation Attack
                break;
            case State.Dead:
                m_NMAgent.isStopped = true;
                //CHANGEANIMATION
                break;

        }
        m_CurrentState = l_NextState;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SOMETHINGHAPPEN
            ChangeState(m_CurrentState, State.Dead);
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        print(Time.time);
        yield return new WaitForSeconds(seconds);
        print(Time.time);
        ChangeState(m_CurrentState, State.Pursue);
    }

    public void NearerPointToBoth()
    {
        float l_MinDistance = 1000;
        float l_Distance = 0;
        foreach (var item in m_PointsOfMov)
        {
            l_Distance = Vector3.Distance(item.transform.position, this.transform.position) + Vector3.Distance(item.transform.position, GameManager.Instance.m_Player.transform.position);

            if (l_Distance < l_MinDistance && Vector3.Distance(item.transform.position, this.transform.position) > 1.0f)
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
