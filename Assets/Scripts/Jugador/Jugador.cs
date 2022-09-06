using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jugador : MonoBehaviour
{
    PlayerController _jugador;
    Rigidbody rb;

    public AudioClip recibirDaño;
    public AudioClip recibirDaño2;

    [SerializeField] private UnityEvent PlayerDeath;

    public static float vida;
    public static float vidaMax;
    public static float mana;
    public static float manaMax;
    public static float stamina;
    public static float staminaMax;

    public static bool jugadorInvulnerable;

    public static int monedas;

    public static int pocionesVida;

    public static bool poderAlmaGolem;

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

        pocionesVida = 2;
    }

    private void Update()
    {
        limitarParametros();

        regenerarStamina();
        regenerarMana();
        ReiniciarApuntar();

        if (vida <=0)
        {
            PlayerDeath?.Invoke();
            ManagerSonido.unicaInstancia.efectoSonido.Stop();
            ManagerSonido.unicaInstancia.efectoSonido2.Stop();
        }
    }

    void regenerarStamina()
    {
        if (stamina < 100 && !_jugador.estaCorriendo && !_jugador.estaRolleando && _jugador.detectaPiso && !_jugador.estaAtacando && !_jugador.estaSaltando)
        {
            stamina += 0.04f;
        }
    }
    void regenerarMana()
    {
        if (mana < 100 && !_jugador.poderActivado)
        {
            mana += 0.005f;
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
    public void limitarParametros()
    {
        if (vida > vidaMax)
        {
            vida = vidaMax;
        }
        if (mana > manaMax)
        {
            mana = manaMax;
        }
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
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
            ManagerSonido.unicaInstancia.PlayEfectoSonidoRandom(recibirDaño, recibirDaño2);
        }
        if (other.CompareTag("Enemy") && !jugadorInvulnerable)
        {
            vida -= Enemigos.dañoContacto;
            jugadorInvulnerable = true;
            StartCoroutine(tiempoInvulnerable());
            ManagerSonido.unicaInstancia.PlayEfectoSonidoRandom(recibirDaño, recibirDaño2);
        }
    }

    IEnumerator tiempoInvulnerable()
    {
        yield return new WaitForSeconds(0.4f);
        jugadorInvulnerable = false;
    }
}
