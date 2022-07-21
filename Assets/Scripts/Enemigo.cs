using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float rangoAtaqueCerca;
    public bool estaRangoCerca;
    public float rangoAtaqueADistancia;
    public GameObject prefabRoca;
    Animator anim;
    public Transform spawnRoca;
    public Transform puntoMiraRay;
    GameObject Roca;


    public float cooldawnLanzaRoca;
    float tiempoParaLanzarRoca;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        TemporizadorLanzarRoca();
        spawnRoca = GetComponent<Transform>().Find("spawnRoca");
        puntoMiraRay = GetComponent<Transform>().Find("puntoMiraRay");
    }

    // Update is called once per frame
    void Update()
    {   
        AtaqueCerca();
        AtaqueADistancia();
        TemporizadorLanzarRoca();
    }

    private void AtaqueADistancia()
    {
        RaycastHit ray;

        if (Physics.Raycast(puntoMiraRay.transform.position, Vector3.forward, out ray, rangoAtaqueADistancia))
        {
            if (ray.transform.tag == "Player" && tiempoParaLanzarRoca <= 0 && !estaRangoCerca)
            {
                anim.SetTrigger("Attack01");

                tiempoParaLanzarRoca = cooldawnLanzaRoca;

                StartCoroutine(tiempoAnimacionLanzarRoca());

                //Roca = Instantiate(prefabRoca,spawnRoca.transform.position,spawnRoca.transform.rotation);
                //Roca.GetComponent<Rigidbody>().AddForce(spawnRoca.forward * 40, ForceMode.Impulse);

            }
        }
    }

    void AtaqueCerca()
    {
        RaycastHit ray;

        if (Physics.Raycast(puntoMiraRay.transform.position, Vector3.forward, out ray, rangoAtaqueCerca))
        {
            estaRangoCerca = true;
            if (ray.transform.tag == "Player")
            {
                anim.SetTrigger("Attack02");
                
            }
        }
        else
        {
            estaRangoCerca = false;
        }
    }

    void TemporizadorLanzarRoca()
    {
        tiempoParaLanzarRoca -= Time.deltaTime;
    }

    IEnumerator tiempoAnimacionLanzarRoca()
    {
        yield return new WaitForSeconds(1.6f);

        Roca = Instantiate(prefabRoca, spawnRoca.transform.position, spawnRoca.transform.rotation);
        Roca.GetComponent<Rigidbody>().AddForce(spawnRoca.forward * 40, ForceMode.Impulse);
    }
}
