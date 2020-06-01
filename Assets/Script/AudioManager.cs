using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
     public AudioSource[] sounds;

    void Awake()
    {

    }

   public void Play(string name)
    {
        foreach (AudioSource s in sounds)
        {
		if(s.name == name){
			s.Play();
		}
		
	}
    }
}
