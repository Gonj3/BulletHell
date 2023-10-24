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

		var spawnPos = GetSpawnableTile() + new Vector2(8, 8);

		enemyInstance.Position = spawnPos;
		enemyInstance.World = this;
		enemyInstance.Connect("Death", deathCallback);

		AddChild(enemyInstance);
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
	public void ThrowBomb(Vector2 pos, float angle)
	{
		var bombInstance = (Bomb)bombScene.Instantiate();
		bombInstance.Position = pos;
		bombInstance.SetAngle(angle);

		GetParent().AddChild(bombInstance);
		bombInstance.ApplyImpulse(bombInstance.vector * 40);
	}
}
