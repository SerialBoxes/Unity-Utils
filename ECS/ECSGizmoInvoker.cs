#if ENTITIES_INSTALLED
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Unity.Entities;


namespace NoodleKit.ECS{
//Using some hacky? generics stuff to make my own DrawGizmos Event in any system, managed or unmanaged.
//https://discussions.unity.com/t/entity-visualizer-ondrawgizmos/744996/4
public static class GizmoInvoker{
    
    private static List<GizmoSystem> gizmoSystems = new ();
    private static float doneTime;

#if UNITY_EDITOR
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawSystemGizmos(MonoBehaviour obj, GizmoType gizmoType) {
        
        if (Time.time == doneTime) return;
        doneTime = Time.time;
        
        foreach (var gizSys in gizmoSystems){
            try {
                gizSys.DrawGizmosInSystem();
            }
            catch {
                //no gizmo for u
            }
        }

//reflection nonsense I tried. won't ever work unless C# add the ability to invoke MethodInfo types with a ref return type ;-;
        // foreach (var world in World.All) {
        //     foreach (var sysHandle in world.Unmanaged.GetAllUnmanagedSystems(Allocator.Temp)) {
        //         SystemState state = world.Unmanaged.ResolveSystemStateRef(sysHandle);
        //         if (state.DebugName.ToString().Contains(typeof(CameraOrbitSystem).ToString())) {
        //             //MethodInfo method = typeof(WorldUnmanaged).GetMethod("GetUnsafeSystemRef");
        //             //MethodInfo generic = method.MakeGenericMethod(typeof(CameraOrbitSystem));
        //             //ref var comeoooon = ref generic.Invoke(world.Unmanaged, new object[] { sysHandle });
        //             
        //             //ref CameraOrbitSystem system = ref world.Unmanaged.GetUnsafeSystemRef<CameraOrbitSystem>(sysHandle);
        //         }
        //     }
        // }
    }
    #endif

    
    public static void GizmoMeUnmanaged<T>() where T : unmanaged, ISystem, IGizmoDrawer {
        bool isGizmoed = false;
        
        foreach (var gizSys in gizmoSystems) {
            isGizmoed |= gizSys.SameType<T>();
        }

        if (!isGizmoed) {
            gizmoSystems.Add(new GizmoUnmanagedSystem<T>());
        }
    }
    
    public static void GizmoMeManaged<T>() where T : SystemBase, IGizmoDrawer {
        bool isGizmoed = false;
        
        foreach (var gizSys in gizmoSystems) {
            isGizmoed |= gizSys.SameType<T>();
        }

        if (!isGizmoed) {
            gizmoSystems.Add(new GizmoManagedSystem<T>());
        }
    }
    
    public static void UnGizmoMe<T>() where T : IGizmoDrawer {
        foreach (var gizSys in gizmoSystems) {
            if (gizSys.SameType<T>()) {
                gizmoSystems.Remove(gizSys);
                break;//should only ever be one GizmoSystem for any type any removing elements from the list you're looping over gives me the heeby-jeebies
            }
        } 
    }

    //wrapper class for all GizmoSystemTypes, so I can keep them all in one lil list ^-^
    private abstract class GizmoSystem {
        public abstract void DrawGizmosInSystem();
        public abstract bool SameType<K>() where K : IGizmoDrawer;
    }

    //class that can invoke DrawGizmos on unmanaged systems
    private class GizmoUnmanagedSystem<T>: GizmoSystem where T : unmanaged, ISystem, IGizmoDrawer  {
        public override void DrawGizmosInSystem() {
            World world = World.All[0];
            var systemHandle = world.GetExistingSystem<T>();
            ref T unmanagedSystem = ref world.Unmanaged.GetUnsafeSystemRef<T>(systemHandle);
            unmanagedSystem.DrawGizmos();
        }

        public override bool SameType<K>() {
            return typeof(K) == typeof(T);
        }
    }
    
    //class that can invoke DrawGizmos on managed systems
    private class GizmoManagedSystem<T>: GizmoSystem where T : SystemBase, IGizmoDrawer  {
        public override void DrawGizmosInSystem() {
            World world = World.All[0];
            var managedSystem = world.GetExistingSystemManaged<T>();
            managedSystem.DrawGizmos();
        }
        
        public override bool SameType<K>() {
            return typeof(K) == typeof(T);
        }
    }

    
}

//interface systems must implement if they want to draw gizmos
public interface IGizmoDrawer {
    public void DrawGizmos();
}
}
#endif