using Terraria.ModLoader;

namespace ClickerClassExampleMod
{
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
