using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knight.cs
public class Knight : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
            new MoveInfo(2, 1, 1),  // 오른쪽 2칸, 위로 1칸
            new MoveInfo(2, -1, 1), // 오른쪽 2칸, 아래로 1칸
            new MoveInfo(-2, 1, 1), // 왼쪽 2칸, 위로 1칸
            new MoveInfo(-2, -1, 1),// 왼쪽 2칸, 아래로 1칸
            new MoveInfo(1, 2, 1),  // 위로 2칸, 오른쪽 1칸
            new MoveInfo(1, -2, 1), // 위로 2칸, 왼쪽 1칸
            new MoveInfo(-1, 2, 1), // 아래로 2칸, 오른쪽 1칸
            new MoveInfo(-1, -2, 1) // 아래로 2칸, 왼쪽 1칸
        };
    }

}