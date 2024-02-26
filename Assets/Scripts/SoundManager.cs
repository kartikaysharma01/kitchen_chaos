using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private SoundClipsSO soundClipsSO;

    private void Start() {
        DeliveryManager.Instance.OnSuccessfulDelivery += DeliveryManager_OnSuccessfulDelivery;
        DeliveryManager.Instance.OnFailedDelivery += DeliveryManager_OnDishDelivered;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnKOPickup += Player_OnKOPickup;
        BaseCounter.OnObjectDropOnCounter += BaseCounter_OnObjectDropOnCounter;
        TrashCounter.OnTrashBinOpen += TrashCounter_OnTrashBinOpen;
    }

    private void TrashCounter_OnTrashBinOpen(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundClipsSO.trashSounds, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectDropOnCounter(object sender, EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundClipsSO.dropingSounds, baseCounter.transform.position);
    }

    private void Player_OnKOPickup(object sender, EventArgs e) {
        PlaySound(soundClipsSO.pickupSounds, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundClipsSO.choppingSounds, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnSuccessfulDelivery(object sender, EventArgs e) {
        PlaySound(soundClipsSO.deliverySuccessSounds, DeliveryManager.Instance.transform.position);
    }

    private void DeliveryManager_OnDishDelivered(object sender, EventArgs e) {
        PlaySound(soundClipsSO.deliveryFailSounds, DeliveryManager.Instance.transform.position);
    }

    private void PlaySound(List<AudioClip> audioClipList, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClipList[UnityEngine.Random.Range(0, audioClipList.Count)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
