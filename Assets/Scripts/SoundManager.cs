using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] AudioClipRefSO audioClipRef;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnpickedUpKitchenObject += Player_OnpickedUpKitchenObject;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        PlaySounds(audioClipRef.trash, ((TrashCounter)sender).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e)
    {
        PlaySounds(audioClipRef.objectDrop, ((BaseCounter)sender).transform.position);
    }

    private void Player_OnpickedUpKitchenObject(object sender, EventArgs e)
    {
        PlaySounds(audioClipRef.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        PlaySounds(audioClipRef.chop, ((CuttingCounter)sender).transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySounds(audioClipRef.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySounds(audioClipRef.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySounds(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position)
    {
        PlaySound(audioClipRef.footsteps[UnityEngine.Random.Range(0, audioClipRef.footsteps.Length)], position, 0.9f);
    }
}
