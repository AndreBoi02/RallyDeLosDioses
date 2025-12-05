using System;
using TMPro;
using UnityEngine;

public class PanelManager : MonoBehaviour {
    public Action onStartGame;
    [SerializeField] GameManager gameManager;

    [Header("Paneles")]
    [SerializeField] GameObject menuInstrucciones;
    [SerializeField] GameObject menuNombreDelJugador;
    [SerializeField] GameObject menuFinal;
    [SerializeField] GameObject Hud;

    [SerializeField] TMP_Text textoPuntosFinales;
    [SerializeField] TMP_Text section;
    [SerializeField] TMP_Text clue;

    private void OnEnable() {
        gameManager.onEndGame += Final;
    }

    private void OnDisable() {
        gameManager.onEndGame -= Final;
    }

    public void Siguiente() {
        menuInstrucciones.SetActive(false);
        menuNombreDelJugador.SetActive(true);
    }

    public void Conmenzar() {
        menuNombreDelJugador.SetActive(false);
        onStartGame?.Invoke();
    }

    public void Final(int puntos) {
        Hud.SetActive(false);
        menuFinal.SetActive(true);
        textoPuntosFinales.text = $"Gracias por jugar!!!\r\nTu puntuación es:\r\n{puntos}";
    }

    public void ActivateFirstClue(int i, string txt) {
        section.text = $"La pista se encuentra en la sección {i}";
        clue.text = $"Primera pista: {txt}";
        section.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void CloseCluePanel() {
        clue.gameObject.transform.parent.gameObject.SetActive(false);
        Hud.SetActive(true);
    }
}
