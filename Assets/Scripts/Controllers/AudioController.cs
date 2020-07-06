using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    private AudioSource _Audio;
    private float _Level;


    private void Awake() {
        _Audio = this.gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetAudioLevel(float level) {
        _Level = level;
        _Audio.volume = level;
    }

    public float GetAudioLevel() {
        return _Level;
    }
}
