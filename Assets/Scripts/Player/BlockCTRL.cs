﻿using UnityEngine;
using System.Collections;

// enables us to block ctrl of player in different ways
public class BlockCTRL : MonoBehaviour
{
	private FirstPersonCharacter fpcharacter;
	private SimpleMouseRotator mouseRotaterX;
	private SimpleMouseRotator mouseRotaterY;
	private InteractionControls interactionCtrl;
	private Rigidbody rigid;
	
	private Vector2 rotationrangeX;
	private Vector2 rotationrangeY;
	
	void Awake()
	{
		fpcharacter = GetComponent<FirstPersonCharacter>();
		mouseRotaterX = GetComponent<SimpleMouseRotator>();
		mouseRotaterY = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SimpleMouseRotator>();
		interactionCtrl = GetComponent<InteractionControls>();
		
		rotationrangeX = mouseRotaterX.rotationRange;
		rotationrangeY = mouseRotaterY.rotationRange;
		
		rigid = rigidbody;
	}
	
	// 	do you want to block the movement? (walking, jumping)
	public void BlockMovement(bool block)
	{
		rigid.velocity = Vector3.zero;
		fpcharacter.enabled = !block;
	}
	
	//	block or unblock x and y axis mouse rotator scripts
	public void BlockLookingaround(bool xAxis, bool yAxis)
	{
		mouseRotaterX.enabled = !xAxis;
		mouseRotaterY.enabled = !yAxis;
	}
	
	// 	restrict mouse rotator scripts
	public void RestrictRotation(bool xAxis, float rotationRange)
	{
		// restrict xAxis
		if (xAxis) {
			mouseRotaterX.rotationRange.Set(0, rotationRange);
		}
		// restrict yAxis
		else {
			mouseRotaterX.rotationRange.Set(rotationRange, 0);
		}
	}
	
	// reset the rotation ranges to default
	public void ResetRotation(bool xAxis, bool yAxis)
	{
		if (xAxis)
			mouseRotaterX.rotationRange = rotationrangeX;
		if (yAxis)
			mouseRotaterY.rotationRange = rotationrangeY;
	}
	
	// block interaction ctrls
	public void BlockInteractionCtrl(bool block)
	{
		interactionCtrl.enableCtrls = !block;
	}
	
	public void BlockForCutscene()
	{
		BlockInteractionCtrl(true);
		BlockLookingaround(true,true);
		BlockMovement(true);
	}
	
	public void UnblockForCutscene()
	{
		BlockInteractionCtrl(false);
		BlockLookingaround(false,false);
		BlockMovement(false);
	}
}
