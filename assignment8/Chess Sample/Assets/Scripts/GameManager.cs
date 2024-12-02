using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹들
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;   // King, Queen, Bishop, Knight, Rook, Pawn 순
    public GameObject EffectPrefab;

    // 오브젝트의 parent들
    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1; // 현재 턴 1 - 백, -1 - 흑
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];   // Tile들
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];    // Piece들

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
    // 8x8로 타일들을 배치
    for (int x = 0; x < Utils.FieldWidth; x++)
    {
        for (int y = 0; y < Utils.FieldHeight; y++)
        {
            GameObject tileObj = Instantiate(TilePrefab, TileParent);
            Tile tile = tileObj.GetComponent<Tile>();
            tile.Set((x, y));
            Tiles[x, y] = tile;
        }
    }

    // 초기 기물 배치
    PlacePieces(1);  // 백
    PlacePieces(-1); // 흑
    }


    void PlacePieces(int direction)
    {
        int baseRow = direction == 1 ? 0 : 7;
        int pawnRow = direction == 1 ? 1 : 6;
        PlacePiece(4, (0, baseRow), direction); // 룩
        PlacePiece(3, (1, baseRow), direction); // 나이트
        PlacePiece(2, (2, baseRow), direction); // 비숍숍
        PlacePiece(1, (3, baseRow), direction); // 퀸
        PlacePiece(0, (4, baseRow), direction); // 킹
        PlacePiece(2, (5, baseRow), direction); // 비숍
        PlacePiece(3, (6, baseRow), direction); // 나이트
        PlacePiece(4, (7, baseRow), direction); // 룩
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            PlacePiece(5, (x, pawnRow), direction); // 폰
        }
    }


    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        GameObject pieceObj = Instantiate(PiecePrefabs[pieceType], PieceParent);
        pieceObj.transform.position = new Vector3(pos.Item1, 0, pos.Item2);
        Piece piece = pieceObj.GetComponent<Piece>();
        piece.initialize(pos, direction); 
        Pieces[pos.Item1, pos.Item2] = piece;
        return piece;
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;

        // 기물 제거
        (int x, int y) = targetPos;
        if (Pieces[x, y] != null)
        {
            Destroy(Pieces[x, y].gameObject);
            Pieces[x, y] = null;
        }

        // 이동
        Pieces[piece.MyPos.Item1, piece.MyPos.Item2] = null;
        piece.MoveTo(targetPos); 
        Pieces[x, y] = piece;

        // 턴 변경
        ChangeTurn();
        if (movementManager.IsInCheck(CurrentTurn)){
            uiManager.ShowCheck();
        }
    }
    void ChangeTurn()
    {
        CurrentTurn = CurrentTurn == 1 ? -1 : 1;
        uiManager.UpdateTurn(CurrentTurn); 
    }
}
