using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilManager : SingletonGameObject<UtilManager>
{
    /// <summary>
    /// GameObject 생성 및 초기화 후, 해당 오브젝트 리턴. 
    /// </summary>
    /// <param name="prefab">생성하고자 하는 오브젝트</param>
    /// <param name="transformParent">생성한 오브젝트의 부모 transform</param>
    /// <returns>생성된 gameobject</returns>
    public GameObject CreateResource(GameObject prefab, Transform transformParent)
    {
        GameObject obj = GameObject.Instantiate(prefab, transformParent) as GameObject;
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        return obj;
    }
}
