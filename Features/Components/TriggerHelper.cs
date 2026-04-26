using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtils.Components {
[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class TriggerHelper : MonoBehaviour {

    /// <summary>
    /// Event that fires when a collider enters this trigger.
    /// Parameter 1 is the first collider on this trigger,
    /// Parameter 2 is the collider that entered this trigger.
    /// </summary>
    public UnityEvent<Collider, Collider> TriggerEnter;
    /// <summary>
    /// Event that fires every physics tick a collider is inside this trigger.
    /// Parameter 1 is the first collider on this trigger,
    /// Parameter 2 is the collider that is inside this trigger.
    /// </summary>
    public UnityEvent<Collider, Collider> TriggerStay;
    /// <summary>
    /// Event that fires when a collider exits this trigger.
    /// Parameter 1 is the first collider on this trigger,
    /// Parameter 2 is the collider that exited this trigger.
    /// </summary>
    public UnityEvent<Collider, Collider> TriggerExit;

    [SerializeField]
    internal bool renderWhenEmpty = true;
    [SerializeField]
    internal bool renderWhenFull = true;

    internal Collider[] myColliders;
    internal List<Collider> collidersInside = new();
    internal float collidersInsideTime = 0f;
    
    private void Awake() {
        GetColliders();
    }

    int componentCount = 0;
    private void Update() {
        if (collidersInside.Count > 0) {
            collidersInsideTime += Time.deltaTime;
        }else {
            collidersInsideTime = 0f;
        }
        
        //a bunch of nonsense to try and detect when new colliders are added while also not being laggy
#if UNITY_EDITOR
        if (Selection.activeGameObject != null && Selection.activeGameObject == this.gameObject) {
            int newCount = gameObject.GetComponentCount();
            if (newCount != componentCount) {
                componentCount = newCount;
                GetColliders();
            }
        }
#endif
    }

    /// <summary>
    /// Returns a list of every detected collider inside of this trigger!
    /// </summary>
    public List<Collider> GetCollidersInside() {
        return collidersInside;
    }
    
    /// <summary>
    /// Returns the number of detected colliders inside of this trigger!
    /// </summary>
    public int GetCollidersInsideCount() {
        return collidersInside.Count;
    }

    /// <summary>
    /// Returns the amount of time at least one collider has been inside of this trigger
    /// </summary>
    public float GetCollidersInsideTime() {
        return collidersInsideTime;
    }

    internal void GetColliders() {
        myColliders = GetComponents<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        collidersInside.Add(other);
        TriggerEnter.Invoke(myColliders[0],other);
    }

    private void OnTriggerStay(Collider other) {
        TriggerStay.Invoke(myColliders[0],other);
    }

    private void OnTriggerExit(Collider other) {
        if (collidersInside.Contains(other)) collidersInside.Remove(other);
        TriggerExit.Invoke(myColliders[0],other);
    }
}

#if UNITY_EDITOR
public class TriggerHelperGizmos : Editor {
    //Gizmo types: https://docs.unity3d.com/ScriptReference/GizmoType.html
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    static void DrawMyGizmos(TriggerHelper script, GizmoType type){//you can call this anything
        Gizmos.matrix = script.transform.localToWorldMatrix;

        if (script.collidersInside.Count > 0) {
            if (!script.renderWhenFull) return;
            Gizmos.color = Color.green;
        }
        else {
            if (!script.renderWhenEmpty) return;
            Gizmos.color = Color.grey;
        }

        if (script.myColliders is null) {
            script.GetColliders();
        }

        foreach (Collider c in script.myColliders) {
            if (c != null && c.isTrigger) {
                DrawCollider(c);
            }
        }
    }

    static void DrawCollider(Collider collider) {
        if (collider is BoxCollider) {
            BoxCollider box = collider as BoxCollider;
            Gizmos.DrawWireCube(box.center,box.size);
        }else if (collider is SphereCollider) {
            SphereCollider sphere = collider as SphereCollider;
            Gizmos.DrawWireSphere(sphere.center,sphere.radius);
        }else if (collider is CapsuleCollider) {
            CapsuleCollider cap = collider as CapsuleCollider;
            Bounds bounds = cap.bounds;
            Gizmos.DrawWireCube(bounds.center-collider.transform.position, bounds.size);
        }else if (collider is MeshCollider) {
            MeshCollider mesh = collider as MeshCollider;
            if (mesh.convex) {//theres no way to get the convex mesh :(
                Bounds bounds = mesh.bounds;
                Gizmos.DrawWireCube(bounds.center-collider.transform.position, bounds.size);
            }
            else {
                Gizmos.DrawWireMesh(mesh.sharedMesh);
            }
        }
    }
}
#endif
}