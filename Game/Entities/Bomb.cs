using Godot;
using System;

public partial class Bomb : RigidBody2D
{
	private float Timeout = 10f;
	private Timer timer;
	private CollisionShape2D area;
	private int Damage = 50;
	[Export]
	private Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitTimer();
	}

	private void InitTimer()
	{
		timer = GetNode<Timer>("ExplosionTimer");
		timer.WaitTime = Timeout;
		timer.Start();
	}

	private void _OnExplosionTimerTimeout()
	{
		// get distance to player
		// lower damage the further away the player is
		var playerDistance = GlobalPosition.DistanceTo(player.GlobalPosition);
		var damage = Damage - (int)playerDistance;
		if (damage <= 0)
		{
			player.TakeDamage(Damage);
		}
		// TOOD: area collections by types, avoiding groups
		/*area = GetNode<CollisionShape2D>("Area");
		var allbodies = get_overlapping_bodies();
		foreach (var body in allbodies)
		{
			if (body is Enemy enemy)
			{
				var enemyDistance = GlobalPosition.DistanceTo(enemy.GlobalPosition);
        		var enemyDamage = Damage - (int)enemyDistance;

        		// lower damage the further away the enemies are
      	  		if (enemyDamage <= 0)
     	   		{
    	        	enemy.TakeDamage(enemyDamage);
   	    		}
			}
		}*/
	}
}
