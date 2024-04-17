using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipe : ScriptableObject
{
    
    public KitchenObjectSO initialKitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public float fryingSecondsMax = 3;
}
