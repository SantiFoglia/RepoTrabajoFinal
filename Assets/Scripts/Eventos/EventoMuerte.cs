using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoMuerte : MonoBehaviour
{
    public void JugadorMuerto()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
