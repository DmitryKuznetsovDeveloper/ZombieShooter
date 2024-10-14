using System;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIExtension
{
    public static class ButtonExtension
    {
        // Добавление анимации при наведении (Highlighted)
        public static void AnimateOnHover(this Button button, Sequence hoverSequence, Ease easeOut, Ease easeIn)
        {
            if (hoverSequence == null) throw new ArgumentNullException(nameof(hoverSequence));

            EventTrigger trigger = GetOrCreateEventTrigger(button);

            AddOrReplaceEventTrigger(trigger, EventTriggerType.PointerEnter, (eventData) =>
            {
                if (button.interactable)
                    hoverSequence.OnComplete(() => hoverSequence.Pause().Complete()).SetEase(easeOut).Restart();
            });

            AddOrReplaceEventTrigger(trigger, EventTriggerType.PointerExit, (eventData) =>
            {
                if (button.interactable)
                    hoverSequence.OnComplete(() => hoverSequence.Pause().Complete()).SetEase(easeIn).PlayBackwards();
            });
        }

        // Добавление анимации нажатия (Pressed)
        public static void AnimateOnClick(this Button button, Sequence clickSequence, Ease easeOut, Action action)
        {
            if (clickSequence == null) throw new ArgumentNullException(nameof(clickSequence));
            if (action == null) throw new ArgumentNullException(nameof(action));

            EventTrigger trigger = GetOrCreateEventTrigger(button);

            AddOrReplaceEventTrigger(trigger, EventTriggerType.PointerClick, (eventData) =>
            {
                if (button.interactable)
                {
                    clickSequence.OnComplete(() =>
                    {
                        clickSequence.Pause().Complete();
                        action.Invoke();
                    }).SetEase(easeOut).Restart();
                }
            });
        }

        // Добавление анимации при выборе (Selected)
        public static void AnimateOnSelected(this Button button, Sequence selectedSequence)
        {
            if (selectedSequence == null) throw new ArgumentNullException(nameof(selectedSequence));

            EventTrigger trigger = GetOrCreateEventTrigger(button);

            AddOrReplaceEventTrigger(trigger, EventTriggerType.Select, (eventData) =>
            {
                if (button.interactable)
                    selectedSequence.OnComplete(() => selectedSequence.Pause().Complete()).Restart();
            });

            AddOrReplaceEventTrigger(trigger, EventTriggerType.Deselect, (eventData) =>
            {
                if (button.interactable)
                    selectedSequence.OnComplete(() => selectedSequence.Pause().Complete()).PlayBackwards();
            });
        }

        // Добавление анимации выключения кнопки (Disabled)
        public static void AnimateOnDisabled(this Button button, Sequence disabledSequence)
        {
            if (disabledSequence == null) throw new ArgumentNullException(nameof(disabledSequence));

            if (!button.interactable)
                disabledSequence.OnComplete(() => disabledSequence.Pause().Complete()).Restart();
        }

        // Метод для отписки от событий
        public static void UnsubscribeAll(this Button button)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();

            if (trigger != null)
                trigger.triggers.Clear();

            button.onClick.RemoveAllListeners();
        }

        // Метод для отписки от конкретных последовательностей
        public static void UnsubscribeSequence(this Button button, Sequence hoverSequence = null, Sequence clickSequence = null, Sequence selectedSequence = null, Sequence disabledSequence = null)
        {
            hoverSequence?.Kill();
            clickSequence?.Kill();
            selectedSequence?.Kill();
            disabledSequence?.Kill();
        }

        // Получение или создание EventTrigger
        private static EventTrigger GetOrCreateEventTrigger(Button button)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = button.gameObject.AddComponent<EventTrigger>();
            
            return trigger;
        }

        // Добавление или замена событий в EventTrigger
        private static void AddOrReplaceEventTrigger(EventTrigger trigger, EventTriggerType eventType, Action<BaseEventData> action)
        {
            EventTrigger.Entry existingEntry = trigger.triggers.Find(entry => entry.eventID == eventType);
            if (existingEntry != null)
            {
                existingEntry.callback.RemoveAllListeners();
                existingEntry.callback.AddListener((eventData) => action(eventData));
            }
            else
            {
                EventTrigger.Entry newEntry = new EventTrigger.Entry { eventID = eventType };
                newEntry.callback.AddListener((eventData) => action(eventData));
                trigger.triggers.Add(newEntry);
            }
        }
    }
}