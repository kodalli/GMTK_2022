using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource slotDemon;
    [SerializeField] private AudioSource bossRoom;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip) {
        source.PlayOneShot(clip);
    }

    public void PlayButtonClickSound() {
        source.PlayOneShot(buttonClickSound);
    }

    public void PlayDeathSound() {
        source.PlayOneShot(deathSound);
    }

    public void PlaySlotDemon() {
        StopBossMusic();
        if (slotDemon.isPlaying) {
            slotDemon.UnPause();
            return;
        }
        slotDemon.Play();
    }

    public void StopSlotDemon() {
        slotDemon.Pause();
        // slotDemon.Stop();
    }

    public void PlayBossMusic() {
        StopSlotDemon();
        if (bossRoom.isPlaying) return;
        bossRoom.Play();
    }

    public void StopBossMusic() {
        bossRoom.Stop();
    }
}