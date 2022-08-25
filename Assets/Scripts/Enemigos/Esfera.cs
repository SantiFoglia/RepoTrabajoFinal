using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Esfera : Enemigos
{
    public PostProcessVolume volumen;
    private ColorGrading _ColorGrading;


    // Start is called before the first frame update
    void Start()
    {
        //velocidad = 8f;
        //Destroy(gameObject, 10f);
        _jugador = GameObject.FindGameObjectWithTag("Player");
        //_ColorGrading.colorGradingLayer = null;
        volumen.profile.TryGetSettings(out _ColorGrading);

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_jugador.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ColorGrading.hueShift.value = 180f;
            Time.timeScale = 0.3f;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ColorGrading.hueShift.value = 0;
            Time.timeScale = 1f;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Jugador.vida -= 0.1f;
        }

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

}
