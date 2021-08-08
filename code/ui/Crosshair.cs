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
		var left = Add.Panel("left");
		left.Parent = this;
		left.AddClass("CrosshairLeft");

		buffer = Add.Panel("buffer");
		buffer.Parent = this;
		buffer.AddClass("Buffer");	

		var right = Add.Panel("right");
		right.Parent = this;
		right.AddClass("CrosshairRight");


	}

	public override void Tick()
	{
		base.Tick();
		var active = fireCounter > 0;
		buffer.SetClass("Buffer", !active);
		buffer.SetClass("Buffer1", active);

		if(fireCounter > 0)
		{			
			fireCounter--;
		}

		//Style.Dirty();
	}

	[PanelEvent]
	public void FireEvent()
	{
		Host.AssertClient();
		if(fireCounter + 2 < 8)
		{
			fireCounter += 2;
		}

		
	}
}
