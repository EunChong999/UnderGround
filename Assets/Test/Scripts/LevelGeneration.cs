using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 레벨 제너레이터 클래스
public class LevelGeneration : MonoBehaviour {
	// 맵의 전체 넓이
	Vector2 worldSize = new Vector2(2,2);
	// 룸 배열
	Room[,] rooms;
	// 룸들의 위치들
	List<Vector2> takenPositions = new List<Vector2>();
	// X좌표의 격자 크기, Y좌표의 격자 크기, 룸의 개수
	int gridSizeX, gridSizeY, numberOfRooms = 8;
	// 기본적인 맵
	public GameObject roomWhiteObj;
	// 모든 맵의 루트 노드
	public Transform mapRoot;
	void Start () {
        // 그리드에 들어갈 수 있는 것보다 더 많은 방을 만들려고 하지 않도록 조치
		// 방의 개수가 맵의 크기보다 크거나 같을 때 
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2)){ 
			// 방의 개수를 격자에 맞게 정수화
			numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
		}
		
		// 맵의 크기의 X좌표를 정수화 
		gridSizeX = Mathf.RoundToInt(worldSize.x);
        // 맵의 크기의 Y좌표를 정수화 
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        // 맵의 실물을 만들다
        CreateRooms();
        // 방들을 연결하는 문들을 배치
        SetRoomDoors();
        // 지도를 만들기 위해 사물을 인스턴스화 
        DrawMap();
		// 룸 정보를 레벨 지오메트리 생성을 처리하는 다른 스크립트에 전달
		GetComponent<SheetAssigner>().Assign(rooms);
	}
	void CreateRooms(){
		// 설정
		// 룸 배열의 크기에 맵의 크기의 두 배만큼 할당
		rooms = new Room[gridSizeX * 2,gridSizeY * 2];
		// 룸 배열의 중앙 좌표에 최초 위치와 룸 타입을 할당 
		rooms[gridSizeX,gridSizeY] = new Room(Vector2.zero, 1);
		// 룸 위치 리스트의 첫번째 인덱스에 최초 위치 삽입
		takenPositions.Insert(0,Vector2.zero);
		// 확인 위치에 최초 위치 할당
		Vector2 checkPos = Vector2.zero;
		// 매직 넘버들 
		// 랜덤 비교 넘버, 랜덤 비교 시작 넘버, 랜덤 비교 종료 넘버
		float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
		// 룸 추가 
		for (int i =0; i < numberOfRooms -1; i++){
			// 랜덤 진행도
			// (현재 방의 인덱스 / 방의 개수 - 1)
			float randomPerc = ((float) i) / (((float)numberOfRooms - 1));
			// 랜덤 비교 넘버는 시작과 종료 넘버의 랜덤 진행도를 통한 선행 보간으로 구함
			randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
			// 확인 위치에 새로운 위치를 할당
			checkPos = NewPosition();
			// 확인 위치로부터 룸 위치 리스트에 이웃이 있는지 확인하고 1.0과 0.0 사이의 랜덤 값보다 방의 개수에 비례한 보간값이 클 때
			if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare){
				// 반복 변수 
				int iterations = 0;
				do{
					// 확인 위치에 선택적 새 위치 할당
					checkPos = SelectiveNewPosition();
					// 반복 변수 증가
					iterations++;
				// 확인 위치로부터 룸 위치 리스트에 이웃이 있는지 확인하고 반복 변수가 100 미만일 때
				}while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
				// 반복 변수가 50 이상일 때
				if (iterations >= 50)
                    // 보다 적은 이웃으로 만들 수 없다고 경고문을 출력
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
			}
            // 위치 완성
            // 방의 배열 (확인 위치 X + 맵의 중앙 X, 확인 위치 Y + 맵의 중앙 Y) = 새로운 방(확인 위치, 타입 0) 
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos, 0);
			// 룸 위치 리스트의 첫 번째 인덱스에 확인 위치를 삽입
			takenPositions.Insert(0,checkPos);
		}	
	}
	Vector2 NewPosition(){
		// 변수 x와 y 선언
		int x = 0, y = 0;
		// 확인 위치 초기화
		Vector2 checkingPos = Vector2.zero;
		// 반복문
		do{
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // 룸 리스트에서 룸을 랜덤으로 선택
			x = (int) takenPositions[index].x; // 룸의 x 좌표를 포착
            y = (int) takenPositions[index].y; // 룸의 y 좌표를 포착
            bool UpDown = (Random.value < 0.5f); // 임의로 행과 열을 볼 것인지 선택하다
            bool positive = (Random.value < 0.5f); // 그 축에서 긍정적일지 부정적일지를 고르다
            // 위의 불값들을 기준으로 하여 그 위치를 수색
            if (UpDown){ // 행이라면
				if (positive){ // 긍정이면 y값을 +1
					y += 1;
				}else{ // 부정이면 y값을 -1
					y -= 1;
				}
			}else{ // 열이라면
				if (positive){ // 긍정이면 x값을 +1
					x += 1;
				}else{ // 부정이면 x값을 -1
					x -= 1;
				}
			}
			// 확인 위치를 할당
			checkingPos = new Vector2(x,y);
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); // 그 자리가 타당한지 확인하다
		// 룸 위치 리스트에 확인 위치가 있다면 || x값이 맵의 X 크기 이상일 때 || x값이 맵의 -X 크기 미만일 때 || y값이 맵의 Y 크기 이상일 때 || y값이 맵의 -Y 크기 미만일 때 
		// 확인 위치를 반환
        return checkingPos;
	}
	Vector2 SelectiveNewPosition(){ // 함수를 위의 두가지 언급된 방법과 달리한다
		// 인덱스와 횟수를 초기화
		int index = 0, inc = 0;
		// x와 y를 초기화
		int x =0, y =0;
		// 확인 위치를 초기화
		Vector2 checkingPos = Vector2.zero;
		// 반복문
		do{
			// 횟수를 초기화
			inc = 0;
			do{
                // 빈 공간을 찾기 위해 방을 얻는 대신, 오직 하나로서 시작 
                // 하나의 이웃으로서. 이렇게 되면 그것이 가지치는 방을 돌려줄 가능성이 더 높아지게 될 것이다
				// 인덱스에 룸 위치 리스트의 갯수를 넘어가지 않도록 값을 무작위로 할당
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				// 횟수를 추가
				inc ++;
			}while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100); 
			// 해당 인덱스에 해당하는 곳에 룸 위치에 이웃이 있는지 확인하고 반복 변수가 100 미만일 때
			// 해당 인덱스에 해당하는 위치의 x값에 x값을 할당
            x = (int) takenPositions[index].x;
            // 해당 인덱스에 해당하는 위치의 y값에 y값을 할당
            y = (int) takenPositions[index].y;
			bool UpDown = (Random.value < 0.5f); // 임의로 행과 열을 볼 것인지 선택하다
            bool positive = (Random.value < 0.5f); // 그 축에서 긍정적일지 부정적일지를 고르다
            if (UpDown){ // 행이라면
				if (positive){
					y += 1; // 긍정이면 y값을 +1
				}else{
					y -= 1; // 부정이면 y값을 -1
                }
			}else{ // 열이라면
				if (positive){
					x += 1; // 긍정이면 x값을 +1
				}else{
					x -= 1; // 부정이면 x값을 -1
                }
			}
            // 확인 위치를 할당
            checkingPos = new Vector2(x,y); 
		}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); // 그 자리가 타당한지 확인하다
		// 룸 위치 리스트에 확인 위치가 있다면 || x값이 맵의 X 크기 이상일 때 || x값이 맵의 -X 크기 미만일 때 || y값이 맵의 Y 크기 이상일 때 || y값이 맵의 -Y 크기 미만일 때
        if (inc >= 100){ // 시간이 너무 오래 걸리면 루프를 끊는다: 이 루프는 해결책을 찾을 것이 보장되지 않으며, 이것은 이것에 좋다
			print("Error: could not find position with only one neighbor"); // 오직 하나의 이웃을 가진 위치를 찾지 못함
		}
		// 확인 위치를 반환
		return checkingPos;
	}
	// 이웃의 개수
	// (확인 위치, 지정된 위치들)
	int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions){ 
		int ret = 0; // 0에서 시작하고, 각 면에 1을 더하면 이미 방이 있다
		// 단순화를 위해 벡터[방향]를 짧은 손으로 사용
        if (usedPositions.Contains(checkingPos + Vector2.right)){ // 지정된 위치 중에서 확인 위치의 오른쪽 위치가 존재할 때
			ret++; // ret을 추가, 이미 존재
		}
		if (usedPositions.Contains(checkingPos + Vector2.left)){ // 지정된 위치 중에서 확인 위치의 왼쪽 위치가 존재할 때
			ret++; // ret을 추가, 이미 존재 
        }
		if (usedPositions.Contains(checkingPos + Vector2.up)){ // 지정된 위치 중에서 확인 위치의 위쪽 위치가 존재할 때
			ret++; // ret을 추가, 이미 존재
        }
		if (usedPositions.Contains(checkingPos + Vector2.down)){ // 지정된 위치 중에서 확인 위치의 아래쪽 위치가 존재할 때
			ret++; // ret을 추가, 이미 존재
        }
		return ret; // 원래 위치를 반환
	}
	// 지도를 그리는 함수
	void DrawMap(){
		foreach (Room room in rooms){ // 룸들에 있는 각각의 룸을 꺼낸다
			if (room == null){ 
				continue; // 자리가 없는 곳을 건너뛰다
            }
			Vector2 drawPos = room.gridPos; // 그리기 위치에 방의 위치를 할당
			drawPos.x *= 16; // 지도 스프라이트 종횡비에 맞게 그리기 위치의 x값에 16을 곱한다
            drawPos.y *= 8; // 지도 스프라이트 종횡비에 맞게 그리기 위치의 y값에 8을 곱한다
			// 맵 개체를 만들고 변수를 할당
			MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
			mapper.type = room.type;
			mapper.up = room.doorTop;
			mapper.down = room.doorBot;
			mapper.right = room.doorRight;
			mapper.left = room.doorLeft;
			mapper.gameObject.transform.parent = mapRoot;
		}
	}
	// 문 배치
	void SetRoomDoors(){
		// 맵의 X 크기만큼 반복
		for (int x = 0; x < ((gridSizeX * 2)); x++){
            // 맵의 Y 크기만큼 반복
            for (int y = 0; y < ((gridSizeY * 2)); y++){
				// 해당 배열에 방이 존재하지 않는다면 건너뛰기
				if (rooms[x,y] == null){
					continue;
				}
				// 위치에 x값과 y값을 할당
				Vector2 gridPosition = new Vector2(x,y);
				if (y - 1 < 0){ // 위쪽을 확인, 맵의 끝 쪽이면
					// 방의 아래문 X
					rooms[x,y].doorBot = false;
				}else{
					// 해당 방의 아래 위치에 방이 존재하면 방의 아래문 O
					rooms[x,y].doorBot = (rooms[x,y-1] != null);
				}
				if (y + 1 >= gridSizeY * 2){ // 아래쪽을 확인, 맵의 끝 쪽이면
					// 방의 윗문 X
					rooms[x,y].doorTop = false;
				}else{
					// 해당 방의 위의 위치에 방이 존재하면 방의 윗문 O
					rooms[x,y].doorTop = (rooms[x,y+1] != null);
				}
				if (x - 1 < 0){ // 왼쪽을 확인, 맵의 끝 쪽이면
					// 방의 왼쪽 문 X
					rooms[x,y].doorLeft = false;
				}else{ 
					// 해당 방의 왼쪽의 위치에 방이 존재하면 방의 왼쪽 문 O
					rooms[x,y].doorLeft = (rooms[x - 1,y] != null);
				}
				if (x + 1 >= gridSizeX * 2){ // 오른쪽을 확인, 맵의 끝 쪽이면
					// 방의 오른쪽 문 X
					rooms[x,y].doorRight = false;
				}else{
					// 해당 방의 오른쪽의 위치에 방이 존재하면 방의 오른쪽 문 O
					rooms[x,y].doorRight = (rooms[x+1,y] != null);
				}
			}
		}
	}
}
