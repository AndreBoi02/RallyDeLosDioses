using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultipleImagesTrackingManager : MonoBehaviour {
    [Header("Prefabs")]
    [SerializeField] List<GameObject> prefabsToSpawn = new List<GameObject>();

    ARTrackedImageManager arTrackedImageManager;

    Dictionary<string, GameObject> arObjects;

    private void Start() {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        if (arTrackedImageManager == null) return;
        arTrackedImageManager.trackablesChanged.AddListener(OnImagesTrackedChange);
        arObjects = new Dictionary<string, GameObject>();

        SetObjectsInScene();
    }

    private void OnDestroy() {
        arTrackedImageManager.trackablesChanged.RemoveListener(OnImagesTrackedChange);
    }

    void SetObjectsInScene() {
        foreach (GameObject prefab in prefabsToSpawn) {
            GameObject tempGO = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            tempGO.name = prefab.name;
            tempGO.gameObject.SetActive(false);
            arObjects.Add(tempGO.name, tempGO);
        }
    }

    void OnImagesTrackedChange(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs) {
        foreach (var trackedImage in eventArgs.added) {
            UpdateTrackedImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.updated) {
            UpdateTrackedImage(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed) {
            UpdateTrackedImage(trackedImage.Value);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage) {
        if (trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None) {
            arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
        arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        arObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}
