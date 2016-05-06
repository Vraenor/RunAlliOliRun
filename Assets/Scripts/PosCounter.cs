using UnityEngine;
using UnityEngine.UI;

public class PosCounter : MonoBehaviour {

    public GameObject player;
    public NauMov playerMovement;
    public Sprite[] numbers;
    public Image posCounter;                         // The image component of the lap.
    public int pos;


    // Use this for initialization
    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<NauMov>();
        pos = 1;
    }
    void FixedUpdate()
    {
        switch (pos)
        {
            case 2:
                posCounter.sprite = numbers[1];
                break;
            case 3:
                posCounter.sprite = numbers[2];
                break;

            default:
                posCounter.sprite = numbers[0];
                break;
        }

    }

}
