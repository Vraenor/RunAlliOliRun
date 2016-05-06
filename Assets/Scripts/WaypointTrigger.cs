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
        if (other.gameObject.tag.Equals("Enemy"))
        {
            string name = this.name;
            name = name.Remove(0, 8);
            int aux = int.Parse(name);

            other.GetComponent<IAMov>().wayPointIndex = aux;
        }

        if (other.gameObject.tag.Equals("Player"))
        {
            string name = this.name;
            name = name.Remove(0, 8);
            int aux = int.Parse(name);

            other.GetComponent<NauMov>().wPos = GetComponentInParent<Transform>().position;
            other.GetComponent<NauMov>().wRot = GetComponentInParent<Transform>().rotation;
            other.GetComponent<NauMov>().wRot.y = GetComponentInParent<Transform>().rotation.y+180f;


        }
    }
}
