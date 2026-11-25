using System.Collections.Generic;
using UnityEngine;
using PocketRealms.Companion;

namespace PocketRealms.World
{
    /// <summary>Core room coordinator. Feed validated reachable points from an MRUK adapter after permission is granted.</summary>
    public sealed class RoomWorldBootstrap : MonoBehaviour
    {
        [SerializeField] private CompanionController companion;
        [SerializeField] private Transform playerHead;
        [SerializeField] private float initialSpawnDistance = 1.35f;
        [SerializeField] private float floorOffset = 0.12f;
        private readonly List<Vector3> explorationPoints = new List<Vector3>();
        private int nextPoint;

        private void Start()
        {
            if (companion == null || playerHead == null) return;
            Vector3 forward = Vector3.ProjectOnPlane(playerHead.forward, Vector3.up).normalized;
            if (forward.sqrMagnitude < 0.01f) forward = Vector3.forward;
            Vector3 spawn = playerHead.position + forward * initialSpawnDistance;
            spawn.y = playerHead.position.y - 0.25f;
            companion.transform.position = spawn;
            companion.SetMood(CompanionController.Mood.Curious);
        }

        public void RegisterExplorationPoint(Vector3 worldPosition)
        {
            if (!IsFinite(worldPosition)) return;
            explorationPoints.Add(worldPosition + Vector3.up * floorOffset);
        }

        public void ClearExplorationPoints()
        {
            explorationPoints.Clear();
            nextPoint = 0;
        }

        public void ExploreNextPoint()
        {
            if (companion == null || explorationPoints.Count == 0) return;
            companion.SetDestination(explorationPoints[nextPoint % explorationPoints.Count]);
            nextPoint++;
        }

        public void SpawnCompanionNearPlayer()
        {
            if (companion == null || playerHead == null) return;
            Vector3 forward = Vector3.ProjectOnPlane(playerHead.forward, Vector3.up).normalized;
            if (forward.sqrMagnitude < 0.01f) forward = Vector3.forward;
            companion.transform.position = playerHead.position + forward * initialSpawnDistance;
            companion.SetMood(CompanionController.Mood.Happy);
        }

        private static bool IsFinite(Vector3 v) =>
            !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z) ||
              float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }
}
