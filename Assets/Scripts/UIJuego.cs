using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJuego : MonoBehaviour
{
    //public static UIJuego unicaInstancia;

    public Text textoMonedas;
    public Text textoCantidadPociones;
    public Image barraVida;
    public Image barraMana;
    public Image barraStamina;

    //private void Awake()
    //{
    //    if (UIJuego.unicaInstancia == null)
    //    {
    //        UIJuego.unicaInstancia = this;

    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //    DontDestroyOnLoad(gameObject);

    //}

    void Update()
    {
        ActualizarVida();
        ActualizarMana();
        ActualizarStamina();
        ActualizarMonedas();
        ActualizarPociones();

        
    }
    private void ActualizarVida()
    {
        barraVida.fillAmount = Jugador.vida / Jugador.vidaMax;
    }
    private void ActualizarMana()
    {
        barraMana.fillAmount = Jugador.mana / Jugador.manaMax;
    }
    private void ActualizarStamina()
    {
        barraStamina.fillAmount = Jugador.stamina / Jugador.staminaMax;
    }
    void ActualizarMonedas()
    {
        if (Jugador.monedas<999999)
        {
            textoMonedas.text = "Monedas: " + Jugador.monedas;
        }
        else
        {
            textoMonedas.text = "Monedas: " + 999999;
        }
        
    }
    void ActualizarPociones()
    {
        textoCantidadPociones.text = "x" + Jugador.pocionesVida;
    }
}
