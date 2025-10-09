using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text textoPuntaje;
    private int puntajeActual = 0;
    private Pin[] pinos;

    private bool revisandoPuntaje = false;

    void Start()
    {
        pinos = FindObjectsByType<Pin>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (revisandoPuntaje)
        {
            CalcularPuntaje();
        }
    }

    public void CalcularPuntaje()
    {
        int puntajeDeRonda = 0;
        foreach (Pin pin in pinos)
        {
            if (pin.EstaCaido())
            {
                puntajeDeRonda++;
            }
        }
        puntajeActual = puntajeDeRonda;
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeActual;
        }
    }

    public void IniciarRevisionDePuntaje()
    {
        revisandoPuntaje = true;
        StartCoroutine(DetenerRevisionDespuesDe(7f));
    }

    private IEnumerator DetenerRevisionDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        revisandoPuntaje = false;
    }
}