using Godot;

public partial class PlayerCamera : Camera2D
{
	[Export]
	private TileMap tileMap;

	public override void _Ready()
	{
		var rect = tileMap.GetUsedRect();
		var quadSize = tileMap.CellQuadrantSize;
		var scaleX = (int)tileMap.Scale.X;
		var scaleY = (int)tileMap.Scale.Y;

		LimitLeft = rect.Position.X * quadSize * scaleX;
		LimitTop = rect.Position.Y * quadSize * scaleY;
		LimitRight = rect.End.X * quadSize * scaleX;
		LimitBottom = rect.End.Y * quadSize * scaleY;
	}
}
