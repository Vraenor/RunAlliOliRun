using UnityEngine;
using System.Collections;

public class WaypointTrigger : MonoBehaviour {

    public GameObject enemy;
    IAMov ia;

    // Use this for initialization
    void Awake () {

        enemy = GameObject.FindGameObjectWithTag("Enemy");

	}
	
	// Update is called once per frame
	void Update () {

    }

    void onTriggerEnter(Collider other)
    {

        if (other.gameObject == enemy)
        {
            ia = enemy.GetComponent<IAMov>();
            ia.wayPointIndex++;
        }
    }
}
