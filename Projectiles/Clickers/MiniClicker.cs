using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Projectiles.Clickers
{
	//Sample code for a clicker projectile
	//This projectile spawns and wiggles around for a bit more than a second, damaging enemies three times before disappearing
	public class MiniClicker : ModProjectile
	{
		//Optional, if you only want this item to exist only when Clicker Class is enabled
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ClickerCompat.ClickerClass != null;
		}

		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as a clicker projectile
			ClickerCompat.RegisterClickerProjectile(this);
		}

		public override void SetDefaults()
		{
			//This call is mandatory as it sets common stats like DamageType which is shared between all clicker projectiles
			ClickerCompat.SetClickerProjectileDefaults(Projectile);

			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.alpha = 255;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 70;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

			//Most clicker projectiles use local immunity
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}

		//Some custom AI, nothing clicker specific
		private int wiggleDirection = 0;
		private const float WiggleThreshold = MathHelper.Pi / 8;
		private const float WiggleSpeed = WiggleThreshold / 3;

		public override void AI()
		{
			if (Projectile.timeLeft < 255 / 30f)
			{
				Projectile.alpha += 30;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
				}
			}
			else
			{
				Projectile.alpha -= 50;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}

			if (wiggleDirection == 0)
			{
				//On spawn
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ClickerCompat.ClickerClass.Find<ModDust>("MiceDust").Type);
					dust.noGravity = true;
				}
				wiggleDirection = Main.rand.NextBool().ToDirectionInt();
			}

			if (wiggleDirection == 1 ? Projectile.rotation < WiggleThreshold : Projectile.rotation > -WiggleThreshold)
			{
				Projectile.rotation += wiggleDirection * WiggleSpeed;
			}
			else
			{
				wiggleDirection *= -1;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * Projectile.Opacity;
		}
	}
}
