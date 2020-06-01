using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerMen : MonoBehaviour
{
		public GameObject PauseMenuBehavior;
		private readonly string scoreResult = "Score";
	    public static bool GameIsPaused = false;
	    public Text ScoreText;
		public Text AudioText;

		public float animSpeed = 5f;				
		public float lookSmoother = 3.0f;			
		public bool useCurves = true;						
		public float useCurvesHeight = 0.5f;		
		public float forwardSpeed = 7.0f;
		public float backwardSpeed = 2.0f;
		public float rotateSpeed = 2.0f;
		public float jumpPower = 3.0f;
		private int score =0;
		private int countKeys = 0;

        private CapsuleCollider col;
		private Rigidbody rb;
		private Vector3 velocity;
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							
		private AnimatorStateInfo currentBaseState;			

		private GameObject cameraObject;	
		
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		void Start ()
		{
			anim = GetComponent<Animator> ();
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
			cameraObject = GameObject.FindWithTag("Camera");
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}



	void Update()
	{
		ScoreText.text = "Score: " + score;

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		anim.SetFloat("Speed", v);
		anim.SetFloat("Direction", h);
		anim.speed = animSpeed;
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		rb.useGravity = true;

		velocity = new Vector3(0, 0, v);
		velocity = transform.TransformDirection(velocity);

		if (Input.GetKeyDown(KeyCode.P))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		if (v > 0.1)
		{
			velocity *= forwardSpeed;
		}
		else if (v < -0.1)
		{
			velocity *= backwardSpeed;
		}

		if (Input.GetButtonDown("Jump"))
		{
			if (currentBaseState.nameHash == locoState)
			{
				if (!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}

		transform.localPosition += velocity * Time.fixedDeltaTime;
		transform.Rotate(0, h * rotateSpeed, 0);
		if (currentBaseState.nameHash == locoState)
		{
			if (useCurves)
			{
				resetCollider();
			}


			else if (currentBaseState.nameHash == jumpState)
			{
				if (!anim.IsInTransition(0))
				{
					if (useCurves)
					{
						float jumpHeight = anim.GetFloat("JumpHeight");
						float gravityControl = anim.GetFloat("GravityControl");
						if (gravityControl > 0)
							rb.useGravity = false;

						Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
						RaycastHit hitInfo = new RaycastHit();
						if (Physics.Raycast(ray, out hitInfo))
						{
							if (hitInfo.distance > useCurvesHeight)
							{
								col.height = orgColHight - jumpHeight + 30;
								float adjCenterY = orgVectColCenter.y + jumpHeight;
								col.center = new Vector3(0, adjCenterY, 0);
							}
							else
							{
								resetCollider();
							}
						}
					}
					anim.SetBool("Jump", false);
				}
			}

			else if (currentBaseState.nameHash == idleState)
			{
				if (Input.GetButtonDown("Jump"))
				{
					anim.SetBool("Rest", true);
				}
			}

			else if (currentBaseState.nameHash == restState)
			{
				if (!anim.IsInTransition(0))
				{
					anim.SetBool("Rest", false);
				}
			}
		}
	}
	void resetCollider()
	{
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}


	void OnTriggerEnter(Collider other)
    	  {
				Debug.Log("Comoara" + other.gameObject.name);

        if (other.gameObject.tag == "Comoara")
        {
                score +=  500;
            PlayerPrefs.SetInt(scoreResult, score);
            loadScene(2);

        }else if(other.gameObject.tag == "Indiciu"){
            Debug.Log("TAG"+ other.gameObject.tag);

            countKeys += 1;
            playSound("key");				
            destroyObject(other);
			setAudioText("In front of you");
			updateScoreKeys();
			
        }
        else if(other.gameObject.tag =="ButterflyKey"){
            Debug.Log("TAG" + other.gameObject.tag);

            countKeys += 1;
            playSound("AfterButterfly");
			setAudioText("After the Butterfly");
			destroyObject(other);
			updateScoreKeys();
		}
		else if(other.gameObject.tag == "FrontKey"){
            Debug.Log("TAG" + other.gameObject.tag);

            countKeys += 1;
            playSound("Front1");
			setAudioText("In front of you");
			destroyObject(other);
			updateScoreKeys();
		}
		else if(other.gameObject.tag =="StangaKey"){
            Debug.Log("TAG" + other.gameObject.tag);

            countKeys += 1;
            playSound("Left");
			setAudioText("In your left");
			destroyObject(other);
			updateScoreKeys();
		}
        
				
		  }

	public void Resume()
	{
		PauseMenuBehavior.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause()
	{
		PauseMenuBehavior.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	private void loadScene(int number){
           		SceneManager.LoadScene(number);
		}
		
		private void playSound(string tag)
		{
			FindObjectOfType<AudioManager>().Play(tag);	
		}
		

		private void destroyObject(Collider objectDestroyed){
			objectDestroyed.gameObject.transform.position = Vector3.one * 9999f;
			//Destroy(objectDestroyed.gameObject, 10);
		}

	private void updateScoreKeys() {
		score -= 100;
		Debug.Log("count Keys" + countKeys);
		if (countKeys == 3) {
			score -= 300;
			loadScene(6);
		}
		PlayerPrefs.SetInt(scoreResult, score);
	}

		private void setAudioText(string text)
    {
		AudioText.text = text;
		AudioText.enabled =true;
		Invoke("InactiveText", 5f);
	}

	private void InactiveText()
    {
		AudioText.enabled =false;
    }
}
