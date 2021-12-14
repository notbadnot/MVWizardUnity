using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WizardLegs : MonoBehaviour
{
    [SerializeField] private AudioClip stepClip;

    private SoundManager soundManager;

    [Inject]
    private void Construct(SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

    public void PlayFootStepSound()
    {
        soundManager.SpawnSoundObject().Play(stepClip, gameObject.transform.position);
    }
}
