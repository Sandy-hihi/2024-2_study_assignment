using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
            // 대각선 이동 (오른쪽 위, 왼쪽 위, 오른쪽 아래, 왼쪽 아래)
            new MoveInfo(1, 1, 1), new MoveInfo(1, 1, 2), new MoveInfo(1, 1, 3),
            new MoveInfo(1, 1, 4), new MoveInfo(1, 1, 5), new MoveInfo(1, 1, 6),
            new MoveInfo(1, 1, 7), new MoveInfo(1, 1, 8),

            new MoveInfo(-1, 1, 1), new MoveInfo(-1, 1, 2), new MoveInfo(-1, 1, 3),
            new MoveInfo(-1, 1, 4), new MoveInfo(-1, 1, 5), new MoveInfo(-1, 1, 6),
            new MoveInfo(-1, 1, 7), new MoveInfo(-1, 1, 8),

            new MoveInfo(1, -1, 1), new MoveInfo(1, -1, 2), new MoveInfo(1, -1, 3),
            new MoveInfo(1, -1, 4), new MoveInfo(1, -1, 5), new MoveInfo(1, -1, 6),
            new MoveInfo(1, -1, 7), new MoveInfo(1, -1, 8),

            new MoveInfo(-1, -1, 1), new MoveInfo(-1, -1, 2), new MoveInfo(-1, -1, 3),
            new MoveInfo(-1, -1, 4), new MoveInfo(-1, -1, 5), new MoveInfo(-1, -1, 6),
            new MoveInfo(-1, -1, 7), new MoveInfo(-1, -1, 8),

            // 수평 이동 (오른쪽, 왼쪽)
            new MoveInfo(1, 0, 1), new MoveInfo(1, 0, 2), new MoveInfo(1, 0, 3),
            new MoveInfo(1, 0, 4), new MoveInfo(1, 0, 5), new MoveInfo(1, 0, 6),
            new MoveInfo(1, 0, 7), new MoveInfo(1, 0, 8),

            new MoveInfo(-1, 0, 1), new MoveInfo(-1, 0, 2), new MoveInfo(-1, 0, 3),
            new MoveInfo(-1, 0, 4), new MoveInfo(-1, 0, 5), new MoveInfo(-1, 0, 6),
            new MoveInfo(-1, 0, 7), new MoveInfo(-1, 0, 8),

            // 수직 이동 (위, 아래)
            new MoveInfo(0, 1, 1), new MoveInfo(0, 1, 2), new MoveInfo(0, 1, 3),
            new MoveInfo(0, 1, 4), new MoveInfo(0, 1, 5), new MoveInfo(0, 1, 6),
            new MoveInfo(0, 1, 7), new MoveInfo(0, 1, 8),

            new MoveInfo(0, -1, 1), new MoveInfo(0, -1, 2), new MoveInfo(0, -1, 3),
            new MoveInfo(0, -1, 4), new MoveInfo(0, -1, 5), new MoveInfo(0, -1, 6),
            new MoveInfo(0, -1, 7), new MoveInfo(0, -1, 8)
        };
    }

}