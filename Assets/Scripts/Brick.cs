using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hitsToDestroy = 2; // Número de golpes necesarios para destruir el bloque (cambiado a 2)
    [SerializeField] private int scoreValue = 10;   // Puntos otorgados por destruir el bloque
    [SerializeField] private float fallSpeed = 2f;  // Velocidad de caída cuando el bloque es destruido
    private int currentHits = 0;                    // Contador de golpes recibidos
    private bool isFalling = false;                 // Indica si el bloque está cayendo

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta si el objeto que colisiona es la pelota (con el tag "Ball")
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Incrementa el contador de golpes que ha recibido el bloque
            currentHits++;

            // Si el bloque ha recibido los 2 golpes, comienza a caer y se destruye
            if (currentHits >= hitsToDestroy)
            {
                StartFalling(); // Llama a la función para iniciar la caída del bloque
            }
        }
    }

    private void Update()
    {
        // Si el bloque está cayendo, aplica la caída y verifica si salió de la pantalla
        if (isFalling)
        {
            // Mueve el bloque hacia abajo con la velocidad especificada por 'fallSpeed'
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            // Si el bloque cae por debajo de la pantalla, se destruye
            if (transform.position.y < Camera.main.ScreenToWorldPoint(Vector3.zero).y - 1f)
            {
                Destroy(gameObject); // Destruye el objeto bloque
            }
        }
    }

    private void StartFalling()
    {
        // Marca el bloque como en estado de caída
        isFalling = true;

        // Añade el puntaje al GameManager
        GameManager.Instance.AddScore(scoreValue);

        // Destruye el bloque después de iniciar la caída
        Destroy(gameObject);
    }
}
