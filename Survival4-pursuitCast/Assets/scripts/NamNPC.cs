    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using UnityEngine.UI;


namespace NamNPC
{
    
   namespace NamAlly
   {
        using NamEnemy;

        public class Ciudadano : CompHerencia
        {
            public DatosCiud utilCiud;
            public Vector3 ubic;
            GameObject zombiObject;
            //List<GameObject> numZombis;
            void Awake()
            {
                utilCiud.edadCiudd = Random.Range(15, 101);
                int darNomb = Random.Range(0, 21);
                utilCiud.varNombrs = (DatosCiud.nombreCiudd)darNomb;
                ubic = new Vector3(Random.Range(1, 20), 0.5f, Random.Range(1, 20));
            }

            void Start()
            {
                //GameObject[] AllGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
                //numZombis = new List<GameObject>();
                //foreach (GameObject aGameObject in AllGameObjects)
                //{
                //    Component aComponent = aGameObject.GetComponent (typeof(NamEnemy.Zombi));
                //    if (aComponent != null)
                //    zombiObject = aGameObject;
                //    numZombis.Add(aGameObject);
                //}
                StartCoroutine("CambioEstado");
            }

            Vector3 direction;
            void Update()
            {
                if (Generator.isPlaying == true)
                {
                    float minDistance = 5;
                    GameObject zombieMasCecano = null;

                    foreach (var zombie in FindObjectsOfType<Zombi>())
                    {
                        Vector3 direccion = zombie.transform.position - transform.position;

                        if (direccion.magnitude <= minDistance)
                        {
                            minDistance = direccion.magnitude;
                            zombieMasCecano = zombie.gameObject;
                        }
                    }

                    if (zombieMasCecano != null)
                    {
                        direction = Vector3.Normalize(zombieMasCecano.transform.position - transform.position);
                        transform.position -= direction * (2f / utilCiud.edadCiudd);
                    }
                    else
                    {
                        movimiento();
                    } 
                }

                //for (int i = 0; i < numZombis.Count; i++)
                //{
                //    Vector3 miVector = numZombis[i].transform.position - transform.position;
                //    if (miVector.magnitude <= 5f)
                //    {
                //        utilCiud.varEstad = (DatosCiud.Estad)0;
                //        direction = Vector3.Normalize(numZombis[i].transform.position - transform.position);
                //        transform.position -= direction * 0.02f;
                //    }

                //}
                //movimiento();

                /*Vector3 myVector = zombiObject.transform.position - transform.position;
                float distanceToPlayer = myVector.magnitude;
                if (distanceToPlayer <= 5.0f)
                {
                    direction = Vector3.Normalize(zombiObject.transform.position -transform.position);
                    transform.position -= direction*0.05f;
                }*/
            }

            void OnCollisionEnter(Collision collision)
            {
                if (collision.transform.name == "Zombi")
                {
                    Zombi datZom = this.gameObject.AddComponent<Zombi>();
                    datZom.utilZom = (DatosZom)this.gameObject.GetComponent<Ciudadano>().utilCiud;
                    gameObject.GetComponent<Renderer>().material.color = collision.transform.GetComponent<Renderer>().material.color;
                    transform.name = "Zombi";
                    Destroy(this.gameObject.GetComponent<Ciudadano>());
                }
            }

        }
        public struct DatosCiud
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

