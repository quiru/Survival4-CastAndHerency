using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompHerencia : MonoBehaviour
{
    States estados; //var tipo enum para manejar los estados
    int cambiaRot; //var para cambiar la direccion de la rotacion
    int cambiaMov; //var para cambiar la direccion del movimiento
    float velRandom; //var para dar velocidad

    void Awake()
    {
        cambiaMov = Random.Range(0, 4); //inicia las variables con un valor random 
        cambiaRot = Random.Range(0, 2);
    }

    void Start()
    {
        int daEstado = Random.Range(1, 4); //random para dar un estado
        estados = (States)daEstado; //da estado a la variable
    }

    public void movimiento() //funcion para generar comportamiento
    {
        velRandom = Random.Range(1f, 2f);
        switch (estados) //switch para moverse segun el estado
        {
            case States.rotating: //si el estado es rotating
                if (cambiaRot == 0) //rota hacia una direccion 
                {
                    transform.eulerAngles += new Vector3(0, 0.5f, 0);
                }
                else //si no rota hacia la otra
                {
                    transform.eulerAngles -= new Vector3(0, 0.5f, 0);
                }
                break;
            case States.moving: //si el estado es moving
                if (cambiaMov == 0) //se mueve hacia alguna direccion  dependiendo de cambiaMov 
                {
                    transform.position += new Vector3(0, 0, velRandom * Time.deltaTime);
                }
                else if (cambiaMov == 1)
                {
                    transform.position -= new Vector3(0, 0, velRandom * Time.deltaTime);
                }
                else if (cambiaMov == 2)
                {
                    transform.position -= new Vector3(velRandom * Time.deltaTime, 0, 0);
                }
                else if (cambiaMov == 3)
                {
                    transform.position += new Vector3(velRandom * Time.deltaTime, 0, 0);
                }
                break;
            case States.idle: //si el estado es idle se queda quieto
                transform.position += new Vector3(0, 0, 0);
                break;
        }
    }

    IEnumerator CambioEstado() //corrutina para cambiar de estado
    {
        while (true)
        {
            if (estados == (States)0)
            {
                estados = (States)1;
                cambiaMov = Random.Range(0, 4); //cambia la variable para cambiar la direccion del movimiento
            }
            else if (estados == (States)1)
            {
                estados = (States)2;
            }
            else
            {
                estados = (States)0;
                cambiaRot = Random.Range(0, 2); //cambia la variable para cambiar la direccion del movimiento
            }
            yield return new WaitForSeconds(3);
        }
    }
}

public enum States //enum de estados
{
    rotating, moving, idle 
}
