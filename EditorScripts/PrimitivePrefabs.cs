using System;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace NoodleKit {

//[CreateAssetMenu(fileName = "PrimitivePrefabs", menuName = "Scriptable Objects/PrimitivePrefabs")]
public class PrimitivePrefabs : ScriptableObject {

    public GameObject[] prefabs;

    public static PrimitivePrefabs instance;

    void Awake() {
        instance = this;
    }
}
}