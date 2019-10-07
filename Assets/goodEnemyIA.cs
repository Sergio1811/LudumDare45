using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class goodEnemyIA : MonoBehaviour
{

    public NavMeshAgent m_NavMeshAgent;
    public enum TState
    {
        PATROL = 0,
        ALERT,
        CHASE,
        ATTACK,
        HIT,
        DIE
    }
    public TState m_State;
    public List<Transform> m_PatrolPositions;
    float m_CurrentTime = 0.0f;
    int m_CurrentPatrolPositionId = -1;
    public GameManager m_GameManager;
    public float m_MinDistanceToAlert = 6.0f;
    public LayerMask m_CollisionLayerMask;
    public float m_MinDistanceToAttack = 4.0f;
    public float m_MaxDistanceToAttack = 8.0f;
    public float m_MaxDistanceToPatrol = 15.0f;
    public float m_ConeAngle = 60.0f;
    public float m_LerpAttackRotation = 0.6f;
    const float m_MaxLife = 100.0f;
    public float m_Life = m_MaxLife;
    [HideInInspector]
    public bool m_Hit = false;

    public Animator myAnimator;

    private void Start()
    {
        //SetDieState();
        SetPatrolState();
    }

    void Update()
    {
        m_CurrentTime += Time.deltaTime;

        switch (m_State)
        {
            case TState.PATROL:
                UpdatePatrolState();
                break;
            case TState.CHASE:
                UpdateChaseState();
                break;
            case TState.DIE:
               
                break;
        }

        /*if (m_Life <= 0)
            SetDieState();*/
    }

    void SetPatrolState()
    {
        m_State = TState.PATROL;
        m_CurrentTime = 0.0f;
        m_CurrentPatrolPositionId = GetClosestPatrolPositionId();
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.speed = 50.0f;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
        m_Hit = false;
    }

    void SetChaseState()
    {
        m_State = TState.CHASE;
        //SetNextChasePosition();
        m_NavMeshAgent.SetDestination(GameManager.Instance.m_Player.transform.position);
        m_CurrentTime = 0.0f;
        m_NavMeshAgent.speed *= 3;
        m_Hit = false;
    }

    void SetAttackState()
    {
        m_State = TState.ATTACK;
        m_NavMeshAgent.isStopped = true;
        m_CurrentTime = 0.0f;
        m_Hit = false;
    }

    void SetDieState()
    {
        m_State = TState.DIE;
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.speed = 0f;
        //StartCoroutine(FadeGameObject());
        //m_Hit = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        myAnimator.SetBool("Dead", true);
    }

    void UpdatePatrolState()
    {
        if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
            MoveToNextPatrolPosition();

        if (SeesPlayer())
        {
            m_State = TState.CHASE;
            //SetNextChasePosition();
            m_NavMeshAgent.SetDestination(GameManager.Instance.m_Player.transform.position);
            m_CurrentTime = 0.0f;
            m_NavMeshAgent.speed = 50000.0f;

            //SetChaseState();
        }

        if (m_Hit)
            SetDieState();
    }

    void UpdateChaseState()
    {
        /*if (GetSqrDistanceXZToPosition(GameManager.Instance.m_Player.transform.position) < m_MinDistanceToAttack * m_MinDistanceToAttack)
            SetAttackState();*/
        if (GetSqrDistanceXZToPosition(GameManager.Instance.m_Player.transform.position) > m_MaxDistanceToPatrol * m_MaxDistanceToPatrol)
            SetPatrolState();

        if (m_NavMeshAgent.isStopped == false)
            SetNextChasePosition();

        if (m_Hit)
            SetDieState();
    }

    void SetNextChasePosition()
    {
        m_NavMeshAgent.isStopped = false;
        Vector3 l_Destination = GameManager.Instance.m_Player.transform.position - transform.position;
        float l_Distance = l_Destination.magnitude;
        l_Destination /= l_Distance;
        l_Destination = transform.position + l_Destination;
        m_NavMeshAgent.SetDestination(l_Destination);
    }

    int GetClosestPatrolPositionId()
    {
        int l_Closest = 0;
        float l_Nearby = 10000000;
        for (int i = 0; i < m_PatrolPositions.Count; i++)
        {
            if (GetSqrDistanceXZToPosition(m_PatrolPositions[i].transform.position) < l_Nearby * l_Nearby)
            {
                l_Nearby = GetSqrDistanceXZToPosition(m_PatrolPositions[i].transform.position);
                l_Closest = i;
            }
        }
        return l_Closest;
    }

    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolPositionId;
        if (m_CurrentPatrolPositionId >= m_PatrolPositions.Count)
            m_CurrentPatrolPositionId = 0;
        m_NavMeshAgent.SetDestination(m_PatrolPositions[m_CurrentPatrolPositionId].position);
    }

    bool SeesPlayer()
    {
        Vector3 l_Direction = (GameManager.Instance.m_Player.transform.position) - transform.position;
        Ray l_Ray = new Ray(transform.position, l_Direction);
        float l_Distance = l_Direction.magnitude;
        l_Direction /= l_Distance;
        bool l_Collides = Physics.Raycast(l_Ray, l_Distance, m_CollisionLayerMask);
        float l_DotAngle = Vector3.Dot(l_Direction, transform.forward);

        Debug.DrawRay(transform.position, l_Direction * l_Distance, l_Collides ? Color.red : Color.yellow);
        return !l_Collides && l_DotAngle > Mathf.Cos(m_ConeAngle * 0.5f * Mathf.Deg2Rad);
    }

    float GetSqrDistanceXZToPosition(Vector3 l_PlayerPosition)
    {
        return new Vector3(l_PlayerPosition.x - this.gameObject.transform.position.x, 0, l_PlayerPosition.z - this.gameObject.transform.position.z).sqrMagnitude;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_Hit = true;
        }
    }
}
