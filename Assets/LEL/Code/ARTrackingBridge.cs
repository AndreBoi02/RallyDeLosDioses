using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackingBridge : MonoBehaviour {
    [Header("Prefab")]
    [SerializeField] GameObject menuGO;
    [SerializeField] ARMenuPresenter activeMenu;

    ARTrackedImageManager arTrackedImageManager;
    [SerializeField] GameManager gameManager;

    public void StartGame() {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        if (arTrackedImageManager == null) return;
        arTrackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChange);
        CreateMenu();
    }

    private void OnDestroy() {
        arTrackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChange);
    }

    void OnImagesTrackedChange(ARTrackablesChangedEventArgs<ARTrackedImage> args) {
        foreach (var img in args.added)
            UpdateTrackedMenu(img);

        foreach (var img in args.updated)
            UpdateTrackedMenu(img);

        foreach (var img in args.removed)
            UpdateTrackedMenu(img.Value);
    }

    void CreateMenu() {
        menuGO = Instantiate(menuGO, Vector3.zero, Quaternion.identity);
        menuGO.name = gameManager.GetData().referenceImageName;
        RegisterActiveMenu(menuGO,
            gameManager.GetData().referenceImageName,
            gameManager.GetData().clues[gameManager.currentAttempt]);
        print($"Menu {menuGO.name} created");
    }

    public void RegisterActiveMenu(GameObject menu, string name, string clue) {
        activeMenu = menu.GetComponent<ARMenuPresenter>();
        activeMenu.SetName(name);
        activeMenu.SetClue(clue);
        activeMenu.SetButton(gameManager); 
    }

    [SerializeField] string trackedImageName;

    private void UpdateTrackedMenu(ARTrackedImage trackedImage) {
        if (trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None) {
            menuGO.SetActive(false);
            return;
        }

        menuGO.gameObject.SetActive(true);
        trackedImageName = trackedImage.name;
        menuGO.transform.position = trackedImage.transform.position;
        menuGO.transform.rotation = trackedImage.transform.rotation;
    }
}
