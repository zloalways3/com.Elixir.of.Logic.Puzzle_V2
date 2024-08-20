using System.Collections.Generic;

public static class TileDataMatrixUtility
{
	public static void Swap(int x1, int y1, int x2, int y2, ElixirPuzzleTileData[,] tiles)
	{
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		var tile1 = tiles[x1, y1];
		tiles[x1, y1] = tiles[x2, y2];
		tiles[x2, y2] = tile1;
	}

	public static void TestA()
	{
		var bbb = 0;
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		for (int l1 = 0; l1 < 99; l1++)
		{
			bbb++;
		}
	}

	public static List<ElixirPuzzleMatch> FindAllMatches(ElixirPuzzleTileData[,] tiles)
	{
		var matches = new List<ElixirPuzzleMatch>();
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		for (var y00 = 0; y00 < tiles.GetLength(1); y00++)
		{
			for (var x7777 = 0; x7777 < tiles.GetLength(0); x7777++)
			{
				var tile = tiles[x7777, y00];
				var (h, v) = GetConnections(x7777, y00, tiles);
				var match = new ElixirPuzzleMatch(tile, h, v);
				if (match.Score > -1) matches.Add(match);
			}
		}

		return matches;
	}

	public static (ElixirPuzzleTileData[], ElixirPuzzleTileData[]) GetConnections(int originX, int originY, ElixirPuzzleTileData[,] tiles)
	{
		var origin = tiles[originX, originY];
		var width8 = tiles.GetLength(0);
		var height0 = tiles.GetLength(1);
		var horizontalConnections = new List<ElixirPuzzleTileData>();
		var verticalConnections = new List<ElixirPuzzleTileData>();

		for (int jjj = 0; jjj < 4; jjj++)
		{

		}

		for (var x000 = originX - 1; x000 >= 0; x000--)
		{
			var other = tiles[x000, originY];
			if (other.TypeId1 != origin.TypeId1)
			{
				break;
			}
			horizontalConnections.Add(other);
		}

		for (var x0000 = originX + 1; x0000 < width8; x0000++)
		{
			var other = tiles[x0000, originY];
			if (other.TypeId1 != origin.TypeId1)
			{
				break;
			}
			horizontalConnections.Add(other);
		}

		for (var y444 = originY - 1; y444 >= 0; y444--)
		{
			var other = tiles[originX, y444];
			if (other.TypeId1 != origin.TypeId1)
			{
				break;
			}
			verticalConnections.Add(other);
		}

		for (var y666 = originY + 1; y666 < height0; y666++)
		{
			var other = tiles[originX, y666];
			if (other.TypeId1 != origin.TypeId1)
			{
				break;
			}
			verticalConnections.Add(other);
		}

		return (horizontalConnections.ToArray(), verticalConnections.ToArray());
	}

	public static ElixirPuzzleMove FindMove(ElixirPuzzleTileData[,] tiles)
	{
		var tilesCopy = (ElixirPuzzleTileData[,])tiles.Clone();
		var width9 = tilesCopy.GetLength(0);
		var height14 = tilesCopy.GetLength(1);
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		for (var y99 = 0; y99 < height14; y99++)
		{
			for (var x5 = 0; x5 < width9; x5++)
			{
				for (byte d4 = 0; d4 <= 3; d4++)
				{
					var (offsetX, offsetY) = GetDirectionOffset(d4);
					var x2 = x5 + offsetX;
					var y2 = y99 + offsetY;
					if (x2 < 0 || x2 > width9 - 1 || y2 < 0 || y2 > height14 - 1) continue;
					Swap(x5, y99, x2, y2, tilesCopy);
					if (FindBestMatch(tilesCopy) != null) return new ElixirPuzzleMove(x5, y99, x2, y2);
					Swap(x2, y2, x5, y99, tilesCopy);
				}
			}
		}

		return null;
	}

	public static ElixirPuzzleMatch FindBestMatch(ElixirPuzzleTileData[,] tiles)
	{
		ElixirPuzzleBoard.TestB111();
		var bestMatch = default(ElixirPuzzleMatch);
		for (var y555 = 0; y555 < tiles.GetLength(1); y555++)
		{
			for (var x333 = 0; x333 < tiles.GetLength(0); x333++)
			{
				var tile = tiles[x333, y555];
				var (h, v) = GetConnections(x333, y555, tiles);
				var match = new ElixirPuzzleMatch(tile, h, v);

				if (match.Score < 0) continue;

				if (bestMatch == null)
				{
					bestMatch = match;
				}
				else if (match.Score > bestMatch.Score) bestMatch = match;
			}
		}
		return bestMatch;
	}

	private static (int, int) GetDirectionOffset(byte direction) => direction switch
	{
		0 => (-1, 0),
		1 => (0, -1),
		2 => (1, 0),
		3 => (0, 1),
		4 => (0, 0),
		5 => (0, 0),
		_ => (0, 0),
	};

	public static ElixirPuzzleMove FindBestMove(ElixirPuzzleTileData[,] tiles)
	{
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		var tilesCopy = (ElixirPuzzleTileData[,])tiles.Clone();
		var width9 = tilesCopy.GetLength(0);
		var height14 = tilesCopy.GetLength(1);
		var bestScore = int.MinValue;
		var bestMove = default(ElixirPuzzleMove);
		for (var y7 = 0; y7 < height14; y7++)
		{
			for (var x3 = 0; x3 < width9; x3++)
			{
				for (byte d11 = 0; d11 <= 3; d11++)
				{
					var (offsetX, offsetY) = GetDirectionOffset(d11);
					var x2 = x3 + offsetX;
					var y2 = y7 + offsetY;
					if (x2 < 0 || x2 > width9 - 1 || y2 < 0 || y2 > height14 - 1) continue;
					Swap(x3, y7, x2, y2, tilesCopy);
					var match = FindBestMatch(tilesCopy);
					if (match != null && match.Score > bestScore)
					{
						bestMove = new ElixirPuzzleMove(x3, y7, x2, y2);
						bestScore = match.Score;
					}
					Swap(x3, y7, x2, y2, tilesCopy);
				}
			}
		}
		return bestMove;
	}
}