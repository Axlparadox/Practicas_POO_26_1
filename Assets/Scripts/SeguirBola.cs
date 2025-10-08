using UnityEngine;

public class SeguirBola : MonoBehaviour
{
    // Aqu� va la bola
    [Header("Configuraci�n de Seguimiento")]
    public Transform objetivo; 

    [Tooltip("Controla qu� tan r�pido la c�mara alcanza a su objetivo.")]
    public float velocidadSuavizado = 0.125f;

    // La distancia y �ngulo que la c�mara mantendr� con la bola.
    private Vector3 offset;

    void Start()
    {
        // Calculamos el offset inicial una sola vez.
        // Es la diferencia de posici�n entre la c�mara y la bola.
        if (objetivo != null)
        {
            offset = transform.position - objetivo.position;
        }
    }

    // Usamos LateUpdate para la c�mara, ya que se ejecuta despu�s
    // de que todos los c�lculos de f�sicas (Update) han terminado.
    void LateUpdate()
    {
        // Si no hay objetivo, no hacemos nada.
        if (objetivo == null) return;

        // 1. Calculamos la posici�n a la que la c�mara QUIERE ir.
        Vector3 posicionDeseada = objetivo.position + offset;

        // 2. Movemos la c�mara suavemente hacia esa posici�n usando Lerp.
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado);

        // 3. Aplicamos la nueva posici�n a la c�mara.
        transform.position = posicionSuavizada;
    }
}