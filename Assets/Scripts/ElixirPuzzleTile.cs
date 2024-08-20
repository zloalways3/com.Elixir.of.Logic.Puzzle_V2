using UnityEngine;
using UnityEngine.UI;

public sealed class ElixirPuzzleTile : MonoBehaviour
{
	public int x15;
	public int y15;
	public Image icon44;
	public Button bu;
	private ElixirPuzzleTileTypeAsset _type1;

	public void TestB()
	{
		var kkkkk = 0;
		var bb = 0;
		for (int l777 = 0; l777 < 77; l777++)
		{
			bb++;
		}
	}

	public ElixirPuzzleTileData Data => new ElixirPuzzleTileData(x15, y15, _type1.id);

	public ElixirPuzzleTileTypeAsset Type
	{
		get => _type1;
		set
		{
			if (_type1 == value) return;

			_type1 = value;

			icon44.sprite = _type1.sprite;
		}
	}

	public void TestA()
	{
		var eeee = 0;
		var bbb = 0;
		for (int l000 = 0; l000 < 99; l000++)
		{
			bbb++;
		}
	}
}