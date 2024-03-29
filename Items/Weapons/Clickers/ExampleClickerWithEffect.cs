using ClickerClassExampleMod.Projectiles.Clickers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;

namespace ClickerClassExampleMod.Items.Weapons.Clickers
{
	//A more advanced example using a custom click effect, which uses a custom clicker projectile
	public class ExampleClickerWithEffect : ModItem
	{
		public static readonly int MiniClickerAmount = 5;
		public static readonly int DamageRatioPercent = 20;

		public static string ExampleEffect { get; private set; } = string.Empty;

		public override void Unload()
		{
			ExampleEffect = string.Empty;
		}

		public override bool IsLoadingEnabled(Mod mod)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//Here we register an optional border/outline texture aswell
			ClickerCompat.RegisterClickerWeapon(this, borderTexture: Texture + "_Outline");

			//Here we register a click effect which we reference in SetDefaults through AddEffect
			string uniqueName = ClickerCompat.RegisterClickEffect(Mod, "ExampleEffect", 6, Color.Red, delegate (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, int type, int damage, float knockBack)
			{
				SoundEngine.PlaySound(SoundID.Chat, position);
				for (int i = 0; i < MiniClickerAmount; i++)
				{
					Projectile.NewProjectile(source, position + 20 * Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi), Vector2.Zero, ModContent.ProjectileType<MiniClicker>(), (int)(damage * DamageRatioPercent / 100f), 0f, Main.myPlayer);
				}
			},
			preHardMode: true,
			descriptionArgs: new object[] { MiniClickerAmount, DamageRatioPercent });
			//The preHardMode parameter flags it as available in pre-hardmode, useful for content referencing other effects
			//The next two parameters are optional and used for localization usage with substitutions for display name and description. Here the amount of mini clickers spawned and the damage they deal is reflected in the description (found in the localization file) so we need to supply those

			//We want to cache the result to make accessing it easier in other places.
			//(Make sure to unload the saved string again!)
			ExampleEffect = uniqueName;
		}

		public override void SetDefaults()
		{
			ClickerCompat.SetClickerWeaponDefaults(Item);

			ClickerCompat.SetRadius(Item, 1f);
			ClickerCompat.SetColor(Item, Color.Red);
			ClickerCompat.SetDust(Item, DustID.SomethingRed);

			//Here we use our custom effect, registered as 'modName:internalName'
			ClickerCompat.AddEffect(Item, "ClickerClassExampleMod:ExampleEffect");
			//We can also use the cached variable for it instead
			//ClickerCompat.AddEffect(Item, ExampleEffect);

			//We can add more than one effect aswell! (Here using the IEnumerable overload to make it more compact)
			ClickerCompat.AddEffect(Item, new List<string> { "ClickerClass:Inferno", "ClickerClass:Embrittle" });

			//We can also access all available effects and do stuff with it
			//Here pick the first registered effect (random would be cool but Main.rand shouldn't be used in SetDefaults)
			List<string> allEffects = ClickerCompat.GetAllEffectNames();
			if (allEffects != null && allEffects.Count > 0)
			{
				ClickerCompat.AddEffect(Item, allEffects[0]);
				//If this happens to be one we already added, it won't do anything
			}

			//In total, atleast 3 effects

			Item.damage = 10;
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 1f;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.MagmaStone, 10).AddTile(TileID.Anvils).Register();
		}
	}
}
