using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ClickerClassExampleMod.Projectiles.Clickers
{
	//Sample code for a clicker weapon. Currently, only RegisterClickerProjectile is required, the rest is up to you

	//This projectile spawns and wiggles around for a bit more than a second, damaging enemies three times before disappearing
	public class MiniClicker : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//You NEED to call this in SetStaticDefaults to make it count as a clicker projectile
			ClickerCompat.RegisterClickerProjectile(this);
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.alpha = 255;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.timeLeft = 70;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;

			//Most clicker projectiles use local immunity
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 30;
		}

		//Some custom AI, nothing clicker specific
		private int wiggleDirection = 0;
		private const float WiggleThreshold = MathHelper.Pi / 8;
		private const float WiggleSpeed = WiggleThreshold / 3;

		public override void AI()
		{
			if (projectile.timeLeft < 255 / 30f)
			{
				projectile.alpha += 30;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				projectile.alpha -= 50;
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
			}

			if (wiggleDirection == 0)
			{
				//On spawn
				for (int i = 0; i < 3; i++)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ClickerCompat.ClickerClass.DustType("MiceDust"));
					dust.noGravity = true;
				}
				wiggleDirection = Main.rand.NextBool().ToDirectionInt();
			}

			if (wiggleDirection == 1 ? projectile.rotation < WiggleThreshold : projectile.rotation > -WiggleThreshold)
			{
				projectile.rotation += wiggleDirection * WiggleSpeed;
			}
			else
			{
				wiggleDirection *= -1;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * ((255 - projectile.alpha) / 255f);
		}
	}
}
