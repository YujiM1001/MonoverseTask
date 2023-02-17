using UnityEngine;

public enum LocationItem
{
	NONE = -1,
	MAP = 0,
	INVENTORY,
}

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

	// 현재 아이템의 위치 : 맵, 인벤 
	public LocationItem location = LocationItem.NONE;
}
