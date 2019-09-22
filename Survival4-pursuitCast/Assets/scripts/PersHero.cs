using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NamNPC;
using System;

public class PersHero : MonoBehaviour
{
    NamNPC.NamEnemy.DatosZom utilZomb;
    NamNPC.NamAlly.DatosCiud utilCiu;
    System.Random rnd = new System.Random();
    public readonly float velhero;
    GameObject buscaCanvas;

    void Awake()
    {
        buscaCanvas = GameObject.Find("GameObject");
    }
    public PersHero()
    {
        velhero = rnd.Next(1, 4);
    }

    void OnCollisionEnter(Collision colision)
    {
        if (colision.transform.name == "Zombi")
        {
            Generator.isPlaying = false;
            utilZomb = colision.gameObject.GetComponent<NamNPC.NamEnemy.Zombi>().utilZom;
            Debug.Log("waaarrrr quiero comer " + utilZomb.queComer);
        }
        else if (colision.transform.name == "Ciudadanito")
        {
            utilCiu = colision.gameObject.GetComponent<NamNPC.NamAlly.Ciudadano>().utilCiud;
            Debug.Log("hola soy " + utilCiu.varNombrs + " y tengo " + utilCiu.edadCiudd);
            buscaCanvas.GetComponent<Generator>().mensZombi.text = "hola soy " + utilCiu.varNombrs + " y tengo " + utilCiu.edadCiudd;
        }
    }
}
