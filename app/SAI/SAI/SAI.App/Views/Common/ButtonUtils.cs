using System;
using System.Drawing;


namespace SAI.SAI.App.Views.Common
{
    internal class ButtonUtils
    {
        public static void SetupButton(Guna.UI2.WinForms.Guna2Button btn, string hoverImageName,
            string defaultImageName)
        {
            SetTransparentStyle(btn);

            Image hoverImage = (Image)Properties.Resources.ResourceManager.GetObject(hoverImageName);
            Image defaultImage = (Image)Properties.Resources.ResourceManager.GetObject(defaultImageName);

            if (hoverImage == null || defaultImage == null)
                throw new ArgumentException("이미지 리소스 이름이 잘못되었습니다.");

            btn.MouseEnter += (s, e) => btn.BackgroundImage = hoverImage;
            btn.MouseLeave += (s, e) => btn.BackgroundImage = defaultImage;
            btn.BackgroundImage = defaultImage;
        }
        public static void SetTransparentStyle(Guna.UI2.WinForms.Guna2Button btn)
        {
            btn.BackColor = Color.Transparent;
            btn.PressedColor = Color.Transparent;
            btn.CheckedState.FillColor = Color.Transparent;
            btn.DisabledState.FillColor = Color.Transparent;
            btn.HoverState.FillColor = Color.Transparent;
        }
    }
}
