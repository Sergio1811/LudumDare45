using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [HideInInspector]
    public GameObject m_Player;
    public CameraShake m_CameraShake;
    public int random = 1;

    public int points;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }


    }

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        points = 0;
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public void sumPoints(int _points)
    {
        points += _points;
    }

}
