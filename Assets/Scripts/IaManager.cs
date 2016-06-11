using System;
using UnityEngine;


[Serializable]
public class IaManager
{
    //apariencia de la IA
    //public Color m_PlayerColor;
    public Transform m_SpawnPoint;

    [HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public GameObject[] waypoints;

    //referencais al movimiento y al canvas de la ia
    private IAMov m_Movement;


    public void Setup()//copiamos la lista de waypoints al control de movimiento y pintamos la nave 
    {
        m_Movement = m_Instance.GetComponent<IAMov>();
        m_Movement.waypoints = waypoints;
    }


    public void DisableControl()//desactivar movimiento
    {
        m_Movement.enabled = false;
    }


    public void EnableControl()//activar movimiento
    {
        m_Movement.enabled = true;
    }


    public void Reset()//reinicio de la nave
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        //m_Instance.SetActive(false);
        //m_Instance.SetActive(true);
    }
}