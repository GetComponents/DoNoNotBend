using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Package CurrentPackage;
    public PackageSegment CurrentSegment;
    [SerializeField]
    private Transform BorderHitboxTransform;
    [HideInInspector]
    public float BorderZPos => BorderHitboxTransform.position.z;
    public bool Submitable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
            Instance = this;
            return;
        }
    }
}
