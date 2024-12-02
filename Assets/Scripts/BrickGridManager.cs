using UnityEngine;

public class BrickGridManager : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;  // Prefab del ladrillo
    [SerializeField] private int rows = 5;            // Número de filas
    [SerializeField] private int columns = 10;        // Número de columnas
    [SerializeField] private float spacing = 0.1f;    // Espaciado entre ladrillos

    // Array de colores para filas
    private Color[] rowColors;

    void Start()
    {
        // Inicializar los colores para las filas
        rowColors = new Color[]
        {
            Color.red,                   // Fila 1: Rojo
            new Color(1.0f, 0.75f, 0.8f), // Fila 2: Rosa
            Color.green,                 // Fila 3: Verde
            Color.yellow,                // Fila 4: Amarillo
            Color.blue                   // Fila 5: Azul
        };

        // Coloca la cuadrícula sobre el fondo
        GenerateBrickGrid();
    }

    void GenerateBrickGrid()
    {
        // Buscar el objeto con el tag "Background" que se uso como referencia para posicionar la cuadrícula
        GameObject background = GameObject.FindGameObjectWithTag("Background");

        if (background == null)
        {
            // Si no se encuentra el objeto "Background", muestra un error
            Debug.LogError("No se encontró un objeto con el tag 'Background'");
            return;
        }

         // Obtener el SpriteRenderer del objeto "Background" para obtener el tamaño del fondo
        SpriteRenderer backgroundRenderer = background.GetComponent<SpriteRenderer>();
        if (backgroundRenderer == null)
        {
            // Si el objeto "Background" no tiene un SpriteRenderer, muestra un error
            Debug.LogError("El objeto 'Background' no tiene un SpriteRenderer");
            return;
        }

        // Calcular las dimensiones del background // Obtener las dimensiones del fondo
        float backgroundWidth = backgroundRenderer.bounds.size.x;
        float backgroundHeight = backgroundRenderer.bounds.size.y;
        Vector3 backgroundPosition = background.transform.position;

        // Calcular los límites de la cuadrícula basados en el fondo (es decir, cuánta área ocupará la cuadrícula)
        float gridMinX = backgroundPosition.x - backgroundWidth / 2f; // Límite izquierdo
        float gridMaxY = backgroundPosition.y + backgroundHeight / 2f; // Límite superior
        float gridWidth = backgroundWidth * 0.8f;  // Ocupa el 80% del ancho del background
        float gridHeight = backgroundHeight * 0.5f; // Ocupa el 50% de la altura del background

        // Calcular el tamaño de cada ladrillo
        float brickWidth = gridWidth / columns; // Divide el ancho total entre el número de columnas
        float brickHeight = gridHeight / rows; // Divide la altura total entre el número de filas

        // Crear la cuadrícula de ladrillos
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calcular la posición de cada ladrillo dentro de la cuadrícula
                float x = gridMinX + col * brickWidth + brickWidth / 2f; // Centrado horizontalmente
                float y = gridMaxY - row * brickHeight - brickHeight / 2f; // Centrado verticalmente
                Vector3 position = new Vector3(x, y, 0); // La posición en el mundo 3D para el ladrillo

                // Instanciar el ladrillo
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity, transform);

                // Configurar el color del ladrillo según la fila
                SpriteRenderer brickRenderer = brick.GetComponent<SpriteRenderer>();
                if (brickRenderer != null && row < rowColors.Length)
                {
                    brickRenderer.color = rowColors[row]; // Asigna el color de la fila correspondiente
                }

                
            }
        }
    }
}
