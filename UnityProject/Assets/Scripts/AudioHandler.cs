using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _clips;
    [SerializeField]
    private float soundPlayCooldownTime;

    private AudioSource _audioSource;
    private float soundPlayCooldown;
    // Start is called before the first frame update
    void Start(){
        _audioSource = GetComponent<AudioSource>();

        soundPlayCooldown = 0f;
    }

    // Update is called once per frame
    void Update(){
        if (soundPlayCooldown > 0){
            soundPlayCooldown -= Time.deltaTime;
        }
    }

    public void PlaySound(int idx){
        if (soundPlayCooldown <= 0){
            soundPlayCooldown = soundPlayCooldownTime;
            _audioSource.Stop();
            _audioSource.clip = _clips[idx];
            _audioSource.Play();
        }
    }
}
