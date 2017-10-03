using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] ObjectPool m_objectPool = null;

    public void CreatePoints(Vector3 position, int value)
    {
        GameObject gameObject = m_objectPool.GetObject();
        gameObject.transform.SetParent(transform);
        Points points = gameObject.GetComponent<Points>();
        points.Initialize(position, value, this);
    }

    public void RemovePoints(Points points)
    {
        m_objectPool.ReturnObject(points.gameObject);
    }
}
