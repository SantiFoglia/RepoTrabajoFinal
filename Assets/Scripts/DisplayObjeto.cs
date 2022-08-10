using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayObjeto : MonoBehaviour
{
    public Objetos _objeto;
    public Text textoNombre;
    public Text textoDescripcion;
    public Image imagenObjeto;

    void Start()
    {
        _objeto.MostrarData();
        textoNombre.text = _objeto.nombreObjeto;
        textoDescripcion.text = _objeto.descripcionObjeto;
        imagenObjeto.sprite = _objeto.dibujoObjeto;
    }

}
