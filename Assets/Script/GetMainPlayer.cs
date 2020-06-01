using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMainPlayer : MonoBehaviour
{
    public SpriteRenderer girl, cat, men;
    private SpriteRenderer mySprite;
    private readonly string selectedPlayer = "SelectedPlayer";
    private Vector3 PlayerPosition;
    private Vector3 OffScreen;
    private int getPlayer;

    void Awake()
    {
     	PlayerPosition = men.transform.position;
	OffScreen = cat.transform.position;
	
    }
    void Start()
    {
        

	getPlayer = PlayerPrefs.GetInt(selectedPlayer);
	Debug.Log("scena joc "+ getPlayer);

	switch(getPlayer)
	{
	   case 1:
		Debug.Log("girl");
		men.enabled = false;
		men.transform.position = PlayerPosition;
		cat.transform.position = OffScreen;
		girl.transform.position = OffScreen;
		break;
	   case 2:
		Debug.Log("pisica");
		cat.enabled = false;
		cat.transform.position = PlayerPosition;
		girl.transform.position = OffScreen;
		men.transform.position = OffScreen;
		break;
           case 3:
		girl.enabled = false;
		girl.transform.position = PlayerPosition;
		cat.transform.position = OffScreen;
		men.transform.position = OffScreen;
		break;
	   default: 
		men.enabled = false;
		men.transform.position = PlayerPosition;
		cat.transform.position = OffScreen;
		girl.transform.position = OffScreen;
		break;
	}
	
    }

    
}
