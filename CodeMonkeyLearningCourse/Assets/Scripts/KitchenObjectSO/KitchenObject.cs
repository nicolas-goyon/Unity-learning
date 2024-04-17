using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newParent) {
        if (newParent.HasKitchenObject()) {
            Debug.LogError("Clear Counter already has a kitchen object");
        }


        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = newParent;

        this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = newParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }


    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        KitchenObject result = Instantiate(kitchenObjectSO.prefab).GetComponent<KitchenObject>();
        result.SetKitchenObjectParent(parent);
        return result;
    }

    public bool TryGetPlate(out PlateKitchenObject plate) {
        plate = null;
        
        if (this is not PlateKitchenObject) {
            return false;
        }

        plate = this as PlateKitchenObject;
        return true;
    }

}
