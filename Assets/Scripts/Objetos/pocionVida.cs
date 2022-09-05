using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocionVida : MonoBehaviour
{
    GameObject _jugador;

    bool jugadorCerca;
    float rangoVision = 5f;
    LayerMask layerJugador;

    bool pocionAgarrada;

    void Start()
    {
        _jugador = GameObject.FindGameObjectWithTag("Player");
        layerJugador = LayerMask.GetMask("Jugador");
    }

    // Update is called once per frame
    void Update()
    {
        jugadorCerca = Physics.CheckSphere(gameObject.transform.position, rangoVision, layerJugador);
        
        if (jugadorCerca)
        {
            Vector3 colisionItemJugador = new Vector3(_jugador.transform.position.x, _jugador.transform.position.y +1, _jugador.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, colisionItemJugador, Time.deltaTime * 5);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pocionAgarrada)
        {
            Jugador.pocionesVida += 1;
            pocionAgarrada = true;
            Destroy(gameObject);
        }
    }
}
