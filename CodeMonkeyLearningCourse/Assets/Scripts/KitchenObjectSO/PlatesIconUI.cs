using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconPattern;

    private void Awake() {
    }

    private void Start() {
        iconPattern.gameObject.SetActive(false);
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (Transform child in transform) {
            if (child != iconPattern)
                Destroy(child.gameObject);
        }


        foreach(KitchenObjectSO inredient in plateKitchenObject.GetIngredients()) {
            Transform icon = Instantiate(iconPattern, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(inredient);
        }
    }
}
