namespace Sandbox.Tools
{
	[Library( "tool_duplicator", Title = "Duplicator", Description = "Duplicate things", Group = "fun" )]
	public class Duplicator : BaseTool
	{

		public ModelEntity EntDupeBuffer { get; private set; }
		public ModelEntity GhostEntity { get; private set; }

		public Rotation PlaceRotation { get; private set; }

		private float yawTotal = 0;

		public override void Activate()
		{
			base.Activate();
			GhostEntity = Sandbox.Entity.Create<ModelEntity>();
			GhostEntity.EnableAllCollisions = false;
			if ( EntDupeBuffer.IsValid() )
			{
				GhostEntity.SetModel( EntDupeBuffer.GetModel() );
				GhostEntity.Position = EntDupeBuffer.Position;
			}

		}

		public override void Deactivate()
		{
			base.Deactivate();

			GhostEntity?.Delete();
		}



		public override void Simulate()
		{

			if ( !Host.IsServer ) return;
			using ( Prediction.Off() )
			{
				if ( Input.Pressed( InputButton.Attack1 ) && EntDupeBuffer != null )
				{
					var tr = Trace.Ray( Owner.EyePos, Owner.EyePos + Owner.EyeRot.Forward * MaxTraceDistance )
					.Ignore( Owner )
					.WorldAndEntities().Run();

					if ( tr.Hit && EntDupeBuffer != null )
					{
						var duped = Entity.Create<Prop>();
						duped.SetModel( (EntDupeBuffer as Prop).GetModel() );
						duped.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
						duped.EnableAllCollisions = true;
						duped.EnableDrawing = true;
						duped.Position = tr.EndPos - Vector3.Up * duped.CollisionBounds.Mins.z;
						duped.Rotation = PlaceRotation;
					}

				}

				if ( Input.Pressed( InputButton.Attack2 ) )
				{
					var tr = Trace.Ray( Owner.EyePos, Owner.EyePos + Owner.EyeRot.Forward * MaxTraceDistance ).Ignore( Owner ).EntitiesOnly().Run();
					if ( tr.Hit )
					{
						EntDupeBuffer = tr.Entity as ModelEntity;
						GhostEntity.SetModel( EntDupeBuffer.GetModel() );
						GhostEntity.Position = tr.Entity.Position;
						var freezeEffect = Particles.Create( "particles/physgun_freeze.vpcf" );
						freezeEffect.SetPosition( 0, EntDupeBuffer.Transform.PointToWorld( EntDupeBuffer.Position ) );
					}

				}

				if ( Input.Down( InputButton.Reload ) && EntDupeBuffer != null )
				{

					PlaceRotation = Rotation.FromYaw( PlaceRotation.Yaw() + 15 );
				}

				if ( EntDupeBuffer != null )
				{
					var tr = Trace.Ray( Owner.EyePos, Owner.EyePos + Owner.EyeRot.Forward * MaxTraceDistance ).Ignore( Owner ).Run();
					GhostEntity.Position = Vector3.Lerp( GhostEntity.Position, tr.EndPos - Vector3.Up * EntDupeBuffer.CollisionBounds.Mins.z, 8.95f * Time.Delta );
					GhostEntity.Rotation = Rotation.Lerp( GhostEntity.Rotation, PlaceRotation, 6.95f * Time.Delta );
				}
			}

		}

	}
}
