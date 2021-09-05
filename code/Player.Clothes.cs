using Sandbox;

partial class SandboxPlayer
{

	public class PlayerOutfit
	{
		/// <summary>
		/// Indexed from hat to shoes.
		/// </summary>
		public ModelEntity[] Clothes = new ModelEntity[4];
	}

	public PlayerOutfit Outfit { get; private set; }

	private bool dressed = false;


	public static readonly string[] Trousers =
	{
		"models/citizen_clothes/trousers/trousers.jeans.vmdl",
		"models/citizen_clothes/trousers/trousers.lab.vmdl",
		"models/citizen_clothes/trousers/trousers.police.vmdl",
		"models/citizen_clothes/trousers/trousers.smart.vmdl",
		"models/citizen_clothes/trousers/trousers.smarttan.vmdl",
		"models/citizen_clothes/trousers/trousers_tracksuitblue.vmdl",
		"models/citizen_clothes/trousers/trousers_tracksuit.vmdl",
		"models/citizen_clothes/shoes/shorts.cargo.vmdl",
	};

	public static readonly string[] Jackets =
	{
		"models/citizen_clothes/jacket/labcoat.vmdl",
		"models/citizen_clothes/jacket/jacket.red.vmdl",
		"models/citizen_clothes/jacket/jacket.tuxedo.vmdl",
		"models/citizen_clothes/jacket/jacket_heavy.vmdl",
	};

	public static readonly string[] Hats =
	{
		"models/citizen_clothes/hat/hat_hardhat.vmdl",
		"models/citizen_clothes/hat/hat_woolly.vmdl",
		"models/citizen_clothes/hat/hat_securityhelmet.vmdl",
		"models/citizen_clothes/hair/hair_malestyle02.vmdl",
		"models/citizen_clothes/hair/hair_femalebun.black.vmdl",
		"models/citizen_clothes/hat/hat_beret.red.vmdl",
		"models/citizen_clothes/hat/hat.tophat.vmdl",
		"models/citizen_clothes/hat/hat_beret.black.vmdl",
		"models/citizen_clothes/hat/hat_cap.vmdl",
		"models/citizen_clothes/hat/hat_leathercap.vmdl",
		"models/citizen_clothes/hat/hat_leathercapnobadge.vmdl",
		"models/citizen_clothes/hat/hat_securityhelmetnostrap.vmdl",
		"models/citizen_clothes/hat/hat_service.vmdl",
		"models/citizen_clothes/hat/hat_uniform.police.vmdl",
		"models/citizen_clothes/hat/hat_woollybobble.vmdl",
	};

	public static readonly string[] Shoes =
	{
		"models/citizen_clothes/shoes/trainers.vmdl",
		"models/citizen_clothes/shoes/shoes.workboots.vmdl"
	};

	public void Strip()
	{
		if ( dressed )
		{
			dressed = false;
		}

		if ( Outfit == null ) return;
		for ( int i = 0; i < Outfit.Clothes.Length; i++ )
		{
			Outfit.Clothes[i].Delete();
		}
		SetBodyGroup( "Chest", 1 );
		SetBodyGroup( "Legs", 1 );
		SetBodyGroup( "Feet", 1 );

	}
	public void Dress( PlayerOutfit newOutfit )
	{
		if ( dressed || IsClient ) return;
		dressed = true;

		if ( newOutfit != null )
		{
			Outfit = newOutfit;
			for ( int i = 0; i < newOutfit.Clothes.Length; i++ )
			{
				Outfit.Clothes[i] = newOutfit.Clothes[i];
			}
			SetBodyGroup( "Chest", 1 );
			SetBodyGroup( "Legs", 1 );
			SetBodyGroup( "Feet", 1 );

		}
		else
		{
			Outfit = new PlayerOutfit();

			//Hat
			Outfit.Clothes[0] = new ModelEntity();
			Outfit.Clothes[0].SetModel( Rand.FromArray<string>( Hats, null ) );
			Outfit.Clothes[0].SetParent( this, true );
			Outfit.Clothes[0].EnableShadowInFirstPerson = true;
			Outfit.Clothes[0].EnableHideInFirstPerson = true;

			//Jacket
			Outfit.Clothes[1] = new ModelEntity();
			Outfit.Clothes[1].SetModel( Rand.FromArray<string>( Jackets, null ) );
			Outfit.Clothes[1].SetParent( this, true );
			Outfit.Clothes[1].EnableShadowInFirstPerson = true;
			Outfit.Clothes[1].EnableHideInFirstPerson = true;
			var propInfo = Outfit.Clothes[1].GetModel().GetPropData();
			if ( propInfo.ParentBodyGroupName != null )
			{
				SetBodyGroup( propInfo.ParentBodyGroupName, propInfo.ParentBodyGroupValue );
			}
			else
			{
				SetBodyGroup( "Chest", 0 );
			}

			//Pants
			Outfit.Clothes[2] = new ModelEntity();
			Outfit.Clothes[2].SetModel( Rand.FromArray<string>( Trousers, null ) );
			Outfit.Clothes[2].SetParent( this, true );
			Outfit.Clothes[2].EnableShadowInFirstPerson = true;
			Outfit.Clothes[2].EnableHideInFirstPerson = true;
			SetBodyGroup( "Legs", 1 );

			Outfit.Clothes[3] = new ModelEntity();
			Outfit.Clothes[3].SetModel( Rand.FromArray<string>( Shoes, null ) );
			Outfit.Clothes[3].SetParent( this, true );
			Outfit.Clothes[3].EnableShadowInFirstPerson = true;
			Outfit.Clothes[3].EnableHideInFirstPerson = true;
			SetBodyGroup( "Feet", 1 );
		}



	}
}
