using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace OverKill.Items.Weapons
{
	public class MetallicCopper : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Metallic Copper");
			Tooltip.SetDefault("'There is copper in your eye!'");
            DisplayName.AddTranslation(GameCulture.Spanish, "Cobre met�lico");
            Tooltip.AddTranslation(GameCulture.Spanish, "'�Hay cobre en tus ojos!'");
        }

		public override void SetDefaults()
		{
			item.damage = 42;
			item.melee = true;
			item.width = 120;
			item.height = 120;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ProjectileID.Bee;
			item.shootSpeed = 5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar, 200);
			recipe.AddIngredient(175, 20);
			recipe.AddIngredient(3508);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = 3;
				item.useTime = 12;
				item.useAnimation = 12;
				item.damage = 26;
				item.shoot = ProjectileID.Bee;
			}
			else
			{
				item.useStyle = 1;
				item.useTime = 25;
				item.useAnimation = 25;
				item.damage = 42;
				item.shoot = 0;
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (player.altFunctionUse == 2)
			{
				target.AddBuff(BuffID.Ichor, 60);
			}
			else
			{
				target.AddBuff(BuffID.OnFire, 60);
			}
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				if (player.altFunctionUse == 2)
				{
					int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 169, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity.X += player.direction * 2f;
					Main.dust[dust].velocity.Y += 0.2f;
				}
				else
				{
					int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
					Main.dust[dust].noGravity = true;
				}
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			speedX = new Vector2(speedX, speedY).Length() * (speedX > 0 ? 1 : -1);
			speedY = 0;
			Vector2 speed = new Vector2(speedX, speedY);
			speed = speed.RotatedByRandom(MathHelper.ToRadians(30));
			damage = (int)(damage * .1f);
			speedX = speed.X;
			speedY = speed.Y;
			return true;
		}
	}
}