using System;
using UnityEngine;


[Serializable]
public class IaManager
{
    //apariencia de la IA
    //public Color m_PlayerColor;
    public Transform m_SpawnPoint;
<<<<<<< HEAD
    [HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public GameObject[] waypoints;
=======
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public GameObject[] waypoints;

>>>>>>> origin/master

    //referencais al movimiento y al canvas de la ia
    private IAMov m_Movement;
    private GameObject m_CanvasGameObject;


    public void Setup()//copiamos la lista de waypoints al control de movimiento y pintamos la nave 
    {
        m_Movement = m_Instance.GetComponent<IAMov>();
        m_Movement.waypoints = waypoints;

        /*MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }*/
    }


    public void DisableControl()//desactivar movimiento
    {
<<<<<<< HEAD
        m_Movement.enabled = false;
        m_CanvasGameObject.SetActive(false);
=======
       m_Movement.enabled = false;
       m_CanvasGameObject.SetActive(false);
>>>>>>> origin/master
    }


    public void EnableControl()//activar movimiento
    {
        m_Movement.enabled = true;
        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()//reinicio de la nave
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}