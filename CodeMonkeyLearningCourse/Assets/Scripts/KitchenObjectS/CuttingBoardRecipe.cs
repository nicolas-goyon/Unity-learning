using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingBoardRecipe : ScriptableObject
{
    
    public KitchenObjectSO initialKitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public int cuttingCounterMax = 3;
}
