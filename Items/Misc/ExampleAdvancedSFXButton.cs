using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Misc
{
	//A more advanced example using API methods to use other sfx buttons' sounds
	//Basic comments are removed here in favor of showcasing advanced behavior
	public class ExampleAdvancedSFXButtonSystem : ModSystem
	{
		public static Dictionary<int, Action<int>> SFXButtonToPlaySound { get; private set; }
		public static bool PlayedSoundThisTick { get; set; } = false;

		public override void OnModLoad()
		{
			SFXButtonToPlaySound = new Dictionary<int, Action<int>>();
		}

		public override void PostSetupContent()
		{
			//Get all modded sfx buttons and add them to a dictionary
			foreach (var modItem in ModContent.GetContent<ModItem>().Where(m => ClickerCompat.IsSFXButton(m.Type)))
			{
				SFXButtonToPlaySound[modItem.Type] = ClickerCompat.GetSFXButton(modItem.Type);
			}
		}

		public override void PostUpdatePlayers()
		{
			PlayedSoundThisTick = false;
		}

		public override void OnModUnload()
		{
			SFXButtonToPlaySound = null;
			PlayedSoundThisTick = false;
		}
	}

	public class ExampleAdvancedSFXButton : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Item.maxStack);

		public static void PlaySound(int stack)
		{
			//Recursion checks (if another mod does the same thing: calling other sfx buttons's sound method).
			//This may cause a sound to not play at all but prevents the game from crashing
			if (ExampleAdvancedSFXButtonSystem.PlayedSoundThisTick)
			{
				return;
			}
			ExampleAdvancedSFXButtonSystem.PlayedSoundThisTick = true;

			//Avoid recursion with self
			var list = ExampleAdvancedSFXButtonSystem.SFXButtonToPlaySound
				.Where(pair => pair.Key != ModContent.ItemType<ExampleAdvancedSFXButton>())
				.Select(pair => pair.Value)
				.ToList();

			var playSound = Main.rand.Next(list);
			playSound.Invoke(stack);
		}

		public override void SetStaticDefaults()
		{
			ClickerCompat.RegisterSFXButton(this, (Action<int>)PlaySound); //The cast is necessary here to avoid a warning
		}

		public override void SetDefaults()
		{
			ClickerCompat.SetSFXButtonDefaults(Item);

			Item.width = 20;
			Item.height = 20;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Orange;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 20).AddTile(TileID.WorkBenches).Register();
		}
	}
}
