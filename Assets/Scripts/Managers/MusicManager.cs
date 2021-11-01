using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public List<AudioClip> clips;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else { Destroy(this); return; }
        DontDestroyOnLoad(this);

        StartCoroutine(AudioPlayer());
    }


    IEnumerator AudioPlayer()
    {
        while (isActiveAndEnabled)
        {
            if (clips.Count == 0) yield break;
            AudioClip clip = clips[Random.Range(0, clips.Count)];

            source.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length);
        }
    }
}
