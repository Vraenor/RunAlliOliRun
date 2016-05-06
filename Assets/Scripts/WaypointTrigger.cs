using UnityEngine;
using System.Collections;

public class WaypointTrigger : MonoBehaviour {

    // Use this for initialization
    void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        string name = this.name;
        name = name.Remove(0, 8);
        int aux = int.Parse(name);

        if (other.gameObject.tag.Equals("Enemy"))
        {
            

            other.GetComponent<IAMov>().wayPointIndex = aux;
        }

        if (other.gameObject.tag.Equals("Player"))
        {
            if ((aux == other.GetComponent<NauMov>().lastWP+1) || (aux ==1 && other.GetComponent<NauMov>().lastWP == 120))
            {
                //Debug.Log("Waypoint en el que estoy " + name + " Waypoint anterior" + other.GetComponent<NauMov>().lastWP);
                other.GetComponent<NauMov>().wPos = GetComponentInParent<Transform>().position;
                other.GetComponent<NauMov>().wRot = GetComponentInParent<Transform>().rotation;
                other.GetComponent<NauMov>().wRot.y = GetComponentInParent<Transform>().rotation.y + 180f;
                other.GetComponent<NauMov>().lastWP = aux;
            }
            
        }
    }
}
