using System;
using Unity.Collections;
using Unity.Entities;

namespace UnityUtils.Entities {
    public static class CleanerUpper {
        
        public static void Cleanup<T, K>(ref SystemState state) where T : unmanaged, ICleanupComponentData, IDisposable where K : IComponentData {
            var queryParams = new EntityQueryBuilder(Allocator.Temp).WithAll<T>().WithNone<K>();
            var query = state.GetEntityQuery(in queryParams);
            if (query.IsEmpty) return;
            var entities = query.ToEntityArray(Allocator.Temp);
            foreach (var e in entities) {
                state.EntityManager.GetComponentData<T>(e).Dispose();
            }
            state.EntityManager.RemoveComponent<T>(query);
        }
        
        public static void Destroy<T>(ref SystemState state) where T : unmanaged, ICleanupComponentData, IDisposable {
            var queryParams = new EntityQueryBuilder(Allocator.Temp).WithAll<T>();
            var query = state.GetEntityQuery(in queryParams);
            if (query.IsEmpty) return;
            var entities = query.ToEntityArray(Allocator.Temp);
            foreach (var e in entities) {
                state.EntityManager.GetComponentData<T>(e).Dispose();
            }
            state.EntityManager.RemoveComponent<T>(query);
        }
        
        public static void BufferCleanup<T, K>(ref SystemState state) where T : unmanaged, ICleanupBufferElementData, IDisposable where K : IComponentData {
            var queryParams = new EntityQueryBuilder(Allocator.Temp).WithAll<T>().WithNone<K>();
            var query = state.GetEntityQuery(in queryParams);
            if (query.IsEmpty) return;
            var entities = query.ToEntityArray(Allocator.Temp);
            foreach (var e in entities) {
                var buffer = state.EntityManager.GetBuffer<T>(e);
                foreach (var element in buffer){
                    element.Dispose();
                }
            }
            state.EntityManager.RemoveComponent<T>(query);
        }
        
        public static void BufferDestroy<T>(ref SystemState state) where T : unmanaged, ICleanupBufferElementData, IDisposable {
            var queryParams = new EntityQueryBuilder(Allocator.Temp).WithAll<T>();
            var query = state.GetEntityQuery(in queryParams);
            if (query.IsEmpty) return;
            var entities = query.ToEntityArray(Allocator.Temp);
            foreach (var e in entities) {
                var buffer = state.EntityManager.GetBuffer<T>(e);
                foreach (var element in buffer){
                    element.Dispose();
                }
            }
            state.EntityManager.RemoveComponent<T>(query);
        }
    }
}