using Terraria.ModLoader;

namespace ClickerClassExampleMod
{
	//TODO for developers:
	//Add some projectile example
	public class ClickerClassExampleMod : Mod
	{
		public override void Load()
		{
			ClickerCompat.Load();
		}

		public override void Unload()
		{
			ClickerCompat.Unload();
		}
	}
}
