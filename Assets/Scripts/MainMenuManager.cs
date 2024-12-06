using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject panelOpciones;  // El panel de opciones
    [SerializeField] private Button botonJugar;  // El botón de jugar
    [SerializeField] private Button botonOpciones;  // El botón de opciones
    [SerializeField] private Button botonSalir;  // El botón de salir
    [SerializeField] private Button botonVolver;  // El botón de volver (en el panel de opciones)

    void Start()
    {
        // Asignar las acciones de los botones
        botonJugar.onClick.AddListener(CargarJuego);
        botonOpciones.onClick.AddListener(AbrirPanelOpciones);
        botonSalir.onClick.AddListener(SalirJuego);
        botonVolver.onClick.AddListener(CerrarPanelOpciones);
    }

    // Método para cargar la escena del juego
    void CargarJuego()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    // Método para abrir el panel de opciones
    void AbrirPanelOpciones()
    {
        panelOpciones.SetActive(true);

    }

    // Método para cerrar el panel de opciones
    void CerrarPanelOpciones()
    {
        panelOpciones.SetActive(false);

    }

    // Método para salir del juego
    void SalirJuego()
    {
        Application.Quit();  // Cierra la aplicación
        Debug.Log("Juego cerrado"); 
    }
}

