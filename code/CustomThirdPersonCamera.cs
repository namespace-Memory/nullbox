using Sandbox;

public class CustomThirdPersonCamera : Camera
{

	public override void Activated()
	{
		base.Activated();
	}
	public override void Update()
	{
		var pawn = Local.Pawn as AnimEntity;
		var client = Local.Client;

		if ( pawn == null )
			return;

		Pos = pawn.Position + (pawn.EyeRot.Backward * 130f + Vector3.Up * 80f);
		Rot = Rotation.LookAt( pawn.EyeRot.Forward, pawn.EyeRot.Up );

		FieldOfView = 90;
		Viewer = null;
	}

}
