using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject soundObjectPrefab;
    public SoundObject SpawnSoundObject()
    {
        GameObject soundObject = Instantiate(soundObjectPrefab);
        Object.DontDestroyOnLoad(soundObject);
        return soundObject.GetComponent<SoundObject>();
    }


}
