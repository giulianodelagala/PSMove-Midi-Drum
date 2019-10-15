using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StickCalibrate : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion rotacion;
    Vector3 posicion;
    Quaternion last_rotacion;
    //bool hacia_abajo;

    public static float cmtometer = 1.0f / 80.0f;
    public int calibrate_x;
    public float calibrate_y;
    public int calibrate_z;

    public bool isCollision = false;
    public float cuenta_regresiva = 0.05f;

    public GameObject arrow_y_up, arrow_y_down,
        arrow_z_left, arrow_z_right, arrow_x_near, arrow_x_far, texto_cal;

    void Start()
    {
        texto_cal.SetActive(false);
    }



    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        if (this.CompareTag("left_mando"))
        {
            rotacion.Set(PSMoveService.mando_left.orie_z,
            -PSMoveService.mando_left.orie_y,
            PSMoveService.mando_left.orie_x,
            PSMoveService.mando_left.orie_w);

            posicion.Set( -(PSMoveService.mando_left.posi_z) * cmtometer + calibrate_x,
            (PSMoveService.mando_left.posi_y)*cmtometer + calibrate_y , (-PSMoveService.mando_left.posi_x + calibrate_z) * cmtometer);
        }
        else
        {
            rotacion.Set(PSMoveService.mando_right.orie_z,
            -PSMoveService.mando_right.orie_y,
            PSMoveService.mando_right.orie_x,
            PSMoveService.mando_right.orie_w);

            posicion.Set( -(PSMoveService.mando_right.posi_z * cmtometer) + calibrate_x,
            (PSMoveService.mando_right.posi_y) * cmtometer + calibrate_y, -PSMoveService.mando_right.posi_x * cmtometer);
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

        if (isCollision)
        {
            cuenta_regresiva -= Time.deltaTime;
            if (cuenta_regresiva < 0)
                cuenta_regresiva = 0;
        }
        else
            cuenta_regresiva = 0.05f;
       

        // Calibracion de Snare
        // Calibracion en eje Z
        Calibracion(transform.position.z, -0.07f, 0.07f, arrow_z_right, arrow_z_left);
        //Calibracion en eje y
        Calibracion(transform.position.y, 1.05f, 1.20f, arrow_y_up, arrow_y_down);
        //Calibracion en eje x
        Calibracion(transform.position.x, -0.14f, 0.02f, arrow_x_far, arrow_x_near);

    }

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
            case "esferasnare":
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

    private IEnumerator OnTriggerStay(Collider other)
    {
        Debug.Log(cuenta_regresiva);
        isCollision = true;
        if (cuenta_regresiva == 0)
        {
            if (other.CompareTag("esferasnare"))
            {
                other.GetComponent<Renderer>().material.color = Color.red;
                texto_cal.SetActive(true);
                yield return new WaitForSeconds(2f);
                PSMoveService.SetRumble(PSMoveService.Cliente, 0, 0.0f);
                SceneManager.LoadScene("Drum3D");
            }
            
        }
             
    }

    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
    }


    private void Calibracion(float posi, float min, float max, GameObject obmin, GameObject obmax)
    {
        if (posi < min)
        {
            obmin.SetActive(true);
            obmax.SetActive(false);
        }
        else if (posi > max)
        {
            obmin.SetActive(false);
            obmax.SetActive(true);
        }
        else
        {
            obmin.SetActive(false);
            obmax.SetActive(false);
        }
    }

}
