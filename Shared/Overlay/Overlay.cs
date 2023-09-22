using System.Linq;
using Godot;

public partial class Overlay : Control
{
	[Signal]
	public delegate void OverlayShownEventHandler();

	[Signal]
	public delegate void OverlayHiddenEventHandler();

	public void AddItem<T>(T item) where T : Control, IOverlayItem
	{
		CallDeferred(nameof(DeferredAddItem), item);
	}

	public void DeferredAddItem(Control item)
	{
		Visible = true;

		foreach (var child in GetChildren())
		{
			((Control)child).Visible = false;
		}

		((IOverlayItem)item).Overlay = this;
		AddChild(item);

		if (GetChildren().Count() == 1)
		{
			EmitSignal(SignalName.OverlayShown);
		}
	}

	public void Back()
	{
		CallDeferred(nameof(DeferredBack));
	}

	public void DeferredBack()
	{
		var children = GetChildren();
		var count = children.Count();

		if (count == 0)
		{
			return;
		}

		for (var i = 0; i < count; i++)
		{
			var ctrl = (Control)children[i];

			if (i == count - 1)
			{
				ctrl.QueueFree();
			}
			else if (i == count - 2)
			{
				ctrl.Visible = true;
			}
			else
			{
				ctrl.Visible = false;
			}
		}

		if (count - 1 == 0)
		{
			Visible = false;
			EmitSignal(SignalName.OverlayHidden);
		}
	}
}
