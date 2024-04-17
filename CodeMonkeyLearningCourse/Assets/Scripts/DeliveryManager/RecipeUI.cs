using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{

    public TextMeshProUGUI recipeTitle;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform container;


    private void Start() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipeSO) {
        recipeTitle.text = recipeSO.recipeName;
        Debug.Log(recipeSO.recipeName);

        foreach (Transform child in container) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        Debug.Log(recipeSO.KitchenObjectSOlist.Count);

        foreach (KitchenObjectSO ingredient in recipeSO.KitchenObjectSOlist) {
            Debug.Log(ingredient);
            var icon = Instantiate(iconTemplate, container);
            icon.gameObject.SetActive(true);
            icon.GetComponent<Image>().sprite = ingredient.iconSprite;
        }


    }
}
