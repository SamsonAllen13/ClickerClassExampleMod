using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClassExampleMod
{
	// Copy this file for your mod, change the namespace above to yours, and read the comments
	/// <summary>
	/// Central file used for mod.Call wrappers.
	/// </summary>
	internal static class ClickerCompat
	{
		//GENERAL INFO - PLEASE READ THIS FIRST!
		//-----------------------
		//https://github.com/SamsonAllen13/ClickerClassExampleMod/wiki
		//-----------------------

		//This is the version of the calls that are used for the mod.
		//If Clicker Class updates, it will keep working on the outdated calls, but new features might not be available
		internal static readonly Version apiVersion = new Version(1, 2);

		internal static string versionString;

		private static Mod clickerClass;

		internal static Mod ClickerClass
		{
			get {
				if (clickerClass == null)
				{
					clickerClass = ModLoader.GetMod("ClickerClass");
				}
				return clickerClass;
			}
		}

		//Call this in your main Mod class in the Load hook like this: ClickerCompat.Load();
		internal static void Load()
		{
			versionString = apiVersion.ToString();
		}

		//Call this in your main Mod class in the Unload hook like this: ClickerCompat.Unload();
		internal static void Unload()
		{
			clickerClass = null;
			versionString = null;
		}

		//Here is a list of available calls you can do. Call them where required/recommended like this: ClickerCompat.SetClickerWeaponDefaults(item);
		//If they return something, they will try to default to a sensible value if Clicker Class is not loaded
		//Will throw an exception if something isn't right
		#region General Calls
		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> to set important default fields for a clicker weapon. Set fields:
		/// useTime, useAnimation, useStyle, holdStyle, noMelee, shoot, shootSpeed.
		/// Only change them afterwards if you know what you are doing!
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to set the defaults for</param>
		internal static void SetClickerWeaponDefaults(Item item)
		{
			ClickerClass?.Call("SetClickerWeaponDefaults", versionString, item);
		}

		/// <summary>
		/// Call this in <see cref="ModProjectile.SetStaticDefaults"/> to register this projectile into the "clicker class" category
		/// </summary>
		/// <param name="modProj">The <see cref="ModProjectile"/> that is to be registered</param>
		internal static void RegisterClickerProjectile(ModProjectile modProj)
		{
			ClickerClass?.Call("RegisterClickerProjectile", versionString, modProj);
		}

		/// <summary>
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this item into the "clicker class" category
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		internal static void RegisterClickerItem(ModItem modItem)
		{
			ClickerClass?.Call("RegisterClickerItem", versionString, modItem);
		}

		/// <summary>
		/// Call this in <see cref="ModItem.SetStaticDefaults"/> to register this weapon into the "clicker class" category as a "clicker".
		/// Do not call <see cref="RegisterClickerItem"/> with it as this method does this already by itself
		/// </summary>
		/// <param name="modItem">The <see cref="ModItem"/> that is to be registered</param>
		internal static void RegisterClickerWeapon(ModItem modItem)
		{
			ClickerClass?.Call("RegisterClickerWeapon", versionString, modItem);
		}

		/// <summary>
		/// Call this to check if a projectile type belongs to the "clicker class" category
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		internal static bool IsClickerProj(int type)
		{
			return ClickerClass?.Call("IsClickerProj", versionString, type) as bool? ?? false;
		}

		/// <summary>
		/// Call this to check if a projectile belongs to the "clicker class" category
		/// </summary>
		/// <param name="proj">The <see cref="Projectile"/> to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		internal static bool IsClickerProj(Projectile proj)
		{
			return ClickerClass?.Call("IsClickerProj", versionString, proj) as bool? ?? false;
		}

		/// <summary>
		/// Call this to check if an item type belongs to the "clicker class" category
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if that category</returns>
		internal static bool IsClickerItem(int type)
		{
			return ClickerClass?.Call("IsClickerItem", versionString, type) as bool? ?? false;
		}

		/// <summary>
		/// Call this to check if an item belongs to the "clicker class" category
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <returns><see langword="true"/> if a "clicker class" item</returns>
		internal static bool IsClickerItem(Item item)
		{
			return ClickerClass?.Call("IsClickerItem", versionString, item) as bool? ?? false;
		}

		/// <summary>
		/// Call this to check if an item type is a "clicker"
		/// </summary>
		/// <param name="type">The item type to be checked</param>
		/// <returns><see langword="true"/> if a "clicker"</returns>
		internal static bool IsClickerWeapon(int type)
		{
			return ClickerClass?.Call("IsClickerWeapon", versionString, type) as bool? ?? false;
		}

		/// <summary>
		/// Call this to check if an item is a "clicker"
		/// </summary>
		/// <param name="item">The <see cref="Item"/> to be checked</param>
		/// <returns><see langword="true"/> if a "clicker"</returns>
		internal static bool IsClickerWeapon(Item item)
		{
			return ClickerClass?.Call("IsClickerWeapon", versionString, item) as bool? ?? false;
		}
		#endregion

		#region Item Calls
		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its color used for various things
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="color">The color</param>
		public static void SetColor(Item item, Color color)
		{
			ClickerClass?.Call("SetColor", versionString, item, color);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its specific radius increase (1f means 100 pixel)
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="radius">The additional radius</param>
		public static void SetRadius(Item item, float radius)
		{
			ClickerClass?.Call("SetRadius", versionString, item, radius);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set the amount of clicks required for an effect to trigger
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="amount">the amount of clicks</param>
		public static void SetAmount(Item item, int amount)
		{
			ClickerClass?.Call("SetAmount", versionString, item, amount);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to define its effect
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="effect">the effect name</param>
		public static void SetEffect(Item item, string effect)
		{
			ClickerClass?.Call("SetEffect", versionString, item, effect);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker weapon to set its dust type when it's used
		/// </summary>
		/// <param name="item">The clicker weapon</param>
		/// <param name="type">the dust type</param>
		public static void SetDust(Item item, int type)
		{
			ClickerClass?.Call("SetDust", versionString, item, type);
		}

		/// <summary>
		/// Call in <see cref="ModItem.SetDefaults"/> for a clicker item to make it display total click count in the tooltip
		/// </summary>
		/// <param name="item">The clicker class item</param>
		public static void SetDisplayTotalClicks(Item item)
		{
			ClickerClass?.Call("SetDisplayTotalClicks", versionString, item);
		}
		#endregion

		#region Player Calls
		/// <summary>
		/// Call to get the players' clicker radius (multiply by 100 for pixels)
		/// </summary>
		/// <param name="player">The player</param>
		public static float GetClickerRadius(Player player)
		{
			return ClickerClass?.Call("GetPlayerStat", versionString, player, "clickerRadius") as float? ?? 1f;
		}

		/// <summary>
		/// Call to get the players' click amount (how many clicks done)
		/// </summary>
		/// <param name="player">The player</param>
		public static int GetClickAmount(Player player)
		{
			return ClickerClass?.Call("GetPlayerStat", versionString, player, "clickAmount") as int? ?? 0;
		}

		/// <summary>
		/// Call to get the players' total clicks required for the next effect to trigger
		/// </summary>
		/// <param name="player">The player</param>
		public static int GetClickerAmountTotal(Player player)
		{
			return ClickerClass?.Call("GetPlayerStat", versionString, player, "clickerAmountTotal") as int? ?? 1;
		}

		/// <summary>
		/// Call to check if the player is wearing a specific set. Supported sets:
		/// Motherboard, Overclock, Precursor, Mice
		/// </summary>
		/// <param name="player">The player</param>
		public static bool GetArmorSet(Player player, string set)
		{
			return ClickerClass?.Call("GetArmorSet", versionString, player, set) as bool? ?? false;
		}

		/// <summary>
		/// Call to check if a specific accessory effect is enabled (i.e. "Gamer Crate" will have multiple effects enabled). Supported accessories:
		/// ChocolateChip, EnchantedLED, HandCream, StickyKeychain, GlassOfMilk, Cookie, ClickingGlove, AncientClickingGlove, RegalClickingGlove.
		/// Visual variants (i.e. EnchantedLED2) are not gettable
		/// </summary>
		/// <param name="player">The player</param>
		public static bool GetAccessory(Player player, string accessory)
		{
			return ClickerClass?.Call("GetAccessory", versionString, player, accessory) as bool? ?? false;
		}

		/// <summary>
		/// Call to set a specific player accessory effect (i.e. to emulate "Gamer Crate" you need to have set multiple effects). Supported accessories:
		/// ChocolateChip, EnchantedLED, HandCream, StickyKeychain, GlassOfMilk, Cookie, ClickingGlove, AncientClickingGlove, RegalClickingGlove.
		/// EnchantedLED and Cookie have a variant with "2" added to them that is a visual variation.
		/// </summary>
		/// <param name="player">The player</param>
		public static void SetAccessory(Player player, string accessory)
		{
			ClickerClass?.Call("SetAccessory", versionString, player, accessory);
		}

		/// <summary>
		/// Call to add to the players' clicker crit value
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">crit chance added</param>
		public static void SetClickerCritAdd(Player player, int add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerCritAdd", add);
		}

		/// <summary>
		/// Call to add to the players' clicker flat damage value
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">flat damage added</param>
		public static void SetDamageFlatAdd(Player player, int add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerDamageFlatAdd", add);
		}

		/// <summary>
		/// Call to add to the players' clicker damage value in %
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">damage added in %</param>
		public static void SetDamageAdd(Player player, float add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerDamageAdd", add);
		}

		/// <summary>
		/// Call to modify the players' effect threshold
		/// (2 will mean 2 less clicks required to reach the effect trigger threshold)
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">amount of clicks to reduce</param>
		public static void SetClickerBonusAdd(Player player, int add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerBonusAdd", add);
		}

		/// <summary>
		/// Call to modify the players' effect threshold
		/// (-0.20f will mean 20% less clicks required to reach the effect trigger threshold)
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">% of total clicks</param>
		public static void SetClickerBonusPercentAdd(Player player, float add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerBonusPercentAdd", add);
		}

		/// <summary>
		/// Call to add to the players' clicker radius (default 1f)
		/// </summary>
		/// <param name="player">The player</param>
		/// <param name="add">distance added in 100 pixels (1f = 100 pixel)</param>
		public static void SetClickerRadiusAdd(Player player, float add)
		{
			ClickerClass?.Call("SetPlayerStat", versionString, player, "clickerRadiusAdd", add);
		}
		#endregion
	}
}
