using Sandbox;
using System;

[Library( "ent_coin", Title = "Coin", Spawnable = true )]
public partial class Coin : Prop, IUse
{

	private bool rotset = false;
	public override void Spawn()
	{
		base.Spawn();
		SetModel( "models/citizen_props/coin01.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Static, false );
		Rotation = Rotation.From( 0, 0, 90 );
		// PostSpawn();

	}

	private async void PostSpawn()
	{
		await GameTask.Delay( 1 );
		Rotation = Rotation.From( 0, 0, 90 );
	}

	[Event.Tick.Server]
	private void ServerTick()
	{
		LocalRotation = Rotation.From( 0, MathX.RadianToDegree( Time.Now ), 90 );
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	public bool OnUse( Entity user )
	{
		if ( user is Player player )
		{
			player.Health += 10;

			Delete();
		}

		return false;
	}
}
