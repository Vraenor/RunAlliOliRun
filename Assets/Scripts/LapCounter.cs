using UnityEngine;
using UnityEngine.UI;


public class LapCounter : MonoBehaviour {


    public GameObject player;
    public NauMov playerMovement;
    public Sprite[] numbers;
    public Image lapCounter;                         // The image component of the lap.
    public Image maxLap;                            // The image component of the lap.
    public int lap;
    public int maxLaps;


    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<NauMov>();
        lap = 0;
    }
    void FixedUpdate()
    {
        lap = playerMovement.currentLap;
        switch (lap) {
           
            case 2:
                lapCounter.sprite = numbers[1];
                break;
            case 3:
                lapCounter.sprite = numbers[2];
                break;

            default:
                lapCounter.sprite = numbers[0];
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("ultimo waypoint  " + playerMovement.lastWP);
        if (other.gameObject.tag.Equals("Player"))
        {
            if (lap == 0 && playerMovement.lastWP == 120) { playerMovement.currentLap = 1; }
            if (lap == 1 && playerMovement.lastWP == 120) { playerMovement.currentLap = 2; }
            if (lap == 2 && playerMovement.lastWP == 120) { playerMovement.currentLap = 3; }
        }
    }
}
