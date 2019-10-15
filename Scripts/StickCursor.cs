using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StickCursor : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion rotacion;
    Vector3 posicion;
    Quaternion last_rotacion;
    //bool hacia_abajo;

    public static float cmtometer = 10.0f;
    public int calibrate_x;
    public float calibrate_y;
    public int calibrate_z;

    public bool isCollision = false;
    //public float cuenta_regresiva = 0.05f;

    public Button boton_play;
    public Button boton_calibrate;
    public Button boton_about;

    //public GameObject arrow_y_up;

    void Start()
    {
        
    }



    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        if (this.CompareTag("cursor"))
        {
            rotacion.Set(0, 0, 0, 0);

            //posicion.Set( -(PSMoveService.mando_left.posi_z) * cmtometer + calibrate_x,
            //(PSMoveService.mando_left.posi_y)*cmtometer + calibrate_y , 0);

            posicion.Set(500 ,
           (PSMoveService.mando_left.posi_y) * cmtometer + calibrate_y, 0);
        }
        
   
        //transform.rotation = rotacion;
        /*
        if (rotacion != last_rotacion)
        {
            if (rotacion.z < last_rotacion.z)
                hacia_abajo = true;
            else
                hacia_abajo = false;

            last_rotacion = rotacion;
        }
        */
        transform.SetPositionAndRotation(posicion, rotacion);

        /*
        if (isCollision)
        {
            cuenta_regresiva -= Time.deltaTime;
            if (cuenta_regresiva < 0)
                cuenta_regresiva = 0;
        }
        else
            cuenta_regresiva = 0.05f;
            */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name); 
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        

        switch (collision.gameObject.tag)
        {
            case "play":
                boton_play.Select();
                break;
            case "calibrate":
                boton_calibrate.Select();
                break;
            case "about":
                boton_about.Select();
                break;
        }

        if (PSMoveService.ButtonState(0, 2))
        {
            //Debug.Log("Presionado");
            switch (collision.gameObject.tag)
            {
                case "play":
                    //yield return new WaitForSeconds(2f);
                    PSMoveService.SetRumble(PSMoveService.Cliente, 0, 0.0f);
                    SceneManager.LoadScene("Drum3D");
                    break;
                case "calibrate":
                    //yield return new WaitForSeconds(2f);
                    PSMoveService.SetRumble(PSMoveService.Cliente, 0, 0.0f);
                    SceneManager.LoadScene("Calibracion");
                    break;
                case "about":
                    //yield return new WaitForSeconds(2f);
                    PSMoveService.SetRumble(PSMoveService.Cliente, 0, 0.0f);
                    //SceneManager.LoadScene("Drum3D");
                    break;
            }
        }

        /*
        switch (collision.gameObject.tag)
        {
            case "play":
                MIDI.SendNota(26, 127);
                Debug.Log("Ingrese");
                Debug.Log(other.GetComponent<Renderer>().material.color);
                break;
            case "calibrate":
                MIDI.SendNota(71, 127);
                break;
            case "about":
                MIDI.SendNota(30, 127);
                break;
        }
        */

    }



    /*
    //private IEnumerator OnTriggerEnter(Collider other)
    private IEnumerator OnTriggerEnter(Collider other)
    {
        int id;
        int velocidad = 100;
        if (this.CompareTag("left_mando"))
            id = 0;
        else
            id = 1;
           
        //velocidad = Clamp(PSMoveService.mando_right.vel_j);
              
            
        switch (other.gameObject.tag)
        {
            case "calibrate":
                MIDI.SendNota(26, velocidad);
                Debug.Log("Ingrese");
                Debug.Log (other.GetComponent<Renderer>().material.color);
                break;
            case "ride":
                MIDI.SendNota(71, velocidad);
                break;
            case "hihat":
                MIDI.SendNota(30, velocidad);
                break;
            case "middletom":
                MIDI.SendNota(35, velocidad);
                break;
            case "hitom":
                MIDI.SendNota(38, velocidad);
                break;
            case "floortom":
                MIDI.SendNota(31, velocidad);
                break;
        }

        PSMoveService.SetRumble(PSMoveService.Cliente, id, 0.5f);
        yield return new WaitForSeconds(0.5f);
        PSMoveService.SetRumble(PSMoveService.Cliente, id, 0.0f);
   
    }
    */
    

}
