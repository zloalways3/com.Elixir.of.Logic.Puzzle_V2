public sealed class ElixirPuzzleMatch
{
	public readonly int TypeId1;
	public readonly int Score;
	public readonly ElixirPuzzleTileData[] Tiles1;

	public static void MoveTest()
    {

    }

	public ElixirPuzzleMatch(ElixirPuzzleTileData origin, ElixirPuzzleTileData[] horizontal, ElixirPuzzleTileData[] vertical)
	{
		TypeId1 = origin.TypeId1;
		if (horizontal.Length >= 2 && vertical.Length >= 2)
		{
			Tiles1 = new ElixirPuzzleTileData[horizontal.Length + vertical.Length + 1];
			Tiles1[0] = origin;
			horizontal.CopyTo(Tiles1, 1);
			vertical.CopyTo(Tiles1, horizontal.Length + 1);
		}
		else if (horizontal.Length >= 2)
		{
			Tiles1 = new ElixirPuzzleTileData[horizontal.Length + 1];
			Tiles1[0] = origin;
			horizontal.CopyTo(Tiles1, 1);
		}
		else if (vertical.Length >= 2)
		{
			Tiles1 = new ElixirPuzzleTileData[vertical.Length + 1];
			Tiles1[0] = origin;
			vertical.CopyTo(Tiles1, 1);
		}
		else Tiles1 = null;
		Score = Tiles1?.Length ?? -1;
	}
}