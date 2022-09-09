using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJuego : MonoBehaviour
{
    //public static UIJuego unicaInstancia;

    public Text textoMonedas;
    public Text textoMonedasFinal;
    public Text textoCantidadPociones;
    public Text textoNumeroVida;
    public Image barraVida;
    public Image barraMana;
    public Image barraStamina;
    public Image almaGolem;

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
        ActualizarMonedasFinal();
        MostrarAlma();

        
    }
    private void ActualizarVida()
    {
        barraVida.fillAmount = Jugador.vida / Jugador.vidaMax;
        textoNumeroVida.text = Jugador.vida + "/" + Jugador.vidaMax;
    }
    private void ActualizarMana()
    {
        barraMana.fillAmount = Jugador.mana / Jugador.manaMax;
    }
    private void ActualizarStamina()
    {
        barraStamina.fillAmount = Jugador.stamina / Jugador.staminaMax;
    }
    private void ActualizarMonedas()
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
    private void ActualizarPociones()
    {
        textoCantidadPociones.text = "x" + Jugador.pocionesVida;
    }
    private void ActualizarMonedasFinal()
    {
        textoMonedasFinal.text = $"Conseguiste {Jugador.monedas} Monedas";
    }
    private void MostrarAlma()
    {
        if (Jugador.poderAlmaGolem)
        {
            almaGolem.gameObject.SetActive(true);
        }
        else
        {
            almaGolem.gameObject.SetActive(false);
        }
    }


    public void ActivarMouse()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void DesactivarMouse()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
