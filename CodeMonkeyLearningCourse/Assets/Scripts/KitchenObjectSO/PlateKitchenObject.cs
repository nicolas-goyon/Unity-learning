using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredients; // List of ingredients that can be added to the plate
    private readonly List<KitchenObjectSO> ingredients = new();

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO ingredient;
    }

    public bool TryAddIngredient(KitchenObjectSO ingredient) {
        if (ingredients.Contains(ingredient)) {
            return false;
        }

        if (!validIngredients.Contains(ingredient)) {
            return false;
        }
        
        ingredients.Add(ingredient);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { ingredient = ingredient });

        return true;

    }

    public List<KitchenObjectSO> GetIngredients() {
        return ingredients;
    }
}
