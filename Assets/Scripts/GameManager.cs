using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Instancia estática del GameManager para que sea accesible globalmente


    private int score = 0; // Puntaje actual
    private int lives = 3; // Mantener el contador de vidas
    private int totalBricks; // Total de ladrillos en el nivel
    private int destroyedBricks = 0;  // Contador de ladrillos destruidos

    [SerializeField] private string gameOverScene = "GameOverScene"; // Escena de Game Over
    [SerializeField] private string WinnerScene = "WinnerScene"; // Escena de Victory

    // Referencia al UIManager para actualizar la interfaz de usuario
    private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Si no hay otra instancia, esta es la instancia principal
            // No destruir el GameManager para mantenerlo entre las escenas 
        }
        else
        {
            Destroy(gameObject); // Eliminar si ya existe una instancia,  destruimos esta para evitar duplicados
        }
    }

    private void Start()
    {
        // Buscar al UIManager en la escena actual 
        uiManager = FindObjectOfType<UIManager>();
        CountTotalBricks(); // Contar todos los ladrillos al inicio
        UpdateUI(); // Sincronizar UI con vidas y puntaje
    }


    // Método para sumar puntos al marcador de puntos
    public void AddScore(int value)
    {
        score += value; // Sumar el valor al puntaje actual
        UpdateUI(); // Reflejar el puntaje en la interfaz
    }
    
    // Método para perder una vida
    public void LoseLife()
    {
        lives--; // Restar una vida
        Debug.Log("Vidas restantes: " + lives);  // Mostrar la cantidad de vidas restantes en la consola para depuración

        // Actualizar la UI con las vidas restantes
        UpdateUI();

        if (lives <= 0)
        {
            GameOver(); // Si no quedan vidas, mostrar la pantalla de "Game Over"
           
        }
    }

    // Método que maneja el fin del juego
    private void GameOver()
    {
        // Cargar la escena de Game Over
        SceneManager.LoadScene(gameOverScene);
    }
    
    // Método para volver al menú principal
    private void ReturnToMainMenu()
    {
        // Asegurarse de que cuando volvemos al Main Menu, se destruye el GameManager actual
        Destroy(gameObject); // Destruir el GameManager para que cuando se inicie un nuevo juego, se restablezca todo
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }

    // Método para obtener el número de vidas actuales
    public int GetLives()
    {
        return lives; // Devuelve el número de vidas restantes
    }

    // Contar el total de ladrillos en la escena
    private void CountTotalBricks()
    {
        // Contar todos los ladrillos en la escena al inicio del nivel
        totalBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    // Método que se llama cuando un ladrillo es destruido
    public void BrickDestroyed()
    {
        destroyedBricks++; // Aumentar el número de ladrillos destruidos
        AddScore(10); // Sumar 10 puntos por cada ladrillo destruido

        if (destroyedBricks >= totalBricks)
        {
            // Si todos los ladrillos han sido destruidos, pasar a la siguiente escena
            NextLevel();
        }
    }

    // Método para avanzar al siguiente nivel
    private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // Obtener el índice de la siguiente escena

        // Si hay más escenas, cargar la siguiente. Si no, cargar la escena de victoria.
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Cargar siguiente nivel
        }
        else
        {
            ShowVictoryScreen(); // Si no hay más niveles, mostrar pantalla de victoria
           
        }
    }

    // Método para mostrar la pantalla de victoria cuando todos los niveles son completados
    private void ShowVictoryScreen()
    {
        // Mostrar la pantalla de victoria si todos los ladrillos son destruidos
        SceneManager.LoadScene(WinnerScene);
    }

    // Método para añadir una vida al jugador
    public void AddLife()
    {
        // Asegurarse de no tener más de 3 vidas
        lives = Mathf.Min(lives + 1, 3);
        UpdateUI(); // Actualizar la UI después de agregar una vida
    }

    // Método que actualiza la UI con el puntaje y las vidas actuales
    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateLives(lives); // Actualizar las vidas en la UI
            uiManager.UpdateScore(score); // Actualizar el puntaje en la UI
        }
    }

    // Método adicional para reiniciar el juego cuando se comienza desde el Main Menu
    public void ResetGame()
    {
        score = 0; // Reiniciar el puntaje
        lives = 3; // Restablecer las vidas
        destroyedBricks = 0; // Reiniciar el contador de ladrillos destruidos
        UpdateUI(); // Actualizar la UI con los valores iniciales
    }
}
