using ClickerClassExampleMod.Projectiles.Clickers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Items.Weapons.Clickers
{
	//A more advanced example using a custom click effect, which uses a custom clicker projectile
	public class ExampleClickerWithEffect : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//Here we register an optional border/outline texture aswell
			ClickerCompat.RegisterClickerWeapon(this, borderTexture: "ClickerClassExampleMod/Items/Weapons/Clickers/ExampleClickerWithEffect_Outline");

			//We want to cache the result to make accessing it easier in other places. For the purpose of the example, we don't
			//(Make sure to unload the saved string again in Mod.Unload()!)
			string uniqueName = ClickerCompat.RegisterClickEffect(mod, "ExampleEffect", "Mini Clickers", "Creates 5 Mini Clickers around the cursor for 20% damage", 6, Color.Red, delegate (Player player, Vector2 position, int type, int damage, float knockBack)
			{
				Main.PlaySound(SoundID.Chat, (int)position.X, (int)position.Y, -1);
				for (int i = 0; i < 5; i++)
				{
					Projectile.NewProjectile(Main.MouseWorld + 20 * Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi), Vector2.Zero, ModContent.ProjectileType<MiniClicker>(), (int)(damage * 0.2f), 0f, Main.myPlayer);
				}
			});

			DisplayName.SetDefault("Example Clicker With Effect");
		}

		public override void SetDefaults()
		{
			ClickerCompat.SetClickerWeaponDefaults(item);

			ClickerCompat.SetRadius(item, 1f);
			ClickerCompat.SetColor(item, Color.Red);
			ClickerCompat.SetDust(item, DustID.SomethingRed);

			//Here we use our custom effect, registered as 'modName:internalName'
			ClickerCompat.AddEffect(item, "ClickerClassExampleMod:ExampleEffect");

			//We can add more than one effect aswell! (Here using the IEnumerable overload to make it more compact)
			ClickerCompat.AddEffect(item, new List<string> { "ClickerClass:Inferno", "ClickerClass:Embrittle" });

			//We can also access all available effects and do stuff with it
			//Here pick the first registered effect (random would be cool but Main.rand shouldn't be used in SetDefaults)
			List<string> allEffects = ClickerCompat.GetAllEffectNames();
			if (allEffects != null && allEffects.Count > 0)
			{
				ClickerCompat.AddEffect(item, allEffects[0]);
				//If this happens to be one we already added, it won't do anything
			}

			//In total, atleast 3 effects

			item.damage = 10;
			item.width = 30;
			item.height = 30;
			item.knockBack = 1f;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MagmaStone, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
