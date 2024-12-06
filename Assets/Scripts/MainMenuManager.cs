using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject panelOpciones;  // El panel de opciones
    [SerializeField] private Button botonJugar;  // El bot�n de jugar
    [SerializeField] private Button botonOpciones;  // El bot�n de opciones
    [SerializeField] private Button botonSalir;  // El bot�n de salir
    [SerializeField] private Button botonVolver;  // El bot�n de volver (en el panel de opciones)

    void Start()
    {
        // Asignar las acciones de los botones
        botonJugar.onClick.AddListener(CargarJuego);
        botonOpciones.onClick.AddListener(AbrirPanelOpciones);
        botonSalir.onClick.AddListener(SalirJuego);
        botonVolver.onClick.AddListener(CerrarPanelOpciones);
    }

    // M�todo para cargar la escena del juego
    void CargarJuego()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    // M�todo para abrir el panel de opciones
    void AbrirPanelOpciones()
    {
        panelOpciones.SetActive(true);

    }

    // M�todo para cerrar el panel de opciones
    void CerrarPanelOpciones()
    {
        panelOpciones.SetActive(false);

    }

    // M�todo para salir del juego
    void SalirJuego()
    {
        Application.Quit();  // Cierra la aplicaci�n
        Debug.Log("Juego cerrado"); 
    }
}

