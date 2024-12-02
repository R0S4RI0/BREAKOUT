using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necesario para trabajar con los botones

public class MainMenuManager : MonoBehaviour
{
    // Referencias a los botones
    [SerializeField] private Button playButton; // Bot�n de Jugar
    [SerializeField] private Button optionsButton; // Bot�n de Opciones

    private void Start()
    {
        // Aseguramos que los botones est�n correctamente asignados en el Inspector
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked); // A�ado un listener para que se ejecute una funci�n cuando se haga clic en el bot�n de "Jugar"
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OnOptionsButtonClicked); // A�ado un listener para que se ejecute una funci�n cuando se haga clic en el bot�n de "Opciones"
        }
    }

    // Funci�n que se ejecutar� cuando se haga clic en el bot�n Jugar
    private void OnPlayButtonClicked()
    {
        // Aqu� cargo la primera escena del juego 
        SceneManager.LoadScene("Level1Scene");  
    }

    // Funci�n que se ejecutar� cuando se haga clic en el bot�n Opciones
    private void OnOptionsButtonClicked()
    {
        // Aqu� cargare obciones de volumen 
        SceneManager.LoadScene("OptionsScene");  
    }
}