using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necesario para trabajar con los botones

public class MainMenuManager : MonoBehaviour
{
    // Referencias a los botones
    [SerializeField] private Button playButton; // Botón de Jugar
    [SerializeField] private Button optionsButton; // Botón de Opciones

    private void Start()
    {
        // Aseguramos que los botones estén correctamente asignados en el Inspector
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked); // Añado un listener para que se ejecute una función cuando se haga clic en el botón de "Jugar"
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OnOptionsButtonClicked); // Añado un listener para que se ejecute una función cuando se haga clic en el botón de "Opciones"
        }
    }

    // Función que se ejecutará cuando se haga clic en el botón Jugar
    private void OnPlayButtonClicked()
    {
        // Aquí cargo la primera escena del juego 
        SceneManager.LoadScene("Level1Scene");  
    }

    // Función que se ejecutará cuando se haga clic en el botón Opciones
    private void OnOptionsButtonClicked()
    {
        // Aquí cargare obciones de volumen 
        SceneManager.LoadScene("OptionsScene");  
    }
}