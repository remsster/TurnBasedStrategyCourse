using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class UnitRagdoll : MonoBehaviour
    {
        [SerializeField] private Transform ragdollRootBone;

        public void Setup(Transform originalRootBone)
        {
            MatchAllChildTransforms(originalRootBone, ragdollRootBone);
            ApplyExplosionToRagdoll(ragdollRootBone, 300f, transform.position, 10f);
        }

        /// <summary>
        /// Match all the transforms from the original unit to the ragdoll
        /// </summary>
        /// <param name="root">Original Unit</param>
        /// <param name="clone">Ragdoll Unit</param>
        private void MatchAllChildTransforms(Transform root, Transform clone)
        {
            foreach(Transform child in root)
            {
                Transform cloneChild = clone.Find(child.name);
                if (cloneChild != null)
                {
                    cloneChild.position = child.position;
                    cloneChild.rotation = child.rotation;
                    MatchAllChildTransforms(child, cloneChild);
                }
            }
        }

        private void ApplyExplosionToRagdoll(Transform root,float explosionForce, Vector3 explosionPosition, float explosionRadius)
        {
            foreach (Transform child in root)
            {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                }

                ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
