using Godot;

public partial class World : Node2D
{
	[Export]
	public Player Player { get; private set; }

	[Export]
	private TileMap tileMap;

	[Export]
	private PackedScene projectileScene;

	[Export]
	private PackedScene bombScene;

	[Export]
	private PackedScene enemyScene;

	[Export]
	private PackedScene bossScene;

	[Export]
	private PackedScene powerupScene;


	private Vector2 bossSpawnPos = new Vector2(1000, 1000);

	public void SpawnProjectile(Vector2 pos, float angle, DamageableKind target, Projectile.Type type)
	{
		var projInstance = (Projectile)projectileScene.Instantiate();

		projInstance.Position = pos;
		projInstance.SetAngle(angle);
		projInstance.Target = target;
		projInstance.SetType(type);

		AddChild(projInstance);
	}

	public void SpawnEnemy(Callable deathCallback)
	{
		var enemyInstance = (Enemy)enemyScene.Instantiate();

		var offset = tileMap.CellQuadrantSize / 2;

		var spawnPos = GetSpawnableTile() + new Vector2(offset, offset);

		enemyInstance.Position = spawnPos;
		enemyInstance.World = this;
		enemyInstance.Connect("Death", deathCallback);

		AddChild(enemyInstance);
	}

	public void SpawnBoss(Callable deathCallback)
	{
		var bossInstance = (Boss)bossScene.Instantiate();

		bossInstance.Position = bossSpawnPos;
		bossInstance.World = this;
		bossInstance.Connect("Death", deathCallback);

		AddChild(bossInstance);
	}

	public void SpawnPowerup(Vector2 pos)
	{
		var powerupInstance = (PowerUp)powerupScene.Instantiate();

		powerupInstance.Position = pos;

		AddChild(powerupInstance);
	}

	private Vector2 GetSpawnableTile()
	{
		var cells = tileMap.GetUsedCells(0);

		while (true)
		{
			var coords = cells.PickRandom();
			if (tileMap.GetCellTileData(0, coords).GetCustomData("Spawnable").AsBool())
				return coords * tileMap.Scale * tileMap.CellQuadrantSize;
		}
	}
	// no target because a bomb should hit everything.
	public void ThrowBomb(Vector2 pos, float angle, int force)
	{
		var bombInstance = (Bomb)bombScene.Instantiate();
		bombInstance.Position = pos;
		bombInstance.SetAngle(angle);

		GetParent().AddChild(bombInstance);
		bombInstance.ApplyImpulse(bombInstance.vector * force);
	}
}
