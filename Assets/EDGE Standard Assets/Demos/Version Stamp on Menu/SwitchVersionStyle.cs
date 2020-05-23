namespace Edge.UnityStandard
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class SwitchVersionStyle : MonoBehaviour {

        public UiTextVersion UiTextVersion;

        public void NextStyle()
        {
            int style = (int)UiTextVersion.VersionStyle;
            style++;
            if (style > 2) style = 0;
            UiTextVersion.VersionStyle = (UiTextVersion.VersionFormat)style;
            UiTextVersion.SetVersionText();
        }
    }
}