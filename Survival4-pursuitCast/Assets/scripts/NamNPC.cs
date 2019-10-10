    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using UnityEngine.UI;


namespace NamNPC //namespace que contiene los namespace NamAlly y NamEnemy
{
    
   namespace NamAlly //namespace que contiene la clase ciudadano
   {
        using NamEnemy; //llamada de la libreria que contiene la clase zombi

        public class Ciudadano : CompHerencia //clase ciudadano que hereda de CompHerencia el comportamiento
        {
            public DatosCiud utilCiud; //var tipo estructura de ciudadno para manejar los datos
            void Awake()
            {
                utilCiud.edadCiudd = Random.Range(15, 101); //le da edad al ciudadano
                int darNomb = Random.Range(0, 21); //random para dar nombre al ciudadano
                utilCiud.varNombrs = (DatosCiud.nombreCiudd)darNomb; //da edad al ciud segun darNombres
            }

            void Start()
            {
                StartCoroutine("CambioEstado"); //llama la corrutina del padre para cambiar de estado
            }

            Vector3 direction; //var vector3 para guardar la direccion hacia donde debe moverse el ciud
            void Update()
            {
                if (Generator.isPlaying == true) //if para mantener el juego en marcha o pararlo segun isPlaying
                {
                    float minDistance = 5; //para determinar la distancia a la cual debe comenzar a huir
                    GameObject zombieMasCecano = null; //para guardar el zombi mas cercano despues de evaluar la posicion mas cercana

                    foreach (var zombie in FindObjectsOfType<Zombi>()) //busca todos los objetos de tipo zombi para determinar si hay alguno cerca del ciudadano y cual es el mas cercano
                    {
                        Vector3 direccion = zombie.transform.position - transform.position;

                        if (direccion.magnitude <= minDistance) //si la distancia es menor a la asignada entonces guarda ese zombi y esa distancia
                        {
                            minDistance = direccion.magnitude;
                            zombieMasCecano = zombie.gameObject;
                        }
                    }

                    if (zombieMasCecano != null) //si hay algun zombi cercano entonces calcula la direccion y se mueve en direccion opuesta a este
                    {
                        direction = Vector3.Normalize(zombieMasCecano.transform.position - transform.position);
                        transform.position -= direction * (2f / utilCiud.edadCiudd);
                    }
                    else //si no hay ningun zombi cercano entonces llama la funcion del padre para generar comporamiento
                    {
                        movimiento();
                    } 
                }
                
            }

            void OnCollisionEnter(Collision collision) //si colisiona con el zombi se convierte en zombi 
            {
                if (collision.transform.name == "Zombi")
                {
                    Zombi datZom = this.gameObject.AddComponent<Zombi>(); //le adiere el componente zombi
                    datZom.utilZom = (DatosZom)this.gameObject.GetComponent<Ciudadano>().utilCiud; //hace un cast para conservar la edad del ciudadano en la del zombi
                    gameObject.GetComponent<Renderer>().material.color = collision.transform.GetComponent<Renderer>().material.color; //le da color del zombi que lo toco
                    transform.name = "Zombi"; //le da nombre para que el ciudadano pueda identificarlo y activar su comportamiento
                    Destroy(this.gameObject.GetComponent<Ciudadano>()); //destruye el componente de tipo ciudadano para dejarlo solo con el de zombi
                }
            }

        }
        public struct DatosCiud //estructura tipo ciudadano para guardar los datos
        {
            public enum nombreCiudd
            {
                rolando, josue, jaimito, romualdo, dioselina, maripan, consepcion, pancracia, leocadio, anzisar, juvenal, arturito, casilda, zacarin, antanas, gargamel, marucha, enriqueta, sinthia, anastasia
            }
            public nombreCiudd varNombrs;

            public enum Estad
            {
                running
            }
            public Estad varEstad;

            public int edadCiudd;

            public static explicit operator DatosZom(DatosCiud datCiu) //funcion para especificar un cast y pedirle que conserve algunos datos
            {
                DatosZom datZom = new DatosZom();
                datZom.edadZombi = datCiu.edadCiudd;

                return datZom;
            }
        }
   }

