using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float paddleSpeed = 10f; // Velocidad de la pala (ajustable desde el Inspector)

    void Update()
    {
        // Movimiento horizontal de la pala con teclas de flecha o "A" y "D"
        float horizontalInput = Input.GetAxis("Horizontal");   // Utilizo Input.GetAxis para obtener la entrada horizontal (teclas de flecha // "A" y "D")
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * paddleSpeed * Time.deltaTime;// Calculo el movimiento de la pala, multiplicado por la velocidad y el tiempo transcurrido(Time.deltaTime)// Esto me asegura que el movimiento sea independiente de la velocidad de fotogramas (frame rate)
       
        // Mueve la pala
        transform.Translate(movement);

        // Limitar el movimiento de la pala en los bordes de la pantalla // Mathf.Clamp asegura que la posición X de la pala se mantenga entre -2.3 y 2.3 en el eje X
        float xPos = Mathf.Clamp(transform.position.x, -2.3f, 2.3f); // Ajusta los valores según el tamaño de la pantalla
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z); // Actualiza la posición de la pala con la posición X limitada, manteniendo su posición en Y y Z
    }
}