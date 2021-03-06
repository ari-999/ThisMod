﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ThisMod.NPCs
{

	public class FloatingEater : ModNPC
	{




		public override void SetDefaults()
		{
			npc.width = 16;
			npc.height = 14;
			npc.aiStyle = 49; //nimbus is 49, current spore is 50
			npc.damage = 20;
			npc.friendly = false;
			npc.lifeMax = 50;
			npc.defense = 20;
			npc.lavaImmune = true;
			npc.noTileCollide = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (!Main.hardMode)
				return SpawnCondition.Sky.Chance * 0.10f;
			return 0;
		}

		// Allows hitting the NPC with melee type weapons, even if it's friendly.
		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return true;
		}

		// Same as the above but with projectiles.
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return projectile.friendly && projectile.owner < 255;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100; i++)
				{
					Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, hitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
					dust.noGravity = true;
				}
				return;
			}
			for (int i = 0; i < 50; i++)
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, 2 * hitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
				dust.noGravity = true;
			}
		}

		// Allows the NPC to talk with the player, even if it isn't a town NPC.
		public override bool CanChat()
		{
			return false;
		}



		// Only show health bar of the NPC when close to the player
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 200)
			{
				if (distance > 100)
				{
					// Make the health bar become smaller the farther away the NPC is.
					scale *= (100 - (distance - 100)) / 100;
				}
				return null;
			}
			return false;
		}
	}
}

