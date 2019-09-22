using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompHerencia : MonoBehaviour
{
    States estados;
    int cambiaRot;
    int cambiaMov;
    float velRandom;

    void Awake()
    {
        cambiaMov = Random.Range(0, 4);
        
        cambiaRot = Random.Range(0, 3);
    }

    void Start()
    {
        int daEstado = Random.Range(1, 4);
        estados = (States)daEstado;

        //StartCoroutine("CambioEstado");
    }

    
    void Update()
    {
        
    }

    public void movimiento()
    {
        velRandom = Random.Range(1f, 2f);
        switch (estados)
        {
            case States.rotating:
                if (cambiaRot == 0)
                {
                    transform.eulerAngles += new Vector3(0, 0.5f, 0);
                }
                else
                {
                    transform.eulerAngles -= new Vector3(0, 0.5f, 0);
                }
                break;
            case States.moving:
                if (cambiaMov == 0)
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
            case States.idle:
                transform.position += new Vector3(0, 0, 0);
                break;
        }
    }

    IEnumerator CambioEstado()
    {
        while (true)
        {
            if (estados == (States)0)
            {
                estados = (States)1;
                cambiaMov = Random.Range(0, 4);
            }
            else if (estados == (States)1)
            {
                estados = (States)2;
            }
            else
            {
                estados = (States)0;
                cambiaRot = Random.Range(0, 2);
            }


            yield return new WaitForSeconds(3);
        }
    }
}

public enum States
{
    rotating, moving, idle 
}
