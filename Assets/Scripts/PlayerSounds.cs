using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer = 0;
    private float footstepTime = 0.1f;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.IsWalking())
        {
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= footstepTime)
            {
                footstepTimer = 0;
                PlayFootstepSound();
            }
        }
    }

    private void PlayFootstepSound()
    {
        SoundManager.Instance.PlayFootstepSound(transform.position);
    }
}
