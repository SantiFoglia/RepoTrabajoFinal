using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public int vida;
    public string nombre;
    public float velocidad;
    public float rangoVision;

    public float rangoAtaqueBasico;
    public bool estaRangoCerca;
    public float rangoAtaqueEspecial;
    public bool estaAtacando;

    public Animator anim;
    public LayerMask layerJugador;
    public GameObject _jugador;

    public BoxCollider campoVision;
    public bool jugadorEnCampoVision;


    public bool jugadorCerca;

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
    virtual public void Morir()
    {
        if (vida < 0)
        {
            GameObject.Destroy(this, 3f);
            //anim.SetBool(estaMuriendo,true);
        }
        
    }
}
