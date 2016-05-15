using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
    public float m_StartDelay = 5f;
    public float m_EndDelay = 3f;
    public IaManager[] m_Pods;
    public PlayerManager[] m_Player;
    public GameObject[] m_PodPrefab;
    public GameObject[] waypoints;
    public int m_NumberofPlayers = 0;
    public GameObject lastWaypoint;
    public int numberWaypoint = 0;
    public int m_LapNumber;

    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;



    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        SpawnAllPods();
        StartCoroutine(GameLoop());
    }

    private void SpawnAllPods()
    {
        for (int i = 0; i < m_Pods.Length; i++)
        {
            m_Pods[i].m_Instance =
                Instantiate(m_PodPrefab[i], m_Pods[i].m_SpawnPoint.position, m_Pods[i].m_SpawnPoint.rotation) as GameObject;
            m_Pods[i].waypoints = waypoints;
            m_Pods[i].Setup();
        }
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].m_Instance =
                Instantiate(m_PodPrefab[i+1], m_Player[i].m_SpawnPoint.position, m_Player[i].m_SpawnPoint.rotation) as GameObject;
            m_Player[i].Setup();
            m_NumberofPlayers++;
        }
    }

    public void RespawnPlayer(GameObject posicion, int waypoint) {
        if (m_NumberofPlayers == 0){
            m_NumberofPlayers++;
            for (int i = 0; i < m_Player.Length; i++)
            {
                m_Player[i].m_Instance =
                    Instantiate(m_PodPrefab[i + 1], posicion.transform.position, posicion.transform.rotation) as GameObject;
                m_Player[i].waypoint = posicion;
                m_Player[i].waypointNumber = waypoint;
                m_Player[i].lapNumber = m_LapNumber;
                m_Player[i].Setup();
                m_Player[i].Reset();
            }
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

    }


    private IEnumerator RoundStarting()
    {
        ResetAllPods();
        DisablePodControl();
        yield return m_StartWait;
    }

    private void DisablePodControl()
    {
        for (int i = 0; i < m_Pods.Length; i++)
        {
            m_Pods[i].DisableControl();
        }
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].DisableControl();
        }
    }

    private void ResetAllPods()
    {
        for (int i = 0; i < m_Pods.Length; i++)
        {
            m_Pods[i].Reset();
        }
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].Reset();
        }
    }

    private IEnumerator RoundPlaying()
    {
        EnablePodControl();
        while (m_LapNumber <= 3) { }
        yield return null;
    }

    private void EnablePodControl()
    {
        for (int i = 0; i < m_Pods.Length; i++)
        {
            m_Pods[i].EnableControl();
        }
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].EnableControl();
        }
    }

    private IEnumerator RoundEnding()
    {
        
        yield return m_EndWait;
    }

    private string EndMessage()
    {
        string message = "DRAW!";
        return message;
    }
    void FixedUpdate() {
        if (m_NumberofPlayers < 1)
        {
            RespawnPlayer(lastWaypoint, numberWaypoint);
        }
    }
}
