using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Action<int> onEndGame;
    [SerializeField] PanelManager panelManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] ARTrackingBridge arBridge;

    [Header("Banco de preguntas")]
    [SerializeField] List<TrackableCardData> cards;

    [SerializeField] TrackableCardData currentCard;

    [Header("Track them Hoes")]
    [SerializeField] int cardIdx;
    public int currentAttempt = 0;
    [SerializeField] int currentSection = 0;
    [SerializeField] int score = 0;

    private void OnEnable() {
        panelManager.onStartGame += StartGame;
    }

    private void OnDisable() {
        panelManager.onStartGame -= StartGame;
    }

    void StartGame() {
        SelectCard();
        FisrtClue();
        arBridge.StartGame();
    }

    [SerializeField] ARMenuPresenter activeMenu;

    void FisrtClue() {
        panelManager.ActivateFirstClue(currentSection, currentCard.clues[currentAttempt]);
    }

    void SelectCard() {
        currentSection++;
        cardIdx = UnityEngine.Random.Range(0, cards.Count);
        currentCard = cards[cardIdx];
    }

    public bool ValidAnswer(string name) {
        if(currentCard.referenceImageName == name) {
            SelectCard();
            return true;
        }
        return false;
    }

    public TrackableCardData GetData() {
        return currentCard;
    }
}
