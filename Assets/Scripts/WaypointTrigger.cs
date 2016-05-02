using UnityEngine;
using System.Collections;

public class WaypointTrigger : MonoBehaviour {

    public GameObject enemy;
    public IAMov ia;

    // Use this for initialization
    void Awake () {

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        ia = enemy.GetComponent<IAMov>();
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("entra en el trigger");
            other.GetComponent<IAMov>().wayPointIndex++;
            
        }
    }
}
