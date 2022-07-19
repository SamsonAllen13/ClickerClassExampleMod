using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Accessories
{
	//Sample code for a clicker related item
	public class ExampleClickerAccessory : ModItem
	{
		//Optional, if you want this item to exist only when Clicker Class is enabled
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as a clicker related item
			ClickerCompat.RegisterClickerItem(this);

			DisplayName.SetDefault("Example Clicker Accessory");
			Tooltip.SetDefault("'Big Red Button 2'" + "\n" +
				"20% increased clicker damage" + "\n" +
				"Reduces the amount of clicks required for a click effect by 1" + "\n" +
				"Gain up to 15% clicker damage based on your amount of clicks within a second" + "\n" +
				"Every 15 clicks releases a burst of damaging chocolate" + "\n" +
				"Makes the radius pulsate up to 50% of the default radius" + "\n" +
				"Pressing the '{$Mods.ClickerClass.Hotkeys.ClickerAccessory}' key will toggle auto click on all Clickers" + "\n" +
				"While auto click is enabled, click rates are moderately decreased");
		}

		public override void SetDefaults()
		{
			//To prevent this accessory from being equippable with similar ones of the same archetype (if it exists in the base mod), use this:
			//ClickerCompat.SetAccessoryType(Item, "ClickingGlove");
			Item.width = 28;
			Item.height = 20;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightRed;
			Item.accessory = true;
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

			//For the Cookie effect, you would need to set both the item and one of the visual variants
			//ClickerCompat.SetAccessoryItem(player, "Cookie", Item);
			//ClickerCompat.SetAccessory(player, "CookieVisual");

			//Enables the click effect given by Chocolate Chip
			//You can use Clicker Classes base effects (you can find them in the source code), or your own ones
			ClickerCompat.EnableClickEffect(player, "ClickerClass:ChocolateChip");

			//Sets an auto-reuse effect to be applied to the player for all clickers
			//In this case, it will make the clickers have a use time of 5 (resulting in 12 cps), and it will only work if the player uses the Clicker Class hotkey for toggling auto-reuse
			ClickerCompat.SetAutoReuseEffect(player, 5f, true, false);

			//How to check if an effect is enabled for the player
			bool hasChocolateChip = ClickerCompat.HasClickEffect(player, "ClickerClass:ChocolateChip");

			//Makes the radius go in a wave motion from 0 to 100 additional pixels
			float fluct = 1f + (float)Math.Sin(2 * Math.PI * (Main.GameUpdateCount % 60) / 60f);
			ClickerCompat.SetClickerRadiusAdd(player, fluct / 2);
		}
	}
}
