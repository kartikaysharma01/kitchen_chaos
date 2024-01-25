using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar {

    [SerializeField] private StoveReceipeSO[] stoveReceipeSOArray;
    private float stoveTimer;

    public event EventHandler<OnStateChnageArgs> OnStateChnage;
    public event EventHandler<IProgressBar.OnProgressChangeArgs> OnProgressChange;

    public class OnStateChnageArgs: EventArgs{
        public State state;
    }
    public enum State {
        Idle,
        Frying,
        Fried,
        Burned

    }

    private StoveReceipeSO stoveReceipeSO;
    private State current_state;

    private void Update() {
        stoveTimer += Time.deltaTime;
        
        if (HasKitchenObject()) {
            switch (current_state) {
                case State.Idle:
                    break;
                case State.Frying:
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                        progressNormalised = stoveTimer/stoveReceipeSO.cookTimeMax
                    }); 
                    if (stoveTimer > stoveReceipeSO.cookTimeMax) {
                        // destroy unfried kitchen object
                        GetKitchenObject().DestroySelf();

                        // spwan fried kitchen object and assign it to stoveReceipeSO
                        KitchenObject.SpawnKitchenObject(stoveReceipeSO.output, this);
                        stoveTimer = 0;
                        current_state = State.Fried;
                        OnStateChnage?.Invoke(this, new OnStateChnageArgs{state = current_state});
                        stoveReceipeSO = GetStoveReceipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                        progressNormalised = stoveTimer/stoveReceipeSO.cookTimeMax
                    }); 
                    if (stoveTimer > stoveReceipeSO.cookTimeMax) {
                        // destroy fried kitchen object
                        GetKitchenObject().DestroySelf();
    
                        // spwan burnt kitchen object 
                        KitchenObject.SpawnKitchenObject(stoveReceipeSO.output, this);
                        stoveTimer = 0;
                        current_state = State.Burned;
                        OnStateChnage?.Invoke(this, new OnStateChnageArgs{state = current_state});
                        OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                            progressNormalised = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    // on interact, let player pickup or drop valid kitchen objects
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // counter does not have kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // what player is carrying can be cooked, let him drop it
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    stoveReceipeSO = GetStoveReceipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    stoveTimer = 0;
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                        progressNormalised = 0
                    }); 
                    current_state = State.Frying;
                    OnStateChnage?.Invoke(this, new OnStateChnageArgs{state = current_state});
                }
            } else {
                // player is not carrying anything, do nothing
            }
        } else {
            // counter has kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something, do nothing
            } else {
                // player is not carrying anything, let him pickup counter object, and set stove to idle
                GetKitchenObject().SetKitchenObjectParent(player);
                current_state = State.Idle;
                OnStateChnage?.Invoke(this, new OnStateChnageArgs{state = current_state});
                OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                    progressNormalised = 0
                }); 
            }
        }
    }

    private bool HasReceipeWithInput(KitchenObjectSO kitchenObjectSO) {
        StoveReceipeSO stoveReceipeSO = GetStoveReceipeSOWithInput(kitchenObjectSO);
        if (stoveReceipeSO != null) {
            return true;
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        StoveReceipeSO stoveReceipeSO = GetStoveReceipeSOWithInput(inputKitchenObjectSO);
        if (stoveReceipeSO != null) {
            return stoveReceipeSO.output;
        }
        return null;
    }

    private StoveReceipeSO GetStoveReceipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(StoveReceipeSO stoveReceipeSO in stoveReceipeSOArray) {
            if (stoveReceipeSO.input == inputKitchenObjectSO) {
                return stoveReceipeSO;
            }
        }
        return null;
    }

}
