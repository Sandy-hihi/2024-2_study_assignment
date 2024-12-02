using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();   // 현재 effect들을 저장할 리스트
    
    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        (int x, int y) direction = (moveInfo.dirX, moveInfo.dirY);
        (int startX, int startY) = piece.MyPos;

        for (int i = 1; i <= moveInfo.distance; i++)
        {
            (int newX, int newY) = (startX + direction.x * i, startY + direction.y * i);

            if (!Utils.IsInBoard((newX, newY))) return false; // 보드 바깥인지 확인
            if (piece is Pawn) 
            {
                if (direction.x != 0) 
                {
                    if (gameManager.Pieces[newX, newY] != null &&
                        gameManager.Pieces[newX, newY].PlayerDirection != piece.PlayerDirection)
                    {
                    
                        return (newX, newY) == targetPos;
                    }
                    return false; 
                }
                else // 직선 이동일 경우
                {
                    if (gameManager.Pieces[newX, newY] != null) return false; // 경로에 다른 말이 있으면 이동 불가
                    if ((newX, newY) == targetPos) return true; // 목표 위치 도달
                }
            }
            else // 다른 기물의 일반 이동 처리
            {
                if (gameManager.Pieces[newX, newY] != null) // 다른 말에 막히는지 확인
                {
                    if (i == moveInfo.distance && gameManager.Pieces[newX, newY].PlayerDirection != piece.PlayerDirection)
                    {
                        // 마지막 칸에 적이 있을 경우, 공격 가능
                        return (newX, newY) == targetPos;
                    }
                    return false; // 중간에 장애물 있음
                }

                if ((newX, newY) == targetPos) return true; // 목표 위치 도달
            }
        }

        return false; // 이동 불가
    }



    // 체크를 제외한 상황에서 가능한 움직임인지를 검증
    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
                return true;
        }
        
        return false;
    }

    // 체크를 포함한 상황에서 가능한 움직임인지를 검증
    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }

    // 체크인지를 확인
    public bool IsInCheck(int playerDirection)
    {
        (int kingX, int kingY) = (-1, -1);

        // 왕의 위치를 찾기
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingX = x;
                    kingY = y;
                    break;
                }
            }
            if (kingX != -1 && kingY != -1) break;
        }

        // 왕의 위치를 기준으로 적의 움직임을 확인
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var enemyPiece = gameManager.Pieces[x, y];
                if (enemyPiece != null && enemyPiece.PlayerDirection != playerDirection)
                {
                    if (IsValidMoveWithoutCheck(enemyPiece, (kingX, kingY)))
                        return true; // 적이 왕을 공격할 수 있음
                }
            }
        }

        return false; // 체크 상태 아님
    }


    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();

        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                if (IsValidMove(piece, (x, y)))
                {
                    var effect = Instantiate(effectPrefab, effectParent);
                    effect.transform.position = Utils.ToRealPos((x, y));
                    currentEffects.Add(effect);
                }
            }
        }
    }


    // 효과 비우기
    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}