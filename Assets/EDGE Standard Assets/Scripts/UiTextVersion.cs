namespace Edge.UnityStandard
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class UiTextVersion : MonoBehaviour
    {
        public enum VersionFormat
        {
            [Tooltip("just the latest build number")]
            BuildOnly = 0,

            [Tooltip("Full Version as in '[Product Name] v1.2.345'")]
            Full = 1,

            [Tooltip("Only the version numbers, such as '1.2.345'")]
            VersionNumbers = 2
        }

        public VersionFormat VersionStyle;

        public Product Product;

        void Start()
        {
            SetVersionText();
        }

        public void SetVersionText()
        { 
            var text = GetComponent<Text>();
            if (VersionStyle == VersionFormat.BuildOnly)
            {
                text.text = Product.Latest.Build.ToString();
            }

            if (VersionStyle == VersionFormat.Full)
            {
                text.text = Product.Latest.Formal;
            }

            if (VersionStyle == VersionFormat.VersionNumbers)
            {
                text.text = Product.Latest.VersionNumbers;
            }
        }
    }
}