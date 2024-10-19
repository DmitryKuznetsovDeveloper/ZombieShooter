using System.Collections.Generic;
using UI.View;
namespace UI
{
    public sealed class PopupManager
    {
        private Stack<IPopup> _popupStack = new Stack<IPopup>();

        public void ShowPopup(IPopup popup)
        {
            if (_popupStack.Count > 0)
                _popupStack.Peek().Hide();

            _popupStack.Push(popup);
            popup.Show();
        }

        public void CloseCurrentPopup()
        {
            if (_popupStack.Count > 0)
            {
                var currentPopup = _popupStack.Pop();
                currentPopup.Hide();
                
                if (_popupStack.Count > 0)
                    _popupStack.Peek().Show();
            }
        }
    }
}