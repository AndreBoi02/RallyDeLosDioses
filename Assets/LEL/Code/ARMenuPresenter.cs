using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ARMenuPresenter : MonoBehaviour {
    [Header("Referencias UI internas")]
    public GameManager gameManager;
    public TMP_Text clueText;
    public TMP_Text headerText;
    public Button button;
    public Image backGround;
    Color color;

    public void SetName(string text) {
        headerText.text = text;
    }

    public void SetClue(string text) {
        clueText.text = text;
    }

    public void GetAnswer(bool isCorrect) {
        if (isCorrect) {
            color = Color.green;
        }
        else
        {
            color = Color.red;
        }
        StartCoroutine("ReactToAnswer");
    }

    IEnumerator ReactToAnswer() {
        backGround.color = color;
        yield return new WaitForSeconds(1);
        backGround.color = Color.black;
    }

    public void SetButton(GameManager manager) {
        button.onClick.AddListener(Buttont);
        gameManager = manager;
    }

    public void Buttont() {
        GetAnswer(gameManager.ValidAnswer(headerText.text));
    }
}
