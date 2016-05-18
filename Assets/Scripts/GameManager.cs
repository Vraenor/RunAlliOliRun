using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

<<<<<<< HEAD
public class GameManager : MonoBehaviour
{
    //Variables para el inicio o fin de partida
=======
public class GameManager : MonoBehaviour {
>>>>>>> origin/master
    public float m_StartDelay = 5f;
    public float m_EndDelay = 3f;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    //Manager de la IA y el jugador
    public IaManager[] m_Pods;
    public PlayerManager[] m_Player;
<<<<<<< HEAD
    //Lista de naves y waypoints
    public GameObject[] m_PodPrefab;
    public GameObject[] waypoints;
    //Variables para el control de vuelta y el respawn de jugador
=======
    public GameObject[] m_PodPrefab;
    public GameObject[] waypoints;
>>>>>>> origin/master
    public int m_NumberofPlayers = 0;
    public GameObject lastWaypoint;
    public int numberWaypoint = 0;
    public int m_LapNumber;
<<<<<<< HEAD
=======

    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;


>>>>>>> origin/master

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        SpawnAllPods();
        StartCoroutine(GameLoop());
    }
<<<<<<< HEAD
    //Metodo para generar las naves al inicio de la carrera
=======

>>>>>>> origin/master
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
<<<<<<< HEAD
                Instantiate(m_PodPrefab[i + 1], m_Player[i].m_SpawnPoint.position, m_Player[i].m_SpawnPoint.rotation) as GameObject;
=======
                Instantiate(m_PodPrefab[i+1], m_Player[i].m_SpawnPoint.position, m_Player[i].m_SpawnPoint.rotation) as GameObject;
>>>>>>> origin/master
            m_Player[i].Setup();
            m_NumberofPlayers++;
        }
    }
<<<<<<< HEAD
    //Metodo que hace respawn al player
    public void RespawnPlayer(GameObject posicion, int waypoint)
    {
        if (m_NumberofPlayers == 0)
        {
=======

    public void RespawnPlayer(GameObject posicion, int waypoint) {
        if (m_NumberofPlayers == 0){
>>>>>>> origin/master
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
<<<<<<< HEAD
    //loop de juego
=======

>>>>>>> origin/master
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
<<<<<<< HEAD
    //desactivar controles
=======

>>>>>>> origin/master
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
<<<<<<< HEAD
    //reinicio de las naves
=======

>>>>>>> origin/master
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
<<<<<<< HEAD
    //mantener la ronda
=======

>>>>>>> origin/master
    private IEnumerator RoundPlaying()
    {
        EnablePodControl();
        while (m_LapNumber <= 3) { }
        yield return null;
    }
<<<<<<< HEAD
    //activar controles
=======

>>>>>>> origin/master
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
<<<<<<< HEAD
    //fin de ronda
=======

>>>>>>> origin/master
    private IEnumerator RoundEnding()
    {

        yield return m_EndWait;
    }
    //mensaje de fin de partida
    private string EndMessage()
    {
        string message = "DRAW!";
        return message;
    }
<<<<<<< HEAD
    //control si hay que hacerle respawn al player
    void FixedUpdate()
    {
=======
    void FixedUpdate() {
>>>>>>> origin/master
        if (m_NumberofPlayers < 1)
        {
            RespawnPlayer(lastWaypoint, numberWaypoint);
        }
    }
}
