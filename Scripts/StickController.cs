using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickController : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion rotacion;
    Vector3 posicion;
    Quaternion last_rotacion;
    bool hacia_abajo;

    public static float cmtometer = 1.0f / 80.0f;
    public int calibrate_x;
    public float calibrate_y;
    public int calibrate_z;

    void Start()
    {
        
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

            posicion.Set(-(PSMoveService.mando_right.posi_z) * cmtometer + calibrate_x,
            (PSMoveService.mando_right.posi_y) * cmtometer + calibrate_y, (-PSMoveService.mando_right.posi_x) * cmtometer);
        }
   
        //transform.rotation = rotacion;

        if (rotacion != last_rotacion)
        {
            if (rotacion.z < last_rotacion.z)
                hacia_abajo = true;
            else
                hacia_abajo = false;

            last_rotacion = rotacion;
        }

        transform.SetPositionAndRotation(posicion, rotacion);

        
    }

    //private IEnumerator OnTriggerEnter(Collider other)
    private void OnTriggerEnter(Collider other)
    {
        if (hacia_abajo)
        {
            int id;
            int velocidad = 100;
            if (this.CompareTag("left_mando"))
            {
                id = 0;
                //velocidad = Clamp(PSMoveService.mando_left.vel_j);
            }
                
            else
            {
                id = 1;
                //velocidad = Clamp(PSMoveService.mando_right.vel_j);
            }
                

            

            switch (other.gameObject.tag)
            {
                case "snare":
                    MIDI.SendNota(26, velocidad);
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

            //PSMoveService.SetRumble(PSMoveService.Cliente, id, 0.5f);
            //yield return new WaitForSeconds(0.5f);
            //PSMoveService.SetRumble(PSMoveService.Cliente, id, 0.0f);

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        MIDI.OffNota();
    }

    private void OnTriggerStay(Collider other)
    {
        MIDI.OffNota();
    }



    private int Clamp(float value)
    {
        value = Math.Abs(value)*1.8f;
        return (int) (
            (value < 10) ? 20 :
            (value < 20) ? 50 :
            (value < 40) ? 80 :
            (value < 50) ? 95 :
            (value < 65) ? 105:
            (value < 90) ? 110 : 127); 
    }


}
