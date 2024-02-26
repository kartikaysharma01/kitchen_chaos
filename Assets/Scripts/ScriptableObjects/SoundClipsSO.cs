using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SoundClipsSO : ScriptableObject {
    public List<AudioClip> choppingSounds;
    public List<AudioClip> deliveryFailSounds;
    public List<AudioClip> deliverySuccessSounds;
    public List<AudioClip> footStepsSounds;
    public List<AudioClip> dropingSounds;
    public List<AudioClip> pickupSounds;
    public AudioClip sizzleSound;
    public List<AudioClip> trashSounds;
    public List<AudioClip> warningSounds;
}
