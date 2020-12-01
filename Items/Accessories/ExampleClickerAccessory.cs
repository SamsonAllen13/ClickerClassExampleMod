using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ClickerClassExampleMod.Items.Accessories
{
	//Sample code for a clicker related item
	public class ExampleClickerAccessory : ModItem
	{
		//Optional, if you want this item to exist only when Clicker Class is enabled
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
				"Gain up to 15% clicker damage based on your amount of clicks within a second" + "\n" +
				"Every 15 clicks releases a burst of damaging chocolate" + "\n" +
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
			//If you use VS, just mouseover the method name to see what it does
			ClickerCompat.SetDamageAdd(player, 0.2f);
			ClickerCompat.SetClickerBonusAdd(player, 1);

			//Enables the special effect of the "Glass Of Milk" accessory
			ClickerCompat.SetAccessory(player, "GlassOfMilk");

			//Enabled the click effect given by Chocolate Chip
			//You can use Clicker Classes base effects (you can find them in the source code), or your own ones
			ClickerCompat.EnableClickEffect(player, "ClickerClass:ChocolateChip");

			//How to check if an effect is enabled for the player
			bool hasChocolateChip = ClickerCompat.HasClickEffect(player, "ClickerClass:ChocolateChip");

			//Makes the radius go in a wave motion from 0 to 100 additional pixels
			float fluct = 1f + (float)Math.Sin(2 * Math.PI * (Main.GameUpdateCount % 60) / 60f);
			ClickerCompat.SetClickerRadiusAdd(player, fluct / 2);
		}
	}
}
