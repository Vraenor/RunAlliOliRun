using UnityEngine;
using System.Collections;

public class DirectionArrow : MonoBehaviour {

    public Transform target;
    private GameObject player;
    public NauMov nau;
    public string cadena;
    public int numero;
    public string aux;

    // Not used directly
    private Vector3 positionVelocity;

    private void Awake()
    {
        target = GameObject.Find("Waypoint1").transform;
        transform.LookAt(target.position);
        transform.Rotate(30f, 0f, 0f);

    }

    void FixedUpdate()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        nau = player.GetComponent<NauMov>();
        //target = nau.lastWaypoint.transform;

        if (nau.lastWaypoint != null)
        {
            cadena = nau.lastWaypoint.name.Remove(0, 8);
            numero = int.Parse(cadena) + 1;
            Debug.Log(numero);
            aux = "Waypoint" + numero;
            Debug.Log("\n"+ aux);
            target = GameObject.Find(aux).transform;
            transform.LookAt(target.position);
            transform.Rotate(30f, 0f, 0f);

        }
    }
}
