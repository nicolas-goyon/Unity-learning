using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite iconSprite;
    public string objectName;


    //public override bool Equals(object other) {
    //    if (other == null) {
    //        return false;
    //    }

    //    if (other.GetType() != typeof(KitchenObjectSO)) {
    //        return false;
    //    }

    //    KitchenObjectSO otherKitchenObjectSO = (KitchenObjectSO) other;

    //    return otherKitchenObjectSO.objectName == objectName;
    //}

    //public override int GetHashCode() {
    //    return objectName.GetHashCode();
    //}
}
