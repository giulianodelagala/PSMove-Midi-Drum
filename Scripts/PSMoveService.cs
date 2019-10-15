using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class PSMoveService : MonoBehaviour
{
    // PSMOVE CLIENTE //

    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr CreateCliente();

    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern bool GetStartup(IntPtr Cliente);

    //[DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    //static extern void GetGyroscope(IntPtr Cliente, out float x);

    //[DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    //static extern unsafe void GetGyrosX(IntPtr Cliente, out float x, out float y, out float z);

    //[DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    //static extern unsafe void GetOrientacion(IntPtr Cliente, out float w, out float x, out float y, out float z);
    
    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern unsafe void GetPose(IntPtr Cliente, int id, out float p_x, out float p_y, out float p_z, out float o_w, out float o_x, out float o_y, out float o_z);

    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern unsafe void GetVelocidad(IntPtr Cliente, int id, out float i, out float j, out float k);
    
    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static public extern void SetRumble(IntPtr Cliente, int x, float y);

    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static public extern bool GetButtonState(IntPtr Cliente, int id, int id_button);

    [DllImport("UnitySampleC.dll",  CallingConvention = CallingConvention.Cdecl)]
    static extern void GetUpdate(IntPtr Cliente);

    [DllImport("UnitySampleC.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern void DeleteCliente(IntPtr Cliente);

    public static IntPtr Cliente;

    public struct Mando
    {
        public int id;
        public float orie_w, orie_x, orie_y, orie_z;
        public float posi_x, posi_y, posi_z;
        public float vel_i, vel_j, vel_k;
        //public bool cross_button;

        public Mando(int id)
        {
            this.id = id;
            orie_w = orie_x = orie_y = orie_z = posi_x = posi_y = posi_z = 0;
            vel_i = vel_j = vel_k = 0;
            //cross_button = false;
        }
    }

    //public static float giro_x, giro_y, giro_z;
    

    public static Mando mando_left = new Mando(0);
    public static Mando mando_right = new Mando(1);

    void Start()
    {
        Cliente = CreateCliente();
        if (GetStartup(Cliente))
            Debug.Log("Conectado");
        else
        {
            Debug.Log("Error en Conexion");
            Application.Quit();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetUpdate(Cliente);

        GetPose(Cliente, 0, out mando_left.posi_x, out mando_left.posi_y, out mando_left.posi_z,
            out mando_left.orie_w, out mando_left.orie_x, out mando_left.orie_y, out mando_left.orie_z);

        GetPose(Cliente, 1, out mando_right.posi_x, out mando_right.posi_y, out mando_right.posi_z,
            out mando_right.orie_w, out mando_right.orie_x, out mando_right.orie_y, out mando_right.orie_z);

        GetVelocidad(Cliente, 0, out mando_left.vel_i, out mando_left.vel_j, out mando_left.vel_k);
        GetVelocidad(Cliente, 1, out mando_right.vel_i, out mando_right.vel_j, out mando_right.vel_k);

        //mando_left.cross_button = GetButtonState(Cliente, 0, 2);
    }

    private void Update()
    {
        //GetGyrosX(Cliente, out giro_x, out giro_y, out giro_z);
        
    }

    private void OnDestroy()
    {
        DeleteCliente(Cliente);
    }

    static public bool ButtonState(int id, int id_button)
    {
        return GetButtonState(Cliente, id, id_button);
    }
}
