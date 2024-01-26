using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StoveReceipeSO : ScriptableObject {

    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float cookTimeMax;
    public StoveCounter.State onStoveState;
}
