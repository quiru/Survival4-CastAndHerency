using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NamNPC;
using NamNPC.NamEnemy;
using System;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public Text contZombi; //variables para asignar los textos
    public Text contCiud;
    public Text mensFin;
    public Text mensZombi;
    int numZomb;
    int numCiuda;
    public static bool isPlaying = true; //variable para parar el juego

    System.Random rnd = new System.Random(); //var random para asignar la variable readonly de minimo
    GameObject heroe; //var para crear personajes
    GameObject zombi;
    GameObject ciudadano;
    NamNPC.NamEnemy.DatosZom utilZombi; //var tipo estructura de zombi para manejar datos
    public readonly int minimo; //var readonly para asignar minimo de pers
    const int maximo = 25; //var const para asignar maximo de pers

    public Generator()
    {
        minimo = rnd.Next(5, 16); //asigna minimo de pers
    }

    void Start()
    {
        heroe = GameObject.CreatePrimitive(PrimitiveType.Cube); //crea pers heroe
        heroe.AddComponent<PersHero>(); //adiere componentes
        heroe.AddComponent<MoviFps>();
        heroe.AddComponent<Rigidbody>();
        GameObject movCam = new GameObject(); //crea un objeto para añadirle la camara y el script de movimiento de camara y hacerlo hijo del heroe
        movCam.AddComponent<Camera>();
        movCam.AddComponent<CamFps>();
        movCam.transform.SetParent(heroe.transform);
        heroe.transform.position = new Vector3(rnd.Next(5, 24), 0.5f, rnd.Next(5, 24));
        heroe.name = "heroe";

        int cantidad = rnd.Next(minimo, maximo); //var para crear numero random de personajes
        for (int i = 0; i < cantidad; i++) //crea los personajes segun cantidad
        {
            int escojer = rnd.Next(0, 2);
            if (escojer == 0) //segun escojer crea un zombi o un ciudadano
            {
                zombi = GameObject.CreatePrimitive(PrimitiveType.Cube); //crea zombi y adiere componentes
                zombi.AddComponent<NamNPC.NamEnemy.Zombi>();
                zombi.transform.position = zombi.GetComponent<NamNPC.NamEnemy.Zombi>().mov;
                utilZombi = zombi.GetComponent<NamNPC.NamEnemy.Zombi>().utilZom;
                zombi.GetComponent<Renderer>().material.color = utilZombi.colorZombi;
                zombi.AddComponent<Rigidbody>();
                zombi.name = "Zombi";
            }
            else
            {
                ciudadano = GameObject.CreatePrimitive(PrimitiveType.Cube); //crea ciudadano y adiere componentes
                ciudadano.AddComponent<NamNPC.NamAlly.Ciudadano>();
                ciudadano.transform.position = new Vector3(rnd.Next(1, 20), 0.5f, rnd.Next(1, 20));
                ciudadano.AddComponent<Rigidbody>();
                ciudadano.name = "Ciudadanito";
            }
        }

        numCiuda = 0;
        foreach (NamNPC.NamAlly.Ciudadano i in Transform.FindObjectsOfType<NamNPC.NamAlly.Ciudadano>()) //busca todos los objetos con el componente ciudadanno para ir sumando numCiuda y mostrar en pantalla
        {
            numCiuda += 1;
        }

        numZomb = 0;
        foreach (Zombi i in Transform.FindObjectsOfType<Zombi>()) //busca todos los objetos con el componente zombi para ir sumando numZomb y mostrar en pantalla
        {
            numZomb += 1;
        }
        contZombi.text = "zombi: " + numZomb; //muestra numero de zombis
        contCiud.text = "ciudadano: " + numCiuda; //muestra numero de ciudadanos

        
    }

    void Update()
    {
        if (Generator.isPlaying == false) //verifica cuando la variable isPlaying es falsa para mostrar en pantalla el mensaje de gameover
        {
            mensFin.text = "GAME OVER \n PERDISTE";
        }
    }


}
