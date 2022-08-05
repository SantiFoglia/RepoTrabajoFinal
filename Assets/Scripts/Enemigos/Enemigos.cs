using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public int vida;
    public string nombre;
    public float velocidad;
    public float velocidadDeRotacion;

    public float rangoAtaqueBasico;
    public bool estaRangoCerca;
    public float rangoAtaqueEspecial;
    public float rangoVision;
    public Animator anim;
    public LayerMask layerJugador;
    public GameObject _jugador;


    public bool jugadorCerca;

    virtual public void AtaqueBasico()
    {

    }

    virtual public void AtaqueEspecial()
    {

    }
    virtual public void DetectarJugador()
    {
        jugadorCerca = Physics.CheckSphere(gameObject.transform.position, rangoVision, layerJugador);
    }

    virtual public void mirarJugador()
    {
        Quaternion newRotation = Quaternion.LookRotation(_jugador.transform.position - transform.position);
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = newRotation;
        //transform.LookAt(_jugador.transform.position);
        //transform.Rotate(new Vector3(0, 90, 0), Space.Self);
    }
    virtual public void seguirJugador()
    {
        transform.Translate(new Vector3(0,0,1 * Time.deltaTime));
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
