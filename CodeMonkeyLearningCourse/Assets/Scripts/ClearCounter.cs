using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact() {
        if (kitchenObject == null) {
            SpawnKitchenObject();
        } else {
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }   

    private void SpawnKitchenObject() {
        Instantiate(kitchenObjectSO.prefab, counterTopPoint).GetComponent<KitchenObject>().SetClearCounter(this);
        
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
