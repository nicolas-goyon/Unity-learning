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


    public event EventHandler<RecipeRequestCreationEventArgs> RecipeRequestCreation;

    public class RecipeRequestCreationEventArgs : EventArgs {
        public RecipeSO newRecipe;
    }


    private readonly List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
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
        Debug.Log("Create Recipe Request: " + recipe.recipeName);
        waitingRecipeList.Add(recipe);
        RecipeRequestCreation?.Invoke(this, new RecipeRequestCreationEventArgs { newRecipe = recipe });
    }

    public void DeliverRecipe(PlateKitchenObject plate) {
        for (int i = 0; i < waitingRecipeList.Count; i++) {
            if (CheckRecipe(plate, waitingRecipeList[i])) {
                Debug.Log("Recipe Delivered: " + waitingRecipeList[i].recipeName);
                waitingRecipeList.RemoveAt(i);
                return;
            }
        }
        Debug.Log("Recipe not found");
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
}
