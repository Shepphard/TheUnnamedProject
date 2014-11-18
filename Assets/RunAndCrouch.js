 var walkSpeed: float = 7; // regular speed
 var crchSpeed: float = 3; // crouching speed
 var runSpeed: float = 20; // run speed
 
 private var chMotor: CharacterMotor;
 private var tr: Transform;
 private var dist: float; // distance to ground
 
 function Start()
 {
     chMotor = GetComponent(CharacterMotor);
     tr = transform;
     var ch:CharacterController = GetComponent(CharacterController);
     dist = ch.height/2; // calculate distance to ground
 }
 
 function Update()
 {
     var vScale = 1.0;
     var speed = walkSpeed;
     
     if (chMotor.grounded && Input.GetKey("left shift") || Input.GetKey("right shift")){
         speed = runSpeed;
     }
     if (Input.GetKey("c")){ // press C to crouch
         vScale = 0.5;
         speed = crchSpeed; // slow down when crouching
     }
     chMotor.movement.maxForwardSpeed = speed; // set max speed
     var ultScale = tr.localScale.y; // crouch/stand up smoothly 
     tr.localScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5*Time.deltaTime);
     tr.position.y += dist * (tr.localScale.y-ultScale); // fix vertical position
 }
     //SOURCE http://answers.unity3d.com/questions/164638/how-to-make-the-fps-character-controller-run-and-c.html