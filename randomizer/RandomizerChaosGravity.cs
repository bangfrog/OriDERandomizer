using System;
using Game;
using UnityEngine;

// Token: 0x020009F5 RID: 2549
public class RandomizerChaosGravity : RandomizerChaosEffect
{
	// Token: 0x06003765 RID: 14181
	public override void Clear()
	{
		this.Countdown = 0;
		this.WellActive = false;
		Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityAngle = 0f;
		this.ApplyGravityMultiplier(1f);
	}

	// Token: 0x06003766 RID: 14182
	public override void Start()
	{
		this.Countdown = UnityEngine.Random.Range(600, 1200);
		this.WellActive = false;
		int num = UnityEngine.Random.Range(0, 16);
		if (num <= 2)
		{
			Randomizer.showChaosEffect("Gravity increase");
			this.ApplyGravityMultiplier(UnityEngine.Random.Range(1.1f, 2f));
			return;
		}
		if (num <= 7)
		{
			Randomizer.showChaosEffect("Gravity decrease");
			this.ApplyGravityMultiplier(UnityEngine.Random.Range(0.1f, 0.9f));
			return;
		}
		if (num <= 10)
		{
			Randomizer.showChaosEffect("Gravity shift");
			Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityAngle = UnityEngine.Random.Range(45f, 315f);
			return;
		}
		if (num <= 13)
		{
			Randomizer.showChaosEffect("Weird gravity");
			Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityAngle = UnityEngine.Random.Range(0f, 360f);
			this.ApplyGravityMultiplier(1f / UnityEngine.Random.Range(0.5f, 2f));
			return;
		}
		if (num <= 15)
		{
			Randomizer.showChaosEffect("Gravity well");
			this.WellActive = true;
			this.WellPosition = new Vector2(Characters.Sein.Position.x + UnityEngine.Random.Range(-20f, 20f), Characters.Sein.Position.y + UnityEngine.Random.Range(-20f, 20f));
			this.WellStrength = UnityEngine.Random.Range(16f, 32f);
		}
	}

	// Token: 0x06003767 RID: 14183
	public override void Update()
	{
		if (this.Countdown == 0)
		{
			this.Clear();
		}
		this.Countdown--;
		if (this.Countdown < -600)
		{
			this.Countdown = 0;
		}
		if (this.WellActive)
		{
			Vector2 vector = new Vector2(this.WellPosition.x - Characters.Sein.Position.x, this.WellPosition.y - Characters.Sein.Position.y);
			float num = this.WellStrength / Math.Max(1f, vector.sqrMagnitude);
			float friction = 1f;
			vector.Normalize();
			if (Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
			{
				friction = 0.25f;
			}
			Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedX = Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedX + friction * num * vector.x;
			if (!Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround || num > 26f)
			{
				Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = Characters.Sein.PlatformBehaviour.PlatformMovement.LocalSpeedY + num * vector.y;
			}
		}
	}

	// Token: 0x06003769 RID: 14185
	public void ApplyGravityMultiplier(float multiplier)
	{
		Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.GravityStrength = 26f * multiplier;
		Characters.Sein.PlatformBehaviour.Gravity.BaseSettings.MaxFallSpeed = 32f * multiplier;
		float jumpHeight = 3f / multiplier;
		Characters.Sein.Abilities.Jump.BackflipJumpHeight = jumpHeight;
		Characters.Sein.Abilities.Jump.CrouchJumpHeight = jumpHeight;
		Characters.Sein.Abilities.Jump.FirstJumpHeight = jumpHeight;
		Characters.Sein.Abilities.Jump.JumpIdleHeight = jumpHeight;
		Characters.Sein.Abilities.Jump.SecondJumpHeight = jumpHeight;
		Characters.Sein.Abilities.Jump.ThirdJumpHeight = jumpHeight;
	}

	// Token: 0x04003234 RID: 12852
	public int Countdown;

	// Token: 0x04003235 RID: 12853
	public bool WellActive;

	// Token: 0x04003236 RID: 12854
	public Vector2 WellPosition;

	// Token: 0x04003237 RID: 12855
	public float WellStrength;
}
