using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public List<GameObject> listaEnemigos;
    public GameObject golem;


    void Start()
    {
        foreach (GameObject enemigo in listaEnemigos)
        {
            Instantiate(golem, enemigo.transform.position, enemigo.transform.rotation);
        }
    }
}
