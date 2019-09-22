using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFps : MonoBehaviour
{
    float mouseX; // variable para guardar valores del movimiento del mouse en X 
    float mouseY; // variable para guardar valores del movimiento del mouse en Y

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X"); // captura el movimiento del mouse y se lo suma a mouseX para el movimiento de la camara en x
        mouseY -= Input.GetAxis("Mouse Y"); // captura el movimiento del mouse y se lo resta a mouseY para el movimiento de la camara en y

        if (mouseY > 40) // condicion para que no mire mas de 40 hacia arriba y menos de 40 hacia abajo (para que no gire la camara en y)
        {
            mouseY = 40;
        }
        else if (mouseY < -40)
        {
            mouseY = -40;
        }
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0); //mueve la camara segun el vaor que alla en
    }
}
