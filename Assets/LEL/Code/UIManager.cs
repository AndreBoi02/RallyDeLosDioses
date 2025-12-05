using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] ARMenuPresenter activeMenu;

    public void RegisterActiveMenu(GameObject menu, string name, string clue) {
        activeMenu = menu.GetComponent<ARMenuPresenter>();
        activeMenu.SetName(name);
        activeMenu.SetClue(clue);
    }

}
