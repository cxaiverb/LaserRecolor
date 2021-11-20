using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using VRC.UI;
using VRC.UI.Core;
using UIExpansionKit.API;

namespace LaserRecolor
{
    public class LaserRecolor : MelonMod
    {
        private HandDotCursor RightHand;
        private HandDotCursor LeftHand;
        private SpriteRenderer RightDot;
        private SpriteRenderer LeftDot;
        public static MelonPreferences_Category LaserColor;
        public static MelonPreferences_Entry<string> RightSel;
        public static MelonPreferences_Entry<string> RightDesel;
        public static MelonPreferences_Entry<string> RightCursor;
        public static MelonPreferences_Entry<string> LeftSel;
        public static MelonPreferences_Entry<string> LeftDesel;
        public static MelonPreferences_Entry<string> LeftCursor;

        public IEnumerator WaitForUIMan()
        {
            while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null) yield return null;
            while (UIManager.field_Private_Static_UIManager_0 == null) yield return null;
            while (GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(true) == null) yield return null;
            OnUIManInit();
        }

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(WaitForUIMan());
            LaserColor = MelonPreferences.CreateCategory("LaserRecolor");
            RightSel = LaserColor.CreateEntry<string>("RightSelected", "#00d8ff", "Right Hand Selected Color (hex)");
            RightDesel = LaserColor.CreateEntry<string>("RightDeselected", "#00d8ff", "Right Hand Deselected Color (hex)");
            RightCursor = LaserColor.CreateEntry<string>("RightCursor", "#00d8ff", "Right Cursor Color (hex)");
            LeftSel = LaserColor.CreateEntry<string>("LeftSelected", "#00d8ff", "Left Hand Selected Color (hex)");
            LeftDesel = LaserColor.CreateEntry<string>("LeftDeselected", "#00d8ff", "Left Hand Deselected Color (hex)");
            LeftCursor = LaserColor.CreateEntry<string>("LeftCursor", "#00d8ff", "Left Cursor Color (hex)");
            RightSel.OnValueChangedUntyped += UpdateColor;
            RightDesel.OnValueChangedUntyped += UpdateColor;
            RightCursor.OnValueChangedUntyped += UpdateColor;
            LeftSel.OnValueChangedUntyped += UpdateColor;
            LeftDesel.OnValueChangedUntyped += UpdateColor;
            LeftCursor.OnValueChangedUntyped += UpdateColor;
        }

        private void OnUIManInit()
        {
            if (XRDevice.isPresent == false)
            {
                return;
            }
            LeftHand = VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.transform.Find("DotLeftHand").GetComponent<HandDotCursor>(); 
            RightHand = VRCUiCursorManager.field_Private_Static_VRCUiCursorManager_0.transform.Find("DotRightHand").GetComponent<HandDotCursor>();
            LeftDot = LeftHand.GetComponentInChildren<SpriteRenderer>(true);
            RightDot = RightHand.GetComponentInChildren<SpriteRenderer>(true);
            UpdateColor();
        }

        public Color ParseHTMLString(string Parse)
        {
            ColorUtility.TryParseHtmlString(Parse, out var color);
            return color;
        }

        private void UpdateColor()
        {
            RightHand.field_Public_Color_0 = ParseHTMLString(RightDesel.Value); //right hand deselected color
            RightHand.field_Public_Color_1 = ParseHTMLString(RightSel.Value); //right hand selected color
            RightDot.color = ParseHTMLString(RightCursor.Value); //right dot color
            LeftHand.field_Public_Color_0 = ParseHTMLString(LeftDesel.Value); //left hand deselected color
            LeftHand.field_Public_Color_1 = ParseHTMLString(LeftSel.Value); //left hand selected color
            LeftDot.color = ParseHTMLString(LeftCursor.Value); //left dot color
        }
    }
}
