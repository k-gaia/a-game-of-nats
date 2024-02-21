using UnityEngine.Audio;
using UnityEngine;
using System;

public class audioManagerScript : MonoBehaviour
{

    public Sound[] sounds;

    public static audioManagerScript Instance;

    public static float themeVolume;


    private void Awake()
    {

        if (Instance == null)
        {

            Instance = this;

        }
        else
        {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);

        foreach ( Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.pitch = s.pitch;

            s.source.loop = s.loop;

        }


    }

    public void Play (string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {

            Debug.Log("Sound: " + name + " wasn't found.");

        }

        if (s.source.loop == true)
        {
            s.source.Play();
        }

        if (s.source.loop == false)
        {
            s.source.PlayOneShot(s.source.clip);
        }

    }

    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {

            Debug.Log("Sound: " + name + " wasn't found.");

        }

        s.source.Stop();

    }



    // Start is called before the first frame update
    void Start()
    {

        Play("Theme");

    }

    public void setVolume (float volume)
    {

        Sound s = Array.Find(sounds, sound => sound.name == "Theme");

        s.source.volume = volume;


    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
