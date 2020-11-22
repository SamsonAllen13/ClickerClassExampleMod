using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ClickerClassExampleMod.Items.Accessories
{
	//Sample code for a clicker related item
	public class ExampleClickerAccessory : ModItem
	{
		//Optional, if you only want this item to exist only when Clicker Class is enabled
		public override bool Autoload(ref string name)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as a clicker related item
			ClickerCompat.RegisterClickerItem(this);

			DisplayName.SetDefault("Example Clicker Accessory");
			Tooltip.SetDefault("'Big Red Button'" +"\n" +
				"20% increased clicker damage" +"\n" +
				"Reduces the amount of clicks required for a click effect by 1" + "\n" +
				"Makes the radius pulsate up to 50% of the default radius");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.value = 100000;
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//Use these methods to adjust clicker class related variables (treat them like player.meleeDamage etc.)
			//Only a small sample here
			ClickerCompat.SetDamageAdd(player, 0.2f);
			float fluct = (float)Math.Sin(2 * Math.PI * Main.GameUpdateCount % 60 / 60f);
			ClickerCompat.SetClickerRadiusAdd(player, fluct);
			ClickerCompat.SetClickerBonusAdd(player, 1);
		}
	}
}
