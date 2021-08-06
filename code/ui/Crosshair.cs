using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Crosshair : Panel
{
	Label Label;
	int fireCounter;


	public Crosshair()
	{
		Label = Add.Label( "^", "value" );

	}


	public override void Tick()
	{
		base.Tick();
		
		Style.Top = new Length { Value = fireCounter > 0 ? 32 : 50, Unit = LengthUnit.Percentage };

		if(fireCounter > 0)
		{
			
			fireCounter--;
		}
		Style.Dirty();
	}

	[PanelEvent]
	public void FireEvent()
	{
		fireCounter += 2;
		
	}
}
