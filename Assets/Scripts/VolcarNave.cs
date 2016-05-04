using UnityEngine;
using System.Collections;

public class VolcarNave : MonoBehaviour
{

    public GameObject nave;
    public NauMov mov;
    public GameObject waypoint;
    string aux = "01";

    // Use this for initialization
    void Awake()
    {
        nave = GameObject.FindGameObjectWithTag("Antivuelco");
        mov = GetComponentInParent<NauMov>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Waypoint"))
        {
            aux = other.name.Remove(0, 8);
        }

        if (other.gameObject.tag.Equals("Terrain"))
        {
            waypoint = GameObject.Find("Waypoint" + aux);
            nave.GetComponentInParent<Transform>().position = waypoint.transform.position;
        }
    }
}
