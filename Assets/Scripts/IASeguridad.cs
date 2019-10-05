using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IASeguridad : MonoBehaviour
{

    public enum State { Searching, Attack, Dead };
    State m_CurrentState = State.Searching;
    NavMeshAgent m_NMAgent;
    void Start()
    {
        m_NMAgent = this.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        switch (m_CurrentState)
        {
            case State.Searching:

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
            case State.Searching:
                break;
            case State.Attack:
                break;
            case State.Dead:
                break;
            default:
                break;
        }

        switch (l_NextState)
        {
            case State.Searching:
                break;
            case State.Attack:
                break;
            case State.Dead:
                break;
            default:
                break;
        }
        m_CurrentState = l_NextState;
    }
}
