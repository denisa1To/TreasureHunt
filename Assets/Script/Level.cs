﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	public void Level1(){
		SceneManager.LoadScene(1);
	}


	public void OkButton()
    {
		gameObject.SetActive(false);

    }


}
