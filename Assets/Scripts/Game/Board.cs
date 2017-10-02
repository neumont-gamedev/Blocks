using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] Transform m_sizeMin = null;
    [SerializeField] Transform m_sizeMax = null;

    public Vector3 sizeMin { get { return m_sizeMin.position; } }
    public Vector3 sizeMax { get { return m_sizeMax.position; } }
}
