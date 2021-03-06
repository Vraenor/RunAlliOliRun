﻿using UnityEngine;
using UnityEngine.UI;


public class SpeedUI : MonoBehaviour
{

    public Slider Slider;                           // The slider to represent how fast is the player going.
    public Image FillImage;                         // The image component of the slider.
    public Color FullSpeed = Color.blue;            // The color the speedbar when it's full speed
    public Color ZeroSpeed = Color.red;             // The color the speedbar when it's stoped

    private float MaxSpeed = 8;                     //Velocidad maxima
    public float CurrentSpeed;                      //Velocidad actual
    private GameObject Player;                      //Referencia al jugador
    private NauMov PlayerMov;                       //referencia al script de movimiento


    private void Awake()
    {

    }


    void FixedUpdate()
    {
        if (Player != null)//control si hay o no player
        {
            PlayerMov = Player.GetComponent<NauMov>();
            CurrentSpeed = Mathf.Abs(PlayerMov.forwardForce.z);
            SetSpeedUI();
        }
        else {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void SetSpeedUI()
    {
        // Set the slider's value appropriately.
        Slider.value = CurrentSpeed;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        FillImage.color = Color.Lerp(ZeroSpeed, FullSpeed, CurrentSpeed / MaxSpeed);
    }

}
