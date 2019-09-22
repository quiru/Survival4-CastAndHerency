using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NamNPC;
using NamNPC.NamEnemy;
using System;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public Text contZombi;
    public Text contCiud;
    public Text mensFin;
    public Text mensZombi;
    int numZomb;
    int numCiuda;
    public static bool isPlaying = true;

    System.Random rnd = new System.Random();
    GameObject heroe;
    GameObject zombi;
    GameObject ciudadano;
    NamNPC.NamEnemy.DatosZom utilZombi;
    public readonly int minimo;
    const int maximo = 25;

    public Generator()
    {
        minimo = rnd.Next(5, 16);
    }

    void Start()
    {
        heroe = GameObject.CreatePrimitive(PrimitiveType.Cube);
        heroe.AddComponent<PersHero>();
        heroe.AddComponent<MoviFps>();
        heroe.AddComponent<Rigidbody>();
        GameObject movCam = new GameObject();
        movCam.AddComponent<Camera>();
        movCam.AddComponent<CamFps>();
        movCam.transform.SetParent(heroe.transform);
        heroe.transform.position = new Vector3(rnd.Next(5, 24), 0.5f, rnd.Next(5, 24));

        int cantidad = rnd.Next(minimo, maximo);
        for (int i = 0; i < cantidad; i++)
        {
            int escojer = rnd.Next(0, 2);
            if (escojer == 0)
            {
                zombi = GameObject.CreatePrimitive(PrimitiveType.Cube);
                zombi.AddComponent<NamNPC.NamEnemy.Zombi>();
                zombi.transform.position = zombi.GetComponent<NamNPC.NamEnemy.Zombi>().mov;
                utilZombi = zombi.GetComponent<NamNPC.NamEnemy.Zombi>().utilZom;
                zombi.GetComponent<Renderer>().material.color = utilZombi.colorZombi;
                zombi.AddComponent<Rigidbody>();
                zombi.name = "Zombi";
            }
            else
            {
                ciudadano = GameObject.CreatePrimitive(PrimitiveType.Cube);
                ciudadano.AddComponent<NamNPC.NamAlly.Ciudadano>();
                ciudadano.transform.position = ciudadano.GetComponent<NamNPC.NamAlly.Ciudadano>().ubic;
                ciudadano.AddComponent<Rigidbody>();
                ciudadano.name = "Ciudadanito";
            }
        }

        numCiuda = 0;
        foreach (NamNPC.NamAlly.Ciudadano i in Transform.FindObjectsOfType<NamNPC.NamAlly.Ciudadano>())
        {
            numCiuda += 1;
        }

        numZomb = 0;
        foreach (Zombi i in Transform.FindObjectsOfType<Zombi>())
        {
            numZomb += 1;
        }
        contZombi.text = "zombi: " + numZomb;
        contCiud.text = "ciudadano: " + numCiuda;

        
    }

    void Update()
    {
        if (Generator.isPlaying == false)
        {
            mensFin.text = "GAME OVER \n PERDISTE";
        }
    }


}
