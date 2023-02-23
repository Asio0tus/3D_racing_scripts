using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float pichModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;

    [SerializeField] private float basePich = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private AudioSource engineAudioSource;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSource.pitch = basePich + pichModifier * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifier);
        engineAudioSource.volume = baseVolume + volumeModifier * (car.EngineRpm / car.EngineMaxRpm);
    }
}
