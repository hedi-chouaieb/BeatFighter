using UnityEngine;
using System.Linq;

namespace Hedi.me
{
    public class LaserHitHandler : MonoBehaviour
    {
        [SerializeField] private Transform hit;

        private Vector3 hitStartingPosition;

        private void Start()
        {
            hitStartingPosition = hit.localPosition;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<EnemyCharacterHandler>())
                hit.position = other.GetContact(0).point;
        }

        void OnCollisionStay(Collision other)
        {
            if (!other.gameObject.GetComponent<EnemyCharacterHandler>()) return;
            ContactPoint[] contacts = new ContactPoint[other.contactCount];
            other.GetContacts(contacts);
            contacts.Min(c => Vector3.Distance(c.point, this.transform.position));
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.GetComponent<EnemyCharacterHandler>())
                hit.localPosition = hitStartingPosition;
        }
    }
}
