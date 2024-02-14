using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DishReceipeSO : ScriptableObject {

    public List<KitchenObjectSO> kitchenObjectSOsList;
    public string receipeName;
}
