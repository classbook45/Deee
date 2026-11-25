using UnityEngine;

namespace PocketRealms.Companion
{
    public sealed class CompanionController : MonoBehaviour
    {
        public enum Mood { Curious, Happy, Calm, Surprised, Sleeping }
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 0.35f;
        [SerializeField] private float turnSpeed = 5f;
        [SerializeField] private float bobHeight = 0.025f;
        [SerializeField] private float bobFrequency = 2.2f;
        [Header("Visuals")]
        [SerializeField] private Transform visualRoot;
        [SerializeField] private Transform gazeTarget;
        private Vector3 destination;
        private Vector3 baseScale;
        private Vector3 baseLocalPosition;
        private float phase;
        private Mood mood = Mood.Curious;
        public Mood CurrentMood => mood;
        public bool HasDestination { get; private set; }

        private void Awake()
        {
            if (visualRoot == null) visualRoot = transform;
            baseScale = visualRoot.localScale;
            baseLocalPosition = visualRoot.localPosition;
            destination = transform.position;
            phase = Random.Range(0f, 6.28f);
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            if (mood != Mood.Sleeping && HasDestination)
            {
                Vector3 delta = destination - transform.position;
                delta.y = 0f;
                if (delta.sqrMagnitude > 0.01f)
                {
                    Quaternion target = Quaternion.LookRotation(delta.normalized, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * dt);
                    transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * dt);
                }
                else HasDestination = false;
            }
            float frequency = mood == Mood.Sleeping ? 0.7f : bobFrequency;
            float amplitude = mood == Mood.Sleeping ? 0.008f : bobHeight;
            phase += dt * frequency;
            visualRoot.localPosition = baseLocalPosition + Vector3.up * (Mathf.Sin(phase) * amplitude);
            visualRoot.localScale = baseScale * (1f + Mathf.Sin(phase * 0.7f) * 0.018f);
            if (gazeTarget != null && mood != Mood.Sleeping)
            {
                Vector3 look = gazeTarget.position - transform.position;
                if (look.sqrMagnitude > 0.01f)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), dt * 1.2f);
            }
        }

        public void SetDestination(Vector3 worldPosition)
        {
            destination = worldPosition;
            HasDestination = true;
            if (mood == Mood.Sleeping) SetMood(Mood.Curious);
        }

        public void SetMood(Mood nextMood)
        {
            mood = nextMood;
            if (nextMood == Mood.Happy) StartCoroutine(QuickCelebration());
        }

        public void LookAt(Transform target) => gazeTarget = target;

        private System.Collections.IEnumerator QuickCelebration()
        {
            float elapsed = 0f;
            while (elapsed < 0.35f)
            {
                elapsed += Time.deltaTime;
                float bump = 1f + Mathf.Sin(elapsed / 0.35f * Mathf.PI) * 0.22f;
                visualRoot.localScale = baseScale * bump;
                yield return null;
            }
            visualRoot.localScale = baseScale;
        }
    }
}
