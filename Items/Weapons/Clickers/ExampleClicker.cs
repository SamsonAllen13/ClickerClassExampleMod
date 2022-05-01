using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Weapons.Clickers
{
	//Sample code for a clicker weapon
	public class ExampleClicker : ModItem
	{
		//Optional, if you only want this item to exist only when Clicker Class is enabled
		public override bool IsLoadingEnabled(Mod mod)
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
			ClickerCompat.SetClickerWeaponDefaults(Item);

			//Use these methods to adjust clicker weapon specific stats
			ClickerCompat.SetRadius(Item, 0.3f);
			ClickerCompat.SetColor(Item, Color.White);
			ClickerCompat.SetDust(Item, 6);

			//You can use Clicker Classes base effects (you can find them in the source code), or your own ones
			ClickerCompat.AddEffect(Item, "ClickerClass:DoubleClick");

			Item.damage = 4;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 0.5f;
			Item.value = 20;
			Item.rare = ItemRarityID.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Wood, 10).AddTile(TileID.WorkBenches).Register();
		}
	}
}
