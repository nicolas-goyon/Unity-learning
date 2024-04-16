using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact() {
        Transform kitchenObjectSOTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectSOTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectSOTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName + " has been placed on the counter");
    }   
}
