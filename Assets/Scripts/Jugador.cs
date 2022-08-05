using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    PlayerController _jugador;

    public static float vida;
    public static float vidaMax;
    public static float mana;
    public static float manaMax;
    public static float stamina;
    public static float staminaMax;

    public static int monedas;

    private void Awake()
    {
        _jugador = GetComponent<PlayerController>();

        vidaMax = 100f;
        vida = vidaMax;
        manaMax = 100f;
        mana = manaMax;
        staminaMax = 100f;
        stamina = staminaMax;

        monedas = 0;
    }

    private void Update()
    {
        regenerarStamina();
    }

    void regenerarStamina()
    {
        if (stamina < 100 && !_jugador.estaCorriendo && !_jugador.estaRolleando && _jugador.detectaPiso && !_jugador.estaAtacando && !_jugador.estaSaltando)
        {
            stamina += 0.02f;
        }
    }
}