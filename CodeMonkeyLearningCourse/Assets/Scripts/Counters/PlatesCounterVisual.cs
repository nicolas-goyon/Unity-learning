using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlatesCounter;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private GameObject platePrefab;
    [SerializeField] private Transform plateSpawnPoint;
    [SerializeField] private float plateSeparation = 0.2f;

    private GameObject[] plates;

    private int plateCount = 0;
    private int maxPlateCount = 5;

    private void Start() {
        platesCounter.OnPlateCountChanged += PlatesCounter_OnPlateCountChanged;
        maxPlateCount = platesCounter.MaxPlateCount;
        plates = new GameObject[maxPlateCount];
    }

    private void PlatesCounter_OnPlateCountChanged(object sender, PlateCountChangedEventArgs e) {
        plateCount = e.plateCount;
        UpdateVisual();
    }

    private void UpdateVisual() {
        for (int i = 0; i < maxPlateCount; i++) {
            if (plates[i] != null) {
                Destroy(plates[i]);
            }

            if (i < plateCount) {
                plates[i] = Instantiate(platePrefab, plateSpawnPoint.position + new Vector3(0, i * plateSeparation, 0), Quaternion.identity);
            }
        }
    }



}
