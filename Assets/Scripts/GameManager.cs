using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    //Variables para el inicio o fin de partida

    public float m_StartDelay = 5f;
    public float m_EndDelay = 3f;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    //Manager de la IA y el jugador
    public IaManager[] m_Pods;
    public PlayerManager[] m_Player;

    //Lista de naves y waypoints
    public GameObject[] m_PodPrefab;
    public GameObject[] waypoints;
    //Variables para el control de vuelta y el respawn de jugador

    public int m_NumberofPlayers = 0;
    public GameObject lastWaypoint;
    public int numberWaypoint = 0;
    public int m_LapNumber;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        SpawnAllPods();
        StartCoroutine(GameLoop());
    }

    //Metodo para generar las naves al inicio de la carrera
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
            m_Player[i].m_Instance = Instantiate(m_PodPrefab[i + 1], m_Player[i].m_SpawnPoint.position, m_Player[i].m_SpawnPoint.rotation) as GameObject;

            m_Player[i].Setup();
            m_NumberofPlayers++;
        }
    }

    //Metodo que hace respawn al player
    public void RespawnPlayer(GameObject posicion, int waypoint)
    {
        if (m_NumberofPlayers == 0)
        {
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

    //loop de juego
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

    }

    //inicio de ronda
    private IEnumerator RoundStarting()
    {
        ResetAllPods();
        DisablePodControl();
        yield return m_StartWait;
    }

    //desactivar controles
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

    //reinicio de las naves
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

    //mantener la ronda
    private IEnumerator RoundPlaying()
    {
        EnablePodControl();
        while (m_LapNumber < 4) {
            yield return null;
        }
    }

    //activar controles
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

    //fin de ronda
    private IEnumerator RoundEnding()
    {
        DisablePodControl();
        yield return m_EndWait;
    }
    //mensaje de fin de partida
    private string EndMessage()
    {
        string message = "DRAW!";
        return message;
    }

    //control si hay que hacerle respawn al player
    void FixedUpdate()
    {
        if (m_NumberofPlayers < 1)
        {
            RespawnPlayer(lastWaypoint, numberWaypoint);
        }
    }
}
