using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject m_Canvas;

    public GameObject[] m_Clothes;
    public GameObject m_Panatalones;
    public GameObject m_Calzones;
    public Transform m_SpawnPoint;
    public GameObject m_PLayer;

    public void ActivateCanvas()
    {
        m_Canvas.SetActive(true);
    }
    public void DesactivateCanvas()
    {
        m_Canvas.SetActive(false);
        m_PLayer.transform.position = m_SpawnPoint.position;
        m_PLayer.transform.rotation = m_SpawnPoint.rotation;
        
    }
    public void ChargeCatalogue()
    {
        SceneManager.LoadScene(2);
    }

    public void ChargeMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChargeGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Upgrade()
    {
        
        int number = Random.Range(0, m_Clothes.Length);
        bool allCompleted = true;


        if (!m_Clothes[number].activeSelf )
        {
            m_Clothes[number].SetActive(true);
          
        }
        else
        {
            foreach (var item in m_Clothes)
            {
                if (!item.activeSelf)
                    allCompleted = false;
            }
            if (!allCompleted)
                Upgrade();
        }
        if (m_Panatalones.activeSelf)
            m_Calzones.SetActive(false);
    }

}
