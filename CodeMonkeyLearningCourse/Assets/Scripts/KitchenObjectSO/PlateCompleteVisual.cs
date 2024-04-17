using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [SerializeField] private KitchenObjectSO_GameObject[] ingredientVisuals;

    [System.Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var ingredientVisual in ingredientVisuals) {
            ingredientVisual.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        GameObject ingredientVisual = GetIngredientVisual(e.ingredient);
        if (ingredientVisual != null) {
            ingredientVisual.SetActive(true);
        }
    }

    private GameObject GetIngredientVisual(KitchenObjectSO ingredient) {
        foreach (var ingredientVisual in ingredientVisuals) {
            if (ingredientVisual.kitchenObjectSO == ingredient) {
                return ingredientVisual.gameObject;
            }
        }

        return null;
    }
}
