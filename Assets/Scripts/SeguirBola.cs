using UnityEngine;

public class SeguirBola : MonoBehaviour
{
    // Aquí va la bola
    [Header("Configuración de Seguimiento")]
    public Transform objetivo; 

    [Tooltip("Controla qué tan rápido la cámara alcanza a su objetivo.")]
    public float velocidadSuavizado = 0.125f;

    // La distancia y ángulo que la cámara mantendrá con la bola.
    private Vector3 offset;

    void Start()
    {
        // Calculamos el offset inicial una sola vez.
        // Es la diferencia de posición entre la cámara y la bola.
        if (objetivo != null)
        {
            offset = transform.position - objetivo.position;
        }
    }

    // Usamos LateUpdate para la cámara, ya que se ejecuta después
    // de que todos los cálculos de físicas (Update) han terminado.
    void LateUpdate()
    {
        // Si no hay objetivo, no hacemos nada.
        if (objetivo == null) return;

        // 1. Calculamos la posición a la que la cámara QUIERE ir.
        Vector3 posicionDeseada = objetivo.position + offset;

        // 2. Movemos la cámara suavemente hacia esa posición usando Lerp.
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado);

        // 3. Aplicamos la nueva posición a la cámara.
        transform.position = posicionSuavizada;
    }
}