using System.Linq;
using UnityEngine;

namespace Ren.Attachment
{
    public class AttachmentManager : MonoBehaviour
    {
        [SerializeField] private AttachmentPoint[] _attachmentPoints;

        private void Awake()
        {
            PopulateAttachmentSlots();
        }

        private void PopulateAttachmentSlots()
        {
            _attachmentPoints = GetComponentsInChildren<AttachmentPoint>();
        }

        [Tooltip("Gets the next avalable weapon slot or returns the first slot if all are full")]
        public AttachmentPoint GetNextAttachmentSlot()
        {
            foreach(AttachmentPoint slot in _attachmentPoints) 
            { 
                if(slot.HasAttachment == false)
                    return slot;
            }

            return _attachmentPoints[0];
        }

        [Tooltip("Finds an empty slot of the type specified or null if all are full")]
        public AttachmentPoint FindAttachmentPointOfType<T>() where T : AttachmentPointData
        {
            return _attachmentPoints.FirstOrDefault(i => i.Data.GetType() == typeof(T));
        }

        public void AddAttachmentToSlot(GameObject AttachmentObject)
        {
            AttachmentPoint Attachment = FindAttachmentPointOfType<WeaponAttachmentData>();

            if(AttachmentObject != null) 
                Attachment.AddAttachment(AttachmentObject);
            
        }
    }
}