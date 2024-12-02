using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public (int, int) MyPos;    // 자신의 좌표
    public int PlayerDirection = 1; // 자신의 방향 1 - 백, -1 - 흑
    
    public Sprite WhiteSprite;  // 백일 때의 sprite
    public Sprite BlackSprite;  // 흑일 때의 sprite
    
    protected GameManager MyGameManager;
    protected SpriteRenderer MySpriteRenderer;

    void Awake()
    {
        MyGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Piece의 초기 설정 함수
    public void initialize((int, int) targetPos, int direction)
    {
        PlayerDirection = direction;
        initSprite(PlayerDirection);
        MoveTo(targetPos);
    }

    // sprite 초기 설정 함수
    void initSprite(int direction)
    {
        if (direction == 1)
        {
            MySpriteRenderer.sprite = WhiteSprite;
        }
        else
        {
            MySpriteRenderer.sprite = BlackSprite;
        }

        // sprite 방향 (회전) 설정
        transform.localScale = new Vector3(1, direction == 1 ? 1 : -1, 1); // 흑은 반대로 회전
    }


    // piece의 실제 이동 함수
    public void MoveTo((int, int) targetPos)
    {
        // 이전 위치 초기화
        if (MyGameManager.Pieces[MyPos.Item1, MyPos.Item2] == this)
        {
            MyGameManager.Pieces[MyPos.Item1, MyPos.Item2] = null;
        }

        // 새로운 위치 설정
        MyPos = targetPos;
        MyGameManager.Pieces[targetPos.Item1, targetPos.Item2] = this;

        // 실제 위치 갱신 (World 좌표 변환)
        transform.position = Utils.ToRealPos(targetPos);
    }

    
    public abstract MoveInfo[] GetMoves();
}
