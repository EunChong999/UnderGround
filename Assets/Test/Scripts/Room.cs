using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 룸 클래스
public class Room {
	// 룸의 위치
	public Vector2 gridPos;
	// 룸의 타입 
	public int type;
	// 문의 불값
	public bool doorTop, doorBot, doorLeft, doorRight;
    // 룸 클래스 (룸의 위치, 룸의 타입) 정의 
    public Room(Vector2 _gridPos, int _type){
		// 룸의 위치 할당
		gridPos = _gridPos;
		// 룸의 타입 할당
		type = _type;
	}
}
