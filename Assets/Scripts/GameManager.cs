using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
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
        m_Player = GameObject.Find("Cart");
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
        if (points + _points > 0)
            points += _points;
        else
            points = 0;

        TextManager.Instance.UpdateText();
    }

}
