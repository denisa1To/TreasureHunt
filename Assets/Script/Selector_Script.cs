using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector_Script : MonoBehaviour
{
    public GameObject Cat; 
    public GameObject Girl;
    public GameObject Men;
    private Vector3 PlayerPosition;	
    private Vector3 OffScreen;
    private int PlayerInt = 1;
    private SpriteRenderer GirlRender, CatRender, MenRender;
    private readonly string selectedPlayer = "SelectedPlayer";

    private void Awake()
    {
	PlayerPosition = Men.transform.position;
	OffScreen = Cat.transform.position;
	CatRender = Cat.GetComponent<SpriteRenderer>();
	GirlRender = Cat.GetComponent<SpriteRenderer>();
	MenRender = Men.GetComponent<SpriteRenderer>();
	
    }	


   public void NextPlayer()
   {
	switch(PlayerInt)
	{
		case 1:				
			MenRender.enabled = false;
			Men.transform.position = OffScreen;
			Cat.transform.position = PlayerPosition;
			CatRender.enabled = true;
			PlayerInt++;			
			break;
		case 2:
			
			CatRender.enabled = false;
			Cat.transform.position = OffScreen;
			Girl.transform.position = PlayerPosition;
			GirlRender.enabled = true;
			PlayerInt++;			
			break;
		case 3:
			//PlayerPrefs.SetInt(selectedPlayer, 3);	
			GirlRender.enabled = false;
			Girl.transform.position = OffScreen;
			Men.transform.position = PlayerPosition;
			MenRender.enabled = true;
			PlayerInt++;			
			break;
		default:
			ResetInt();
			break;
	}
	PlayerPrefs.SetInt(selectedPlayer, PlayerInt);
	Debug.Log("Player SELECTED " + PlayerPrefs.GetInt(selectedPlayer));
    } 

   public void PreviousPlayer()
   {
	switch(PlayerInt)
	{
		case 1:
			PlayerPrefs.SetInt(selectedPlayer, 3);
			GirlRender.enabled = false;
			Girl.transform.position = OffScreen;
			Men.transform.position = PlayerPosition;
			MenRender.enabled = true;
			ResetInt();			
			break;
		case 2:
			PlayerPrefs.SetInt(selectedPlayer, 2);
			CatRender.enabled = false;
			Cat.transform.position = OffScreen;
			Girl.transform.position = PlayerPosition;
			GirlRender.enabled = true;
			PlayerInt--;			
			break;
		case 3:
			PlayerPrefs.SetInt(selectedPlayer, 1);
			MenRender.enabled = false;
			Men.transform.position = OffScreen;
			Cat.transform.position = PlayerPosition;
			CatRender.enabled = true;
			PlayerInt--;			
			break;
		default:
			ResetInt();
			break;
	}
	
    } 

    private void ResetInt()
    {
     	if(PlayerInt >=3)
	{
		PlayerInt = 1;

	}else{
		PlayerInt = 3;
	}
     }

     public void ChangeScene()
     {
        SceneManager.LoadScene(0);
     }
     
}
