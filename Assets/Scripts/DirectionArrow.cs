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
            aux = "Waypoint" + numero;
            target = GameObject.Find(aux).transform;
            transform.LookAt(target.position, gameObject.transform.up);
        }
    }
}
