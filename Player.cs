using Godot;
using System;

public class Player : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	[Export]
	public int Speed = 14;

	[Export]
	public int FallAcceleration = 75;

	private Vector3 _velocity = Vector3.Zero;

	public float mouse_sensitivity = 1;
	


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseMode.Captured);
		
		
	}
	public override void  _UnhandledInput(InputEvent InMotion)
	{
		
		InputEventMouseMotion motion = InMotion as InputEventMouseMotion;
		if(motion != null)
		{
			RotateY(Mathf.Deg2Rad(-motion.Relative.x * mouse_sensitivity));
			RotateX(Mathf.Deg2Rad(-motion.Relative.x * mouse_sensitivity));
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	 // We create a local variable to store the input direction.
	var direction = Vector3.Zero;

	// We check for each move input and update the direction accordingly
	if (Input.IsActionPressed("right"))
	{
		direction.x += 1f;
	}
	
	if (Input.IsActionPressed("left"))
	{
		direction.x -= 1f;
	}
	
	if (Input.IsActionPressed("backward"))
	{
		// Notice how we are working with the vector's x and z axes.
		// In 3D, the XZ plane is the ground plane.
		direction.z += 1f;
	}

	if (Input.IsActionPressed("forward"))
	{
		direction.z -= 1f;
	}

	if (direction != Vector3.Zero)
	{
		direction = direction.Normalized();
	   
	}

	// Ground velocity
	_velocity.x = direction.x * Speed;
	_velocity.z = direction.z * Speed;
	// Vertical velocity
	_velocity.y -= FallAcceleration * delta;
	// Moving the character
	_velocity = MoveAndSlide(_velocity, Vector3.Up);

	}
}
