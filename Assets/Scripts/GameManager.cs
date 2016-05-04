using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public float m_StartDelay = 5f;
    public float m_EndDelay = 3f;
    public Text m_MessageText;
    public IaManager[] m_Pods;
    public GameObject[] m_PodPrefab;


    private int m_LapNumber;
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
            m_Pods[i].m_PlayerNumber = i + 1;
            m_Pods[i].Setup();
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


        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        yield return null;
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


    private void ResetAllTanks()
    {

    }

    private void EnableTankControl()
    {
   
    }

    private void DisableTankControl()
    {

    }

}
