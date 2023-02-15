using UnityEngine;

public enum GroupItem
{
	NONE = -1,
	MAP = 0,
	INVENTORY,
}

// TODO : 테스트 끝나면 제거 
[System.Serializable]
public class Item
{
	// 고유 컬러 
	public float colorR = 0f;
	public float colorG = 0f;
	public float colorB = 0f;
	public float colorA = 0f;

	// 맵내 위치
	public float positionX = 0f;
	public float positionY = 0f;
	public float positionZ = 0f;

	// 위치 : 맵, 인벤 
	public GroupItem group = GroupItem.NONE;
}