            public static explicit operator DatosZom(DatosCiud datCiu)
            {
                DatosZom datZom = new DatosZom();
                datZom.edadZombi = datCiu.edadCiudd;

                return datZom;
            }
        }
   }

    namespace NamEnemy
    {
        public class Zombi : CompHerencia
        {
            public DatosZom utilZom;
            public NamAlly.DatosCiud utilCiud;
            public Vector3 mov;
            float velRand;
            GameObject playerObject;
            public static Text mensaZombi;
            Generator mens;
            GameObject buscCanvas;

            void Awake()
            {
                buscCanvas = GameObject.Find("GameObject");
                int numColor = Random.Range(1, 4);
                switch (numColor)
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

                int darGusto = Random.Range(0, 5);
                utilZom.queComer = (DatosZom.Gusto)darGusto;
                utilZom.edadZombi = Random.Range(15, 100);
                mov = new Vector3(Random.Range(1, 20), 0.5f, Random.Range(1, 20));
                //cambiaMov = 0;
                velRand = Random.Range(1f, 2f);
                //cambiaRot = 0;
            }

            void Start()
            {
                //int daEstado = Random.Range(1, 4);
                //utilZom.estado = (DatosZom.Estados)daEstado;

                //GameObject[] AllGameObjects = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];
                

                StartCoroutine("ResetMens");
                StartCoroutine("CambioEstado");
            }

            Vector3 direction;
            Vector3 directionHero;
            void Update()
            {
                if (Generator.isPlaying == true)
                {
                    float minDistance = 5;
                    GameObject ciudMasCecano = null;
                    GameObject objHero = null;

                    foreach (var ciud in FindObjectsOfType<NamAlly.Ciudadano>())
                    {
                        Vector3 direccion = ciud.transform.position - transform.position;

                        if (direccion.magnitude <= minDistance)
                        {
                            minDistance = direccion.magnitude;
                            ciudMasCecano = ciud.gameObject;
                        }
                    }

                    foreach (var hero in FindObjectsOfType<PersHero>())
                    {
                        objHero = hero.gameObject;
                    }

                    if (ciudMasCecano != null)
                    {
                        direction = Vector3.Normalize(ciudMasCecano.transform.position - transform.position);
                        transform.position += direction * (2f / utilZom.edadZombi);
                    }
                    else if ((objHero.transform.position - transform.position).magnitude <= 5)
                    {
                        direction = Vector3.Normalize(objHero.transform.position - transform.position);
                        transform.position += direction * (2f / utilZom.edadZombi);
                        utilZom = gameObject.GetComponent<Zombi>().utilZom;
                        Debug.Log("waaarrrr quiero comer " + utilZom.queComer);
                        //mens.mensZombi.text = "waarr quiero comer " + utilZom.queComer;
                        buscCanvas.GetComponent<Generator>().mensZombi.text= "waarr quiero comer " + utilZom.queComer;
                    }
                    else
                    {
                        movimiento();
                        //buscCanvas.GetComponent<Generator>().mensZombi.text = "";
                    }
                }

                //Vector3 myVector = playerObject.transform.position - transform.position;
                //float distanceToPlayer = myVector.magnitude;
                //if (distanceToPlayer <= 5.0f)
                //{
                //    direction = Vector3.Normalize(playerObject.transform.position -transform.position);
                //    transform.position += direction*0.05f;
                //}

                //switch (utilZom.estado)
                //{
                //    case DatosZom.Estados.rotating:
                //        if (cambiaRot == 0)
                //        {
                //            transform.eulerAngles += new Vector3(0, 0.5f, 0);
                //        }
                //        else
                //        {
                //            transform.eulerAngles -= new Vector3(0, 0.5f, 0);
                //        }
                //        break;
                //    case DatosZom.Estados.moving:
                //        if (cambiaMov == 0)
                //        {
                //            transform.position += new Vector3(0, 0, velRand * Time.deltaTime);
                //        }
                //        else if (cambiaMov == 1)
                //        {
                //            transform.position -= new Vector3(0, 0, velRand * Time.deltaTime);
                //        }
                //        else if (cambiaMov == 2)
                //        {
                //            transform.position -= new Vector3(velRand * Time.deltaTime, 0, 0);
                //        }
                //        else if (cambiaMov == 3)
                //        {
                //            transform.position += new Vector3(velRand * Time.deltaTime, 0, 0);
                //        }
                //        break;
                //    case DatosZom.Estados.idle:
                //        transform.position += new Vector3(0, 0, 0);
                //        break;
                //}
            }

            void OnDrawGizmos()
            {
                Gizmos.DrawLine(transform.position, transform.position + direction);
            }

            IEnumerator ResetMens()
            {
                while (true)
                {
                    buscCanvas.GetComponent<Generator>().mensZombi.text = "";
                    yield return new WaitForSeconds(2);
                }
            }
        }
        public struct DatosZom
        {
            public Color colorZombi;

            public enum Gusto
            {
                culito, deditos, uñas, teticas, homoplato
            }
            public Gusto queComer;

            //public enum Estados
            //{
            //    rotating, moving, idle 
            //}
            //public Estados estado;

            public int edadZombi;
        }
    }
}
