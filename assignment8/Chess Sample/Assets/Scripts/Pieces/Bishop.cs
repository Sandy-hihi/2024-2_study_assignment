using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
            // 오른쪽 위 대각선
            new MoveInfo(1, 1, 1), new MoveInfo(1, 1, 2), new MoveInfo(1, 1, 3), 
            new MoveInfo(1, 1, 4), new MoveInfo(1, 1, 5), new MoveInfo(1, 1, 6), 
            new MoveInfo(1, 1, 7), new MoveInfo(1, 1, 8),

            // 왼쪽 위 대각선
            new MoveInfo(-1, 1, 1), new MoveInfo(-1, 1, 2), new MoveInfo(-1, 1, 3), 
            new MoveInfo(-1, 1, 4), new MoveInfo(-1, 1, 5), new MoveInfo(-1, 1, 6), 
            new MoveInfo(-1, 1, 7), new MoveInfo(-1, 1, 8),

            // 오른쪽 아래 대각선
            new MoveInfo(1, -1, 1), new MoveInfo(1, -1, 2), new MoveInfo(1, -1, 3), 
            new MoveInfo(1, -1, 4), new MoveInfo(1, -1, 5), new MoveInfo(1, -1, 6), 
            new MoveInfo(1, -1, 7), new MoveInfo(1, -1, 8),

            // 왼쪽 아래 대각선
            new MoveInfo(-1, -1, 1), new MoveInfo(-1, -1, 2), new MoveInfo(-1, -1, 3), 
            new MoveInfo(-1, -1, 4), new MoveInfo(-1, -1, 5), new MoveInfo(-1, -1, 6), 
            new MoveInfo(-1, -1, 7), new MoveInfo(-1, -1, 8)
        };
    }
}

