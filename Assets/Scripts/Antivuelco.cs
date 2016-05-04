using UnityEngine;
using System.Collections;

public class Antivuelco : MonoBehaviour {

    public GameObject nave;
    public IAMov ia;

	// Use this for initialization
	void Awake ()
    {
        nave = GameObject.FindGameObjectWithTag("Antivuelco");
        ia = GetComponentInParent<IAMov>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Terrain"))
        {
            nave.GetComponentInParent<Transform>().position = ia.waypoints[ia.wayPointIndex - 1].transform.position;
        }

    }
}
