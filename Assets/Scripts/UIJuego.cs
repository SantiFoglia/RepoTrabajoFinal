using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJuego : MonoBehaviour
{
    public Text textoMonedas;
    public Image barraVida;
    public Image barraMana;
    public Image barraStamina;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarVida();
        ActualizarMana();
        ActualizarStamina();
        ActualizarMonedas();
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
}
