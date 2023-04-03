using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;

	public AudioSource efxSource;
	public AudioSource musicSource;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;


	// Start is called before the first frame update
	void Awake()
    {
        if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
    }

	public void PlaySingle(AudioClip clip)
	{
		efxSource.pitch = Random.Range(lowPitchRange, highPitchRange);
		efxSource.clip = clip;
		efxSource.Play();
	}

    
}
