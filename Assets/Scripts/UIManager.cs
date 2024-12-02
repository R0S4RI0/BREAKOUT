using UnityEngine;
using UnityEngine.UI;
using TMPro; // Asegúrate de tener este using para TextMeshPro

public class UIManager : MonoBehaviour
{
    [Header("Vidas")]
    [SerializeField] private Image noLivesImage; // Imagen que siempre se muestra
    [SerializeField] private Image[] livesImages; // Imágenes para 1, 2, y 3 vidas

    [Header("Puntaje")]
    [SerializeField] private TextMeshProUGUI scoreText; // Usamos TextMeshProUGUI para puntajes

    // Actualiza las vidas en la UI
       public void UpdateLives(int lives)
    {
        // Aseguro de que las imágenes de vidas estén activas o desactivadas correctamente
        for (int i = 0; i < livesImages.Length; i++)
        {
            if (livesImages[i] != null)
            {
                livesImages[i].gameObject.SetActive(i < lives); // Activar solo las imágenes correspondientes
            }
        }

        // Mostrar la imagen de "sin vidas" solo cuando el jugador no tenga vidas
        if (noLivesImage != null)
        {
            noLivesImage.gameObject.SetActive(lives <= 0); // Mostrar solo cuando las vidas sean cero
        }
    }


    // Actualiza el puntaje en la UI
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Mostrar solo el puntaje sin la palabra "Score"
        }
    }
}
