using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJuegoPrincipal : MonoBehaviour
{
    private void Awake()
    {
        ManagerSonido.unicaInstancia.PlayMusicaFondo();
        ManagerSonido.unicaInstancia.StopMusica2();
        ManagerSonido.unicaInstancia.StopMusica3();
    }
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
