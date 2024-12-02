using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class volverMenuPrincipal : MonoBehaviour
{
    [SerializeField] private string GameOverScene = "GameOverScene"; // Escena de Game Over
    [SerializeField] private string WinnerScene = "WinnerScene"; // Escena de Victory

    void Start()
    {
       
    }

    
    private void Update()
    {
        // Si estamos en la pantalla de "GameOver" o "Winner", y el jugador presiona espacio
        if ((SceneManager.GetActiveScene().name == GameOverScene || SceneManager.GetActiveScene().name == WinnerScene)
            && Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToMainMenu(); // Regresar al men� principal
        }
    }
    // Funci�n para regresar al men� principal
    private void ReturnToMainMenu()
    {
        // Cargar la escena principal (men�)
        SceneManager.LoadScene("MainMenu"); 
    }
}
