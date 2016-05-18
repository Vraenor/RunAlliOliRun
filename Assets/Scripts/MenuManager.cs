using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //cargar el nivel elegido
    public void CargaNivel(string Nombre)
    {
        SceneManager.LoadScene(Nombre);
    }
}
