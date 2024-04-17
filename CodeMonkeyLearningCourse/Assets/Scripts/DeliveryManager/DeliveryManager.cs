using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            throw new System.Exception("There are multiple DeliveryManager instances");
        }
    }


    public event EventHandler<OnWaitingRecipeChangeEventArgs> OnWaitingRecipeChange;

    public class OnWaitingRecipeChangeEventArgs : EventArgs {
    }


    private readonly List<RecipeSO> waitingRecipeList = new ();
    [SerializeField] private RecipeHolder recipeHolder;
    [SerializeField] private float deliveryTimeMax = 5f;
    [SerializeField] private int maxWaitingRecipe = 5;

    private float deliveryTime = 0f;

    private void Start() {
        deliveryTime = deliveryTimeMax;
    }

    private void Update() {
        if (waitingRecipeList.Count >= maxWaitingRecipe) return;

        deliveryTime -= Time.deltaTime;

        if (deliveryTime > 0) return;

        deliveryTime = deliveryTimeMax;
        CreateRecipeRequest();
    }

    private void CreateRecipeRequest() {
        RecipeSO recipe = recipeHolder.recipeList[UnityEngine.Random.Range(0, recipeHolder.recipeList.Length)];
        waitingRecipeList.Add(recipe);
        OnWaitingRecipeChange?.Invoke(this, new());
    }

    public void DeliverRecipe(PlateKitchenObject plate) {
        for (int i = 0; i < waitingRecipeList.Count; i++) {
            if (CheckRecipe(plate, waitingRecipeList[i])) {
                waitingRecipeList.RemoveAt(i);
                OnWaitingRecipeChange?.Invoke(this, new());
                return;
            }
        }
    }

    public bool CheckRecipe(PlateKitchenObject plate, RecipeSO recipe) {
        if (plate.GetIngredients().Count != recipe.KitchenObjectSOlist.Count) return false;

        // Check if all ingredients in the plate are in the recipe
        foreach (var ingredient in plate.GetIngredients()) {
            bool isInRecipe = false;
            foreach (var recipeIngredient in recipe.KitchenObjectSOlist) {
                if (ingredient == recipeIngredient) {
                    isInRecipe = true;
                    break;
                }
            }
            if (!isInRecipe) return false;
        }

        return true;
    }


    public List<RecipeSO> GetWaitingRecipes() {
        return waitingRecipeList;
    }
}
