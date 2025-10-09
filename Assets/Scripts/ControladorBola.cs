using UnityEngine;

public class ControladorBola : MonoBehaviour
{
    public float fuerzaDeLanzamiento = 1000f;
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;

    private Rigidbody rb;
    private bool haSidoLanzada = false;

    public CameraFollow cameraFollow;
    public ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!haSidoLanzada)
        {
            Apuntar();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LanzarBola();
            }
        }
    }

    void Apuntar()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);
        Vector3 posicionActual = transform.position;
        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);
        transform.position = posicionActual;
    }

    void LanzarBola()
    {
        haSidoLanzada = true;
        rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);

        if (cameraFollow != null)
        {
            cameraFollow.IniciarSeguimiento();
        }

        if (scoreManager != null)
        {
            scoreManager.IniciarRevisionDePuntaje();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            if (cameraFollow != null)
            {
                cameraFollow.DetenerSeguimiento();
            }
        }
    }
}