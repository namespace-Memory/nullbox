
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class NetworkInfo : Panel
{
	Label label;
	public NetworkInfo()
	{
		label = Add.Label( "", "value" );
		label.SetText( Global.IsListenServer ? "Server" : "Client" );

	}


}
