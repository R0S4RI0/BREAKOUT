using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 8f; // Velocidad inicial de la pelota
    private float speed; // Velocidad actual de la pelota
    private Rigidbody2D rb; // Componente Rigidbody2D para manejar la f�sica de la pelota
    private bool isLaunched = false; // Estado de lanzamiento

    private Transform paddleTransform; // Referencia a la posici�n de la pala
    private float lowerBoundY = -5.0f; // L�mite inferior eje Y,  indica el borde donde la pelota muere

    private int destroyedBricks = 0; // Contador de ladrillos destruidos consecutivamente sin tocar la pala

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D de la pelota
        speed = initialSpeed; // Establece la velocidad inicial de la pelota

        // Encuentra la pala usando el tag "Paddle"
        GameObject paddle = GameObject.FindWithTag("Paddle");
        if (paddle != null)
        {
            paddleTransform = paddle.transform; // Si encuentra la pala, obtiene su referencia
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con el tag 'Paddle'. Aseg�rate de que la pala tenga asignado el tag.");
        }

        ResetBallPosition(); // Coloca la pelota sobre la pala al inicio
    }

    void Update()
    {
        if (!isLaunched && paddleTransform != null)
        {
            // Lanza la pelota cuando presionas la tecla Espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchBall();
            }
        }

        // Verificar si la pelota se ha salido de los l�mites (por debajo de 'lowerBoundY')
        CheckOutOfBounds();
    }

    void LaunchBall()
    {
        // Lanza la pelota en una direcci�n aleatoria hacia arriba cuando el jugador presiona Espacio
        isLaunched = true; // Marca que la pelota ha sido lanzada
        this.transform.parent = null; // Desasocia la pelota de la pala (para que no siga movi�ndose con la pala)
        // Genera una direcci�n aleatoria en X e Y
        float randomX = Random.Range(-0.7f, 0.7f);
        float randomY = Random.Range(0.8f, 1f); // Asegura que la pelota siempre suba
        Vector2 direction = new Vector2(randomX, randomY).normalized; // Normaliza la direcci�n para que la velocidad sea constante
        rb.velocity = direction * speed; // Asigna la velocidad de la pelota con la direcci�n calculada
    }

    void CheckOutOfBounds()
    {
        // Si la pelota cae por debajo del l�mite inferior, pierde una vida
        if (transform.position.y < lowerBoundY)
        {
            GameManager.Instance.LoseLife(); // Llama al m�todo para perder una vida
            ResetBallPosition(); // Restablece la posici�n de la pelota sobre la pala
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Colisiones con la pala
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector3 paddlePosition = collision.transform.position; // Posici�n de la pala
            float paddleWidth = collision.collider.bounds.size.x; // Ancho de la pala

            // Calcula el porcentaje de impacto de la pelota en la pala (0 a 1)
            float hitPercent = (transform.position.x - paddlePosition.x) / paddleWidth + 0.5f;
            hitPercent = Mathf.Clamp01(hitPercent); // Limita el porcentaje entre 0 y 1

            // Mapea el porcentaje al rango de �ngulos (45 a 135 grados)
            float minAngle = 45f;
            float maxAngle = 135f;
            float bounceAngle = Mathf.Lerp(maxAngle, minAngle, hitPercent);

            // Calcula la nueva direcci�n de la bola en funci�n del �ngulo de rebote
            Vector2 newDirection = Quaternion.Euler(0, 0, bounceAngle) * Vector2.right;

            // Ajusta la velocidad manteniendo la direcci�n
            rb.velocity = newDirection.normalized * speed;
        }

        // Colisiones con ladrillos
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); // Destruye el ladrillo al ser tocado

            // Invertir la direcci�n en Y para el rebote
            Vector2 newDirection = new Vector2(rb.velocity.x, -rb.velocity.y);
            rb.velocity = newDirection;

            // Aumenta la velocidad tras romper un ladrillo
            speed = Mathf.Min(speed + 0.5f, 15f); // Limita la velocidad m�xima a 15
            rb.velocity = rb.velocity.normalized * speed;

            // Incrementa el contador de ladrillos destruidos
            destroyedBricks++;

            // Si la pelota golpea 4 ladrillos consecutivos sin tocar la pala, agrandar la Pala
            if (destroyedBricks >= 4)
            {
                GrowPaddle();
                destroyedBricks = 0; // Reinicia el contador despu�s de agrandar la pala
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Colisiones con el prefab "recuperarVida"
        if (other.CompareTag("AddVida"))
        {
            GameManager.Instance.AddLife(); // Recupera una vida
            Destroy(other.gameObject); // Destruye el prefab tras la colisi�n
        }
    }

    void GrowPaddle()
    {
        // Agranda la pala si se ha cumplido la condici�n de 4 ladrillos destruidos
        if (paddleTransform != null)
        {
            Vector3 currentScale = paddleTransform.localScale; // Obtiene la escala actual de la pala
            float maxWidth = 5.0f; // Tama�o m�ximo del Paddle en X

            // Incrementar la escala del Paddle en X pero no exceder el l�mite
            paddleTransform.localScale = new Vector3(
                Mathf.Min(currentScale.x + 0.5f, maxWidth), // Incrementa 0.5 en X
                currentScale.y,
                currentScale.z
            );
        }
    }

    void ResetBallPosition()
    {
        // Coloca la pelota sobre la pala, ligeramente encima
        if (paddleTransform != null)
        {
            transform.position = new Vector3(paddleTransform.position.x, paddleTransform.position.y + 0.5f, 0f);
            isLaunched = false; // La pelota a�n no ha sido lanzada
            rb.velocity = Vector2.zero; // Detiene la pelota
        }
    }
}
