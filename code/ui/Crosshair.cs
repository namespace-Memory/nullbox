using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Crosshair : Panel
{
	Label Label;
	int fireCounter;

	Image image;

	public Crosshair()
	{
		var left = Add.Panel("left");
		left.Parent = this;
		left.AddClass("CrosshairLeft");

		var buffer = Add.Panel("buffer");
		buffer.Parent = this;
		buffer.AddClass("Buffer");	

		var right = Add.Panel("right");
		right.Parent = this;
		right.AddClass("CrosshairRight");

	}


	public override void Tick()
	{
		base.Tick();
		
		Style.Top = new Length { Value = fireCounter > 0 ? 32 : 50, Unit = LengthUnit.Percentage };

		if(fireCounter > 0)
		{			
			fireCounter--;
		}

		//Style.Dirty();
	}

	[PanelEvent]
	public void FireEvent()
	{
		//fireCounter += 2;
		
	}
}
