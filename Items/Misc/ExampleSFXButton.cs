using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Misc
{
	public class ExampleSFXButton : ModItem
	{
		//Optional, if you only want this item to exist only when Clicker Class is enabled
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ClickerCompat.ClickerClass != null;
		}

		//Optional, reuse a clicker class tooltip that all sfx buttons share. Remove if you want to specify your own
		public override LocalizedText Tooltip => Language.GetText("Mods.ClickerClass.Common.Tooltips.SFXButtonTip").WithFormatArgs(Item.maxStack);

		/// <summary>
		/// The method used to play a sound
		/// </summary>
		/// <param name="stack">Usually used to control the volume by multiplying with 0.5f, ranges from 1 to 5</param>
		public static void PlaySound(int stack)
		{
			SoundStyle style = SoundID.Coins.WithVolumeScale(0.5f * stack) with
			{
				PitchVariance = 0.2f,
			};
			SoundEngine.PlaySound(style);
		}

		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as an sfx button and to register the sound playback
			//Doing so will automatically apply its functionality: While in the inventory (or opened Void Bag) it will apply its sound effect every click
			ClickerCompat.RegisterSFXButton(this, (Action<int>)PlaySound); //The cast is necessary here to avoid a warning
		}

		public override void SetDefaults()
		{
			//This call is mandatory as it sets common stats like maxStack which is shared between all sfx buttons
			ClickerCompat.SetSFXButtonDefaults(Item);

			Item.width = 20;
			Item.height = 20;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Green;
		}

		//Optional: Showcase of some API methods
		public override void UpdateInventory(Player player)
		{
			//Let's make it so that this item also acts as the soundboard 25% of the time
			//DISCLAIMER: Calling this for your own item is not needed, it gets taken care of by RegisterSFXButton

			if (!ModContent.TryFind<ModItem>("ClickerClass/SFXSoundboard", out var soundboard))
			{
				return;
			}

			if (Main.rand.NextBool(4))
			{
				return;
			}

			bool atMaxStacks = ClickerCompat.AddSFXButtonStack(player, soundboard.Type, Item.stack);

			//We can also fetch currently active sfx button stacks (though this is not the correct timing, use a hook like ModPlayer.PostUpdate)
			var current = ClickerCompat.GetAllSFXButtonStacks(player);
			//Just for testing, we can verify that it works - it should spawn dust on the players head
			if (current.ContainsKey(soundboard.Type))
			{
				Dust.QuickDust(player.Top.ToTileCoordinates(), Color.White);
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 10).AddTile(TileID.WorkBenches).Register();
		}
	}
}
