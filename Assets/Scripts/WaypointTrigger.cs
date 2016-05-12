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
            switch (aux) {
                case 5:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 13:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 16:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 19:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 23:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 31:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 33:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 36:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 39:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 42:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 49:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 51:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 60:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 62:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 64:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 70:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 71:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 78:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 80:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 83:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 85:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 86:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 87:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 91:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 93:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 95:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 97:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 99:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 102:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 104:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 105:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 106:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 107:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 109:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 110:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 113:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 114:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 117:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 119:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 121:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 122:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 124:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 126:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 127:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                case 133:
                    other.GetComponent<IAMov>().functionState = 1;
                    break;
                default:
                    other.GetComponent<IAMov>().functionState = 0;
                    break;
            }
        }

        if (other.gameObject.tag.Equals("Player"))
        {
            if ((aux == other.GetComponent<NauMov>().lastWP+1) || (aux ==1 && other.GetComponent<NauMov>().lastWP == 134))
            {
                other.GetComponent<NauMov>().wPos = GetComponentInParent<Transform>().position;
                other.GetComponent<NauMov>().wRot = GetComponentInParent<Transform>().rotation;
                other.GetComponent<NauMov>().wRot.y = GetComponentInParent<Transform>().rotation.y + 180f;
                other.GetComponent<NauMov>().lastWP = aux;
            }
            
        }
    }
}
