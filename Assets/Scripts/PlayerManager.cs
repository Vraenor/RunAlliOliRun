
﻿﻿using UnityEngine;

using System;
using System.Collections;

[Serializable]

public class PlayerManager
{

    //public Color m_PlayerColor;
    public Transform m_SpawnPoint;
    [HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public GameObject waypoint;
    [HideInInspector]
    public int waypointNumber;
    [HideInInspector]
    public int lapNumber = 1;


    //Control del movimiento y del canavas
    private NauMov m_Movement;
    private GameObject m_CanvasGameObject;

    public void Setup()//asignacion de variables de control
    {
        m_Movement = m_Instance.GetComponent<NauMov>();
        m_Movement.lastWP = waypointNumber;
        m_Movement.lastWaypoint = waypoint;
        m_Movement.currentLap = lapNumber;


        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        /* for (int i = 0; i < renderers.Length; i++)
         {
             renderers[i].material.color = m_PlayerColor;
         }*/
    }


    public void DisableControl()//desactivado del movimiento
    {
        m_Movement.enabled = false;
        m_CanvasGameObject.SetActive(false);

    }


    public void EnableControl()//activado del movimiento
    {
        m_Movement.enabled = true;
        m_CanvasGameObject.SetActive(true);
    }



    public void Reset()//reinicio de la nave
    {
        if (waypoint == null)
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;
        }
        else
        {
            m_Instance.transform.position = waypoint.transform.position;
            m_Instance.transform.rotation = waypoint.transform.rotation;
        }
        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}
