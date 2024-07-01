using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JSAM.Controllers
{
    [RequireComponent(typeof(EventTrigger))]
    public class OnButtonDownSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundFileObject _clickSound;

        private EventTrigger _eventTrigger;
        private EventTrigger.Entry _onButtonDownEntry;

        protected void OnEnable()
        {
            _eventTrigger = gameObject.GetComponent<EventTrigger>();

            bool isOnDownRegistered = _eventTrigger.triggers.Any(entry =>
               entry.eventID == EventTriggerType.PointerDown &&
               entry.callback.GetPersistentEventCount() > 0 &&
               entry.callback.GetPersistentTarget(0) == this
           );

            if (!isOnDownRegistered)
            {
                _onButtonDownEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown
                };
                _onButtonDownEntry.callback.AddListener(
                    (eventData) => { OnButtonDown(); });

                _eventTrigger.triggers.Add(_onButtonDownEntry);
            }
        }

        protected void OnDisable()
        {
            if (_eventTrigger != null && _onButtonDownEntry != null)
            {
                _eventTrigger.triggers.Remove(_onButtonDownEntry);
            }
        }

        public void OnButtonDown()
        {
            JSAM.AudioManager.PlaySound(_clickSound);
        }
    }
}