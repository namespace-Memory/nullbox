namespace Sandbox.Tools
{
	[Library( "tool_ragdollgun", Title = "Ragdoll Shooter", Description = "Shoot terries", Group = "fun" )]
	public class RagdollShooter : BaseTool
	{
		TimeSince timeSinceShoot;

		public override void Simulate()
		{
			if ( Host.IsServer )
			{
				if ( Input.Pressed( InputButton.Attack1 ) )
				{
					ShootBox();
				}

				if ( Input.Down( InputButton.Attack2 ) && timeSinceShoot > 0.05f )
				{
					timeSinceShoot = 0;
					ShootBox();
				}
			}
		}

		void ShootBox()
		{
			var ent = new AnimEntity
			{
				Position = Owner.EyePos + Owner.EyeRot.Forward * 100,
				Rotation = Owner.EyeRot
			};


			ent.SetModel( "models/citizen/citizen.vmdl" );
			ent.SetAnimBool( "b_swim", true );
			ent.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
			ent.PhysicsGroup?.ApplyAngularImpulse( Vector3.Random * 4000, true );
			ent.Velocity = Owner.EyeRot.Forward * 3000;
		}
	}
}
