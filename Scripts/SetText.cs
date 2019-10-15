using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
       
        
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Gyros.  x  " + PSMoveService.mando_left.posi_x.ToString() +
            "  y  " + PSMoveService.mando_left.posi_y.ToString() +
            "  z  " + PSMoveService.mando_left.posi_z.ToString()
        +   "  y1  " + PSMoveService.mando_left.posi_y.ToString();
            
    }
}
