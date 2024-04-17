using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipe : ScriptableObject
{
    
    public KitchenObjectSO initialKitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public float burningSecondsMax = 3;
}
