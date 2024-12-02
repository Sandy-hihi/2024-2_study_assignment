using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rook.cs
public class Rook : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
            // 오른쪽
            new MoveInfo(1, 0, 1), new MoveInfo(1, 0, 2), new MoveInfo(1, 0, 3),
            new MoveInfo(1, 0, 4), new MoveInfo(1, 0, 5), new MoveInfo(1, 0, 6),
            new MoveInfo(1, 0, 7), new MoveInfo(1, 0, 8),

            // 왼쪽
            new MoveInfo(-1, 0, 1), new MoveInfo(-1, 0, 2), new MoveInfo(-1, 0, 3),
            new MoveInfo(-1, 0, 4), new MoveInfo(-1, 0, 5), new MoveInfo(-1, 0, 6),
            new MoveInfo(-1, 0, 7), new MoveInfo(-1, 0, 8),

            // 위
            new MoveInfo(0, 1, 1), new MoveInfo(0, 1, 2), new MoveInfo(0, 1, 3),
            new MoveInfo(0, 1, 4), new MoveInfo(0, 1, 5), new MoveInfo(0, 1, 6),
            new MoveInfo(0, 1, 7), new MoveInfo(0, 1, 8),

            // 아래
            new MoveInfo(0, -1, 1), new MoveInfo(0, -1, 2), new MoveInfo(0, -1, 3),
            new MoveInfo(0, -1, 4), new MoveInfo(0, -1, 5), new MoveInfo(0, -1, 6),
            new MoveInfo(0, -1, 7), new MoveInfo(0, -1, 8)
        };
    }

}