    namespace NamEnemy //namespace que guarda la clase zombi
    {
        public class Zombi : CompHerencia //clase zombi que hereda de compHerencia
        {
            public DatosZom utilZom; //variable tipo estructura de zombi para manejar los datos
            public NamAlly.DatosCiud utilCiud; //variable tipo estructura de ciud para manejar los datos
            public Vector3 mov;
            float velRand;
            GameObject buscCanvas; //variable gameObject para buscar el objeto que tiene los text de canvas, y mostrar los mensajes

            void Awake()
            {
                buscCanvas = GameObject.Find("GameObject"); //para buscar el objeto que tiene el script que maneja el canvas en la escena 
                int numColor = Random.Range(1, 4);
                switch (numColor) //switc para dar el color al zombi
                {
                    case 1:
                        utilZom.colorZombi = Color.cyan;
                        break;
                    case 2:
                        utilZom.colorZombi = Color.magenta;
                        break;
                    case 3:
                        utilZom.colorZombi = Color.green;
                        break;
                }

                int darGusto = Random.Range(0, 5); //random para dar gusto al zombi
                utilZom.queComer = (DatosZom.Gusto)darGusto; //da gusto al zombi
                utilZom.edadZombi = Random.Range(15, 100); //da edad al zombi
                mov = new Vector3(Random.Range(1, 20), 0.5f, Random.Range(1, 20));
                velRand = Random.Range(1f, 2f);
            }

            void Start()
            {
                StartCoroutine("ResetMens"); //llama corrutina para reiniciar el mensaje que muestra los zombis en pantalla
                StartCoroutine("CambioEstado"); //llama corrutina del padre para cambiar de estado
            }

            Vector3 direction; //var vector3 para guardar la direccion hacia donde debe moverse el zombi
            void Update()
            {
                if (Generator.isPlaying == true) //if para mantener el juego en marcha o pararlo segun isPlaying
                {
                    float minDistance = 5; //var para verificar la dist mas cercana
                    GameObject ciudMasCecano = null; //var para guardar el ciud mas cercano
                    GameObject objHero = null; //var para guardar el heroe

                    foreach (var ciud in FindObjectsOfType<NamAlly.Ciudadano>()) //busca todos los objetos de tipo ciudadano para determinar si hay alguno cerca del zombi y cual es el mas cercano
                    {
                        Vector3 direccion = ciud.transform.position - transform.position;

                        if (direccion.magnitude <= minDistance) //si la distancia es menor a la asignada entonces guarda ese ciudadano y esa distancia
                        {
                            minDistance = direccion.magnitude;
                            ciudMasCecano = ciud.gameObject;
                        }
                    }

                    objHero = GameObject.Find("heroe"); //encuentra al objeto del heroe

                    if (ciudMasCecano != null) //verifica si tiene algun ciudadano cerca para perseguirlo
                    {
                        direction = Vector3.Normalize(ciudMasCecano.transform.position - transform.position);
                        transform.position += direction * (2f / utilZom.edadZombi);
                    }
                    else if ((objHero.transform.position - transform.position).magnitude <= 5) //si no verifica si el heroe esta cerca para perseguirlo
                    {
                        direction = Vector3.Normalize(objHero.transform.position - transform.position);
                        transform.position += direction * (2f / utilZom.edadZombi);
                        utilZom = gameObject.GetComponent<Zombi>().utilZom;
                        buscCanvas.GetComponent<Generator>().mensZombi.text= "waarr quiero comer " + utilZom.queComer;
                    }
                    else //si no tiene ningun personaje cerca continua con su comportamiento
                    {
                        movimiento();
                    }
                }
            }

            IEnumerator ResetMens() //corrutina para reiniciar los mensajes que envian los zombis a la pantalla
            {
                while (true)
                {
                    buscCanvas.GetComponent<Generator>().mensZombi.text = "";
                    yield return new WaitForSeconds(2);
                }
            }
        }
        public struct DatosZom //estructura tipo zombi para guardar datos
        {
            public Color colorZombi;

            public enum Gusto
            {
                culito, deditos, uñas, teticas, homoplato
            }
            public Gusto queComer;

            public int edadZombi;
        }
    }
}
