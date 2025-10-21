using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // Movimiento
    public float velocidad = 5f;
    public float gravedad = -9.8f;
    private CharacterController controller;
    private Vector3 velocidadVectical;

    // Variables vista
    public Transform camara;
    public float sensibilidadMouse = 200f;
    private float rotacionXVertical = 0f;





    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Esta linea bloquea el puntero del mouse en los limites de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorVista();
        ManejadorMovimiento();
    }

    void ManejadorVista()
    {
        //1. leer el input del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        //2.Construir la rotacion horizontal
        transform.Rotate(Vector3.up * mouseX);

        //3. registro de la rotacion vertical
        rotacionXVertical -= mouseY;

        //4. limitar la rotacion vectical

        Mathf.Clamp(rotacionXVertical, -90f, 90f);

        // 5. Aplicar la rotacion
        // son los ejes         X           Y  Z
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);


    }

    void ManejadorMovimiento()
    {
        //1 leer el input de movimiento (WASD o la flechas de direccion)
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        //2 Crear el vector de movimiento
        //se almacena de forma local el registro de direccion de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;

        //3 Mover el CharacterController
        controller.Move(direccion * velocidad * Time.deltaTime);

        //4 Aplicar la gravedad
        //Registro si estoy en el piso para un futuro comportamiento de salto
        if (controller.isGrounded && velocidadVectical.y < 0)
        {
            velocidadVectical.y = -2f;//Una pequela fuerza hacia abajo para mantenerlo pegado al piso
        }

        //Aplicamos la acelecarion de la gravedad
        velocidadVectical.y += gravedad * Time.deltaTime;

        //Movemos el controlador hacia abajo
        controller.Move(velocidadVectical * Time.deltaTime);

    }
}
