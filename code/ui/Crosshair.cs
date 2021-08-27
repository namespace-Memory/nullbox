using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Crosshair : Panel
{
	Label Label;
	int fireCounter;
	Image image;
	Panel buffer;

	public Crosshair()
	{
		AddClass( "Crosshair" );

	}

	public override void Tick()
	{
		base.Tick();

		SetClass( "fire", fireCounter > 0 );

		if ( fireCounter > 0 )
		{
			fireCounter--;
		}
	}

	[PanelEvent( "fire" )]
	public void FireEvent()
	{
		Host.AssertClient();

		fireCounter = 16;
	}
}
