using UnityEngine;
using UnityEngine.UI;


public class LapCounter : MonoBehaviour
{


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
        lap = 1;
        maxLaps = 3;
    }
    void FixedUpdate()
    {
        if (player == null)//control si existe jugador
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<NauMov>();
        }
        else
        {

            lap = playerMovement.currentLap;
            switch (lap)//control de la imagen de la vuelta en la que estas
            {

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
            switch (maxLaps)//control de la imagen de maximo numero de vueltas
            {

                case 2:
                    maxLap.sprite = numbers[1];
                    break;
                case 3:
                    maxLap.sprite = numbers[2];
                    break;

                default:
                    maxLap.sprite = numbers[0];
                    break;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))//control de vuelta
        {
            if (lap == 1 && playerMovement.lastWP == 134) { playerMovement.currentLap = 2; }
            if (lap == 2 && playerMovement.lastWP == 134) { playerMovement.currentLap = 3; }
        }
    }
}
