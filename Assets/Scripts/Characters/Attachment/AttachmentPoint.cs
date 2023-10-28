using System;
using UnityEngine;

namespace Ren.Attachment
{

    public class AttachmentPoint : MonoBehaviour
    {
        public AttachmentPointData Data;

        public event Action<GameObject> OnAttachmentAdded;
        public event Action<GameObject> OnAttachmentRemoved;

        [Header("Attachment")]
        [SerializeField] protected GameObject _attachedObject;

        public bool HasAttachment
        {
            get { return _hasAttachment; }
            private set { _hasAttachment = value; }
        }

        private bool _hasAttachment;

        private void Awake()
        {
            if (_attachedObject != null)
            {
                AddAttachment(_attachedObject);
            }
        }

        public void AddAttachment(GameObject Item)
        {
            if (Item == null)
                return;

            Item.transform.parent = transform;
            Item.transform.position = transform.position;
            Item.transform.rotation = transform.rotation;

            _attachedObject = Item;
            OnAttachmentAdded?.Invoke(Item);
        }


    }
}
