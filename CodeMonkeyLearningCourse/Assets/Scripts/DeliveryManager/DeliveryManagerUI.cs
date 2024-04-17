using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Start() {
        recipeTemplate.gameObject.SetActive(false);
        DeliveryManager.Instance.OnWaitingRecipeChange += DeliveryManager_OnWaitingRecipeChange;
    }

    private void DeliveryManager_OnWaitingRecipeChange(object sender, System.EventArgs e) {
        UpdateVisual();
    }


    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child != recipeTemplate) 
                Destroy(child.gameObject);
        }

        List<RecipeSO> waitingRecipeList = DeliveryManager.Instance.GetWaitingRecipes();

        for (int i = 0; i < waitingRecipeList.Count; i++) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);

            RecipeUI recipeUI = recipeTransform.GetComponent<RecipeUI>();
            recipeUI.SetRecipe(waitingRecipeList[i]);
        }
    }
}
