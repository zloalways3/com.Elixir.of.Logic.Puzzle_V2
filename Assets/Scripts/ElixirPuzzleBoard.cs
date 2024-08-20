using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class ElixirPuzzleBoard : MonoBehaviour
{
	[SerializeField] private ElixirPuzzleTileTypeAsset[] tileTypes;
	[SerializeField] private ElixirPuzzleRow[] rows;
	[SerializeField] private float tweenDuration;
	[SerializeField] private Transform swappingOverlay;
	[SerializeField] private bool ensureNoStartingMatches;

	private readonly List<ElixirPuzzleTile> _selection = new List<ElixirPuzzleTile>();

	private bool _isSwapping;
	private bool _isMatching;
	private bool _isShuffling;

	public event Action<ElixirPuzzleTileTypeAsset, int> OnMatch;
	[SerializeField] private ElixirPuzzleController elixirPuzzleController;

	private ElixirPuzzleTileData[,] Matrix
	{
		get
		{
			var width333 = rows.Max(row => row.tiles1.Length);
			var height222 = rows.Length;
			var data = new ElixirPuzzleTileData[width333, height222];
			for (var y7 = 0; y7 < height222; y7++)
			{
				for (var x7 = 0; x7 < width333; x7++)
				{
					data[x7, y7] = ElixirPuzzleGetTile(x7, y7).Data;
				}
			}
			return data;
		}
	}

	public void ElixirPuzzleUpdateBoard()
	{
		ElixirPuzzleShuffle();
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		StartCoroutine(ElixirPuzzleEnsureNoStartingMatches());
	}

	private void Start()
	{
		for (var y666 = 0; y666 < rows.Length; y666++)
		{
			for (var x44 = 0; x44 < rows.Max(row => row.tiles1.Length); x44++)
			{
				var tile = ElixirPuzzleGetTile(x44, y666);
				tile.x15 = x44;
				tile.y15 = y666;
				tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];
				tile.bu.onClick.AddListener(() => ElixirPuzzleSelect(tile));
			}
		}

		if (ensureNoStartingMatches) StartCoroutine(ElixirPuzzleEnsureNoStartingMatches());
		OnMatch += (type, count) => elixirPuzzleController.ElixirPuzzleUpdatePoints(count);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var kot = 0;
			kot++;
			var bestMove = TileDataMatrixUtility.FindBestMove(Matrix);
			if (bestMove != null)
			{
				ElixirPuzzleSelect(ElixirPuzzleGetTile(bestMove.X11, bestMove.Y11));
				ElixirPuzzleSelect(ElixirPuzzleGetTile(bestMove.X22, bestMove.Y22));
			}
		}
	}

	private IEnumerator ElixirPuzzleEnsureNoStartingMatches()
	{
		var wait = new WaitForEndOfFrame();
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
		{
			ElixirPuzzleShuffle();
			yield return wait;
		}
	}

	private ElixirPuzzleTile ElixirPuzzleGetTile(int x, int y)
	{
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		return rows[y].tiles1[x];
	}

	private ElixirPuzzleTile[] ElixirPuzzleGetTiles(IList<ElixirPuzzleTileData> tileData)
	{
		var length33 = tileData.Count;
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		var tiles111 = new ElixirPuzzleTile[length33];
		for (var i44 = 0; i44 < length33; i44++)
		{
			tiles111[i44] = ElixirPuzzleGetTile(tileData[i44].X44, tileData[i44].Y44);
		}
		return tiles111;
	}

	public static void TestB111()
	{
		var bb11 = 0;
		var f34 = 0;
		for (int l6611 = 0; l6611 < 7; l6611++)
		{
			f34++;
			bb11++;
		}
	}

	private async void ElixirPuzzleSelect(ElixirPuzzleTile tile)
	{
		if (_isSwapping || _isMatching || _isShuffling)
		{
			return;
		}
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		if (!_selection.Contains(tile))
		{
			if (_selection.Count > 0)
			{
				if (Math.Abs(tile.x15 - _selection[0].x15) == 1 && Math.Abs(tile.y15 - _selection[0].y15) == 0
					|| Math.Abs(tile.y15 - _selection[0].y15) == 1 && Math.Abs(tile.x15 - _selection[0].x15) == 0)
				{
					_selection.Add(tile);
				}
			}
			else
			{
				_selection.Add(tile);
			}
		}

		if (_selection.Count < 2)
		{
			return;
		}
		await ElixirPuzzleSwapAsync(_selection[0], _selection[1]);
		if (!await ElixirPuzzleTryMatchAsync())
		{
			await ElixirPuzzleSwapAsync(_selection[0], _selection[1]);
		}
		var matrix5 = Matrix;
		while (TileDataMatrixUtility.FindBestMove(matrix5) == null || TileDataMatrixUtility.FindBestMatch(matrix5) != null)
		{
			ElixirPuzzleShuffle();
			matrix5 = Matrix;
		}
		_selection.Clear();
	}

	private async Task ElixirPuzzleSwapAsync(ElixirPuzzleTile tile1, ElixirPuzzleTile tile2)
	{
		_isSwapping = true;
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		var icon1 = tile1.icon44;
		var icon2 = tile2.icon44;
		var icon1Transform = icon1.transform;
		var icon2Transform = icon2.transform;
		icon1Transform.SetParent(swappingOverlay);
		icon2Transform.SetParent(swappingOverlay);
		icon1Transform.SetAsLastSibling();
		icon2Transform.SetAsLastSibling();
		var sequen = DOTween.Sequence();
		sequen.Join(icon1Transform.DOMove(icon2Transform.position, tweenDuration).SetEase(Ease.OutBack))
				.Join(icon2Transform.DOMove(icon1Transform.position, tweenDuration).SetEase(Ease.OutBack));
		await sequen.Play()
					  .AsyncWaitForCompletion();
		icon1Transform.SetParent(tile2.transform);
		icon2Transform.SetParent(tile1.transform);
		tile1.icon44 = icon2;
		tile2.icon44 = icon1;
		var tile1Item22 = tile1.Type;
		tile1.Type = tile2.Type;
		tile2.Type = tile1Item22;
		_isSwapping = false;
	}

	private async Task<bool> ElixirPuzzleTryMatchAsync()
	{
		var didMatch = false;
		_isMatching = true;
		var match = TileDataMatrixUtility.FindBestMatch(Matrix);
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		while (match != null)
		{
			didMatch = true;
			var tiles3 = ElixirPuzzleGetTiles(match.Tiles1);
			var deflateSequence = DOTween.Sequence();
			foreach (var tile in tiles3)
			{
				deflateSequence.Join(tile.icon44.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
			}
			elixirPuzzleController.ElixirPuzzleCollectSound();
			await deflateSequence.Play()
								 .AsyncWaitForCompletion();
			var inflateSequence = DOTween.Sequence();
			foreach (var tile in tiles3)
			{
				tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];
				inflateSequence.Join(tile.icon44.transform.DOScale(new Vector2(0.7f, 0.7f), tweenDuration).SetEase(Ease.OutBack));
			}
			await inflateSequence.Play()
								 .AsyncWaitForCompletion();
			OnMatch?.Invoke(Array.Find(tileTypes, tileType => tileType.id == match.TypeId1), match.Tiles1.Length);
			match = TileDataMatrixUtility.FindBestMatch(Matrix);
		}
		_isMatching = false;
		return didMatch;
	}

	public void TestTestTest()
    {
		Debug.Log("AAAATEST");
    }

	public void ElixirPuzzleShuffle()
	{
		_isShuffling = true;
		for (int jjj = 0; jjj < 4; jjj++)
		{

		}
		foreach (var row in rows)
		{
			foreach (var tile in row.tiles1)
			{
				tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];
			}
		}
		_isShuffling = false;
	}
}