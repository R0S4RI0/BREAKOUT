using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausaUI;

    private bool isPaused = false; // Para controlar si el juego está en pausa.

    private void Update()
    {
        // Detecta si se presiona la tecla flecha arriba.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isPaused)
            {
                Reanudar(); // Si está pausado, reanuda el juego.
            }
            else
            {
                Pausa(); // Si no está pausado, pausa el juego.
            }
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausaUI.SetActive(true);
        isPaused = true; // Marca el estado como pausado.
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausaUI.SetActive(false);
        isPaused = false; // Marca el estado como no pausado.
    }

    public void cerrar()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
