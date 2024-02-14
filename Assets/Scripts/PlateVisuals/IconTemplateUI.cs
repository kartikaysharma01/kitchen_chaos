using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplateUI : MonoBehaviour {
    [SerializeField] private Image iconSprite;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO) {
        iconSprite.sprite = kitchenObjectSO.sprite;
    }
}
