using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NamNPC;
using System;

public class PersHero : MonoBehaviour
{
    NamNPC.NamEnemy.DatosZom utilZomb; //variable tipo estructura de zombi para utilizar los datos
    NamNPC.NamAlly.DatosCiud utilCiu;
    System.Random rnd = new System.Random(); //variable random para dearle velocidad al heroe
    public readonly float velhero; //variable readonly para asignar la vel del heroe
    GameObject buscaCanvas; //variable gameObject para buscar el objeto que tiene los text de canvas, y mostrar los mensajes

    void Awake()
    {
        buscaCanvas = GameObject.Find("GameObject"); //encuentra el gameobject con el canvas
    }
    public PersHero()
    {
        velhero = rnd.Next(1, 4); //asigna vel a heroe
    }

    void OnCollisionEnter(Collision colision) //si colisiona con un zombi o ciudadano
    {
        if (colision.transform.name == "Zombi")
        {
            Generator.isPlaying = false; //vuelve la variable statica que para el juego
            utilZomb = colision.gameObject.GetComponent<NamNPC.NamEnemy.Zombi>().utilZom;
            Debug.Log("waaarrrr quiero comer " + utilZomb.queComer);
        }
        else if (colision.transform.name == "Ciudadanito")
        {
            utilCiu = colision.gameObject.GetComponent<NamNPC.NamAlly.Ciudadano>().utilCiud;
            Debug.Log("hola soy " + utilCiu.varNombrs + " y tengo " + utilCiu.edadCiudd);
            buscaCanvas.GetComponent<Generator>().mensZombi.text = "hola soy " + utilCiu.varNombrs + " y tengo " + utilCiu.edadCiudd; //muestra un mensaje con el nombre y edad del ciudadano
        }
    }
}
