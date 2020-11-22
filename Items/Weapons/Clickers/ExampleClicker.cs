using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Weapons.Clickers
{
	//Sample code for a clicker weapon
	public class ExampleClicker : ModItem
	{
		//Optional, if you only want this item to exist only when Clicker Class is enabled
		public override bool Autoload(ref string name)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as a clicker weapon. also sets the default tooltip
			ClickerCompat.RegisterClickerWeapon(this);

			DisplayName.SetDefault("Example Clicker");
		}

		public override void SetDefaults()
		{
			//This call is mandatory as it sets common stats like useStyle which is shared between all clickers
			ClickerCompat.SetClickerWeaponDefaults(item);

			//Use these methods to adjust clicker weapon specific stats
			ClickerCompat.SetRadius(item, 0.3f);
			ClickerCompat.SetColor(item, Color.White);
			ClickerCompat.SetDust(item, DustID.Fire);

			//These two aren't finished/implemented properly yet so you can only use Clicker Classes Effects (you can find them in the source code)
			//ClickerCompat.SetEffect(item, "Embrittle");
			//ClickerCompat.SetAmount(item, 10);

			item.damage = 4;
			item.width = 30;
			item.height = 30;
			item.knockBack = 0.5f;
			item.value = 20;
			item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
