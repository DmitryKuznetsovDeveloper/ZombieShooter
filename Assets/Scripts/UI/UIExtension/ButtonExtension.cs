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
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

            AddEventTrigger(trigger, EventTriggerType.PointerEnter,(eventData) =>
            {
                if (button.interactable)
                    hoverSequence.OnComplete(() => hoverSequence.Pause().Complete()).SetEase(easeOut)?.Restart();
            });

            AddEventTrigger(trigger, EventTriggerType.PointerExit,(eventData) =>
            {
                if (button.interactable)
                    hoverSequence.OnComplete(() => hoverSequence.Pause().Complete()).SetEase(easeIn)?.PlayBackwards();
            });
        }

        // Добавление анимации нажатия (Pressed)
        public static void AnimateOnClick(this Button button, Sequence clickSequence, Ease easeOut, Action action)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(trigger, EventTriggerType.PointerClick,(eventData) =>
            {
                if (button.interactable)
                {
                    clickSequence.OnComplete(() =>
                        {
                            clickSequence.Pause().Complete();
                            action?.Invoke();
                        })
                        .SetEase(easeOut)?.Restart();
                }
            });
        }

        // Добавление анимации при выборе (Selected)
        public static void AnimateOnSelected(this Button button, Sequence selectedSequence)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(trigger, EventTriggerType.Select,(eventData) =>
            {
                if (button.interactable)
                    selectedSequence.OnComplete(()=> selectedSequence.Pause().Complete())?.Restart();
            });

            AddEventTrigger(trigger, EventTriggerType.Deselect, (eventData) =>
            {
                if (button.interactable)
                    selectedSequence.OnComplete(()=> selectedSequence.Pause().Complete())?.PlayBackwards();
            });
        }

        // Добавление анимации выключения кнопки (Disabled)
        public static void AnimateOnDisabled(this Button button, Sequence disabledSequence)
        {
            if (!button.interactable)
                disabledSequence.OnComplete((() => disabledSequence.Pause().Complete()))?.Restart();
        }
        
        // Метод для отписки от событий
        public static void UnsubscribeAll(this Button button)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();

            // Если EventTrigger существует, очищаем все записи
            if (trigger is not null)
                trigger.triggers.Clear(); // Очищаем все триггеры

            // Удаляем все подписки на события кнопки
            button.onClick.RemoveAllListeners();
        }
        public static void UnsubscribeSequence(this Button button,Sequence hoverSequence = null, Sequence clickSequence = null, Sequence selectedSequence = null, Sequence disabledSequence = null)
        { 
            hoverSequence?.Kill();
            clickSequence?.Kill();
            selectedSequence?.Kill();
            disabledSequence?.Kill();
        }

        // Добавления событий в EventTrigger
        private static void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
            entry.callback.AddListener((eventData) => action(eventData));
            trigger.triggers.Add(entry);
        }
    }
}