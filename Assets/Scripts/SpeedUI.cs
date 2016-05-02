using UnityEngine;
using UnityEngine.UI;


public class SpeedUI : MonoBehaviour {

    public Slider Slider;                           // The slider to represent how fast is the player going.
    public Image FillImage;                         // The image component of the slider.
    public Color FullSpeed = Color.blue;            // The color the speedbar when it's full speed
    public Color ZeroSpeed = Color.red;             // The color the speedbar when it's stoped

    private float MaxSpeed = 8;
    public float CurrentSpeed;
    private GameObject Player;
    private NauMov PlayerMov;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMov = Player.GetComponent<NauMov>();
    }


    void FixedUpdate() {
        CurrentSpeed = Mathf.Abs(PlayerMov.forwardForce.z);
        SetSpeedUI();
    }

    private void SetSpeedUI()
    {
        // Set the slider's value appropriately.
        Slider.value = CurrentSpeed;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        FillImage.color = Color.Lerp(ZeroSpeed, FullSpeed, CurrentSpeed / MaxSpeed);
    }

}
