using Ren.Controller;
using System;
using UnityEngine;

namespace Ren.Attachment
{
    public class InputAttachmentPoint : AttachmentPoint
    {
        [Header("Input")]
        [SerializeField] private InputReceiver _inputReceiver;

        private void Awake()
        {
            if(_inputReceiver == null) 
                throw new Exception("Add " + _inputReceiver.name + "to attachment point");

            OnAttachmentAdded += BindToUseable;
            OnAttachmentRemoved += UnBindToUseable;
        }

        private void BindToUseable(GameObject AttachedObj)
        {
            IUseable UseableObject = AttachedObj.GetComponent<IUseable>();

            if (UseableObject == null)
                return;

            _inputReceiver.ShootInputEvents.OnInputEventStarted += UseableObject.StartUsing;
            _inputReceiver.ShootInputEvents.OnInputEventCanceled += UseableObject.StopUsing;
            _inputReceiver.ShootInputEvents.OnInputEventPerfored += UseableObject.UsingUpdate;
        }

        private void UnBindToUseable(GameObject DetachedObj)
        {
            IUseable UseableObject = DetachedObj.GetComponent<IUseable>();

            if (UseableObject == null)
                return;

            _inputReceiver.ShootInputEvents.OnInputEventStarted -= UseableObject.StartUsing;
            _inputReceiver.ShootInputEvents.OnInputEventCanceled -= UseableObject.StopUsing;
            _inputReceiver.ShootInputEvents.OnInputEventPerfored -= UseableObject.UsingUpdate;
        }
    }
}