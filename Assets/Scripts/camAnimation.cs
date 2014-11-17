using UnityEngine;
using System.Collections;

public class camAnimation : MonoBehaviour
{
	public CharacterController playerController;
	
	private Animation _anim; //Empty GameObject's animation component
	private bool isMoving;
	private bool left;
	private bool right;
	
	void Awake()
	{
		_anim = GetComponent<Animation>();
	}
	
	void CameraAnimations()
	{
		if (playerController.isGrounded == true){
			if (isMoving == true){
				if (left == true){
					if(!_anim.isPlaying){//Waits until no animation is playing to play the next
						_anim.Play("walkLeft");
						left = false;
						right = true;
					}
				}
				if (right == true){
					if (!_anim.isPlaying){
						_anim.Play("walkRight");
						right = false;
						left = true;
					}
				}
			}                  
		}
	}
	
	void Start () { //First step in a new scene/life/etc. will be "walkLeft"
		left = true;
		right = false;
	}
	
	
	void Update () {
		//Keyboard input to determine if player is moving
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		if(inputX  != 0 || inputY != 0)
			isMoving = true;       
		else if(inputX == 0 && inputY == 0)
			isMoving = false;
		
		CameraAnimations();
	}
}