using UnityEngine;

namespace Platformer.UI
{
    public class ObjectiveText : MonoBehaviour
    {
        [SerializeField] private Color _completeColor = Color.green;

        private Color _defaultColor = Color.white;
        private TMPro.TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TMPro.TextMeshProUGUI>();
            if (_text == null)
            {
                Debug.LogError("ObjectiveText: No TextMeshProUGUI component found on this object.");
                enabled = false;
                return;
            }

            // Get the default color of the text
            _defaultColor = _text.color;
        }

        public void UpdateText(string text)
        {
            if (!CheckText(text)) return;
            _text.text = text;
        }

        public void UpdateText(string text, int current)
        {
            if (!CheckText(text)) return;
            _text.text = text + " " + current;
        }

        public void UpdateText(string text, int current, int total)
        {
            if (!CheckText(text)) return;
            _text.text = text + " " + current + "/" + total;
        }

        public void CompleteText()
        {
            if (_text.color == _completeColor) return;
            _text.color = _completeColor;
        }

        public void ResetText()
        {
            if (_text.color == _defaultColor) return;
            _text.color = _defaultColor;
        }

        private bool CheckText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                _text.enabled = false;
                return false;
            }

            return true;
        }
    }
}
