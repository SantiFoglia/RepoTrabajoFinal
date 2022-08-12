using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public int vida;
    public string nombre;
    public float velocidad;
    public float rangoVision;
    static public int dañoContacto;

    public float rangoAtaqueBasico;
    public bool estaRangoCerca;
    public float rangoAtaqueEspecial;
    public bool estaAtacando;

    public Animator anim;
    public LayerMask layerJugador;
    public GameObject _jugador;

    public BoxCollider campoVision;
    public bool jugadorEnCampoVision;

    public bool enemigoInvulnerable;

    public bool jugadorCerca;

    static public bool enemigoMuriendo;

    virtual public void AtaqueBasico()
    {

    }
    virtual public void AtaqueEspecial()
    {

    }
    virtual public void detectarJugador()
    {
        jugadorCerca = Physics.CheckSphere(gameObject.transform.position, rangoVision, layerJugador);
    }
    virtual public void mirarJugador()
    {
        Quaternion newRotation = Quaternion.LookRotation(_jugador.transform.position - transform.position);
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = newRotation;
    }
    virtual public void seguirJugador()
    {
        transform.Translate(Vector3.forward *Time.deltaTime * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flecha") && !enemigoInvulnerable)
        {
            vida -= Flecha.daño;
            enemigoInvulnerable = true;
            StartCoroutine(tiempoInvulnerable());
        }

    }

    IEnumerator tiempoInvulnerable()
    {
        yield return new WaitForSeconds(0.4f);
        enemigoInvulnerable = false;
    }

}
