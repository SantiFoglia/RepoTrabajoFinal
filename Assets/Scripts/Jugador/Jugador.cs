using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jugador : MonoBehaviour
{
    PlayerController _jugador;
    Rigidbody rb;

    [SerializeField] private UnityEvent PlayerDeath;

    public static float vida;
    public static float vidaMax;
    public static float mana;
    public static float manaMax;
    public static float stamina;
    public static float staminaMax;

    static public bool jugadorInvulnerable;

    public static int monedas;

    private void Awake()
    {
        _jugador = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();

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
        ReiniciarApuntar();

        if (vida <=0)
        {
            PlayerDeath?.Invoke();
        }
    }

    void regenerarStamina()
    {
        if (stamina < 100 && !_jugador.estaCorriendo && !_jugador.estaRolleando && _jugador.detectaPiso && !_jugador.estaAtacando && !_jugador.estaSaltando)
        {
            stamina += 0.04f;
        }
    }
    public void retrocesoGolpe()
    {
        
    }
    public void ReiniciarApuntar()
    {
        if (Enemigos.enemigoMuriendo)
        {
            StartCoroutine(delayMuerteEnemigo());
        }
    }

    IEnumerator delayMuerteEnemigo()
    {
        yield return new WaitForSeconds(1f);
        Enemigos.enemigoMuriendo = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Roca") && !jugadorInvulnerable)
        {
            jugadorInvulnerable = true;
            StartCoroutine(tiempoInvulnerable());
        }
        if (other.CompareTag("Enemy") && !jugadorInvulnerable)
        {
            vida -= Enemigos.da�oContacto;
            jugadorInvulnerable = true;
            StartCoroutine(tiempoInvulnerable());
        }
    }

    IEnumerator tiempoInvulnerable()
    {
        yield return new WaitForSeconds(0.4f);
        jugadorInvulnerable = false;
    }
}
