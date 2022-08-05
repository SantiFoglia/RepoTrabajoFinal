using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public List<GameObject> listaSpawnsEnemigos;
    public GameObject golem;
    public GameObject miniGolem;
    public Dictionary<int,string> nombres;


    void Start()
    {
        nombres = new Dictionary<int, string>();

        nombres.Add(1, "golem");
        nombres.Add(2, "miniGolem");

        foreach (GameObject spawn in listaSpawnsEnemigos)
        {
            if (spawn.name == nombres[1])
            {
                Instantiate(golem, spawn.transform.position, spawn.transform.rotation);
            }
            if (spawn.name == nombres[2])
            {
                Instantiate(miniGolem, spawn.transform.position, spawn.transform.rotation);
            }

        }
    }
}
