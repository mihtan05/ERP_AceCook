using System.Drawing;

namespace HR_Management.GUI
{
    public static class ThemeColor
    {
        public static Color SidebarBg = Color.FromArgb(15, 23, 42);       // Slate 900
        public static Color SidebarHover = Color.FromArgb(30, 41, 59);    // Slate 800
        public static Color SidebarActive = Color.FromArgb(220, 38, 38);  // Red 600 (match logo)
        public static Color TopBarBg = Color.White;
        public static Color BodyBg = Color.FromArgb(248, 250, 252);       // Slate 50
        public static Color CardBg = Color.White;
        public static Color CardBorder = Color.FromArgb(226, 232, 240);   // Slate 200
        public static Color Divider = Color.FromArgb(241, 245, 249);      // Slate 100

        public static Color Primary = Color.FromArgb(37, 99, 235);        // Royal Blue
        public static Color PrimaryHover = Color.FromArgb(29, 78, 216);   // Blue 700
        public static Color PrimaryLight = Color.FromArgb(239, 246, 255); // Blue 50
        public static Color PrimaryText = Color.FromArgb(29, 78, 216);    // Blue 700

        // Status & Badge Palette (Background + Foreground pairs)
        public static Color Success = Color.FromArgb(16, 185, 129);       // Emerald 500
        public static Color SuccessHover = Color.FromArgb(5, 150, 105);   // Emerald 600
        public static Color SuccessBg = Color.FromArgb(236, 253, 245);    // Emerald 50
        public static Color SuccessLight = Color.FromArgb(236, 253, 245); // Emerald 50
        public static Color SuccessText = Color.FromArgb(4, 120, 87);     // Emerald 700

        public static Color Warning = Color.FromArgb(245, 158, 11);       // Amber 500
        public static Color WarningHover = Color.FromArgb(217, 119, 6);   // Amber 600
        public static Color WarningBg = Color.FromArgb(254, 243, 199);    // Amber 100
        public static Color WarningLight = Color.FromArgb(254, 243, 199); // Amber 100
        public static Color WarningText = Color.FromArgb(180, 83, 9);     // Amber 700

        public static Color Danger = Color.FromArgb(239, 68, 68);         // Rose 500
        public static Color DangerHover = Color.FromArgb(220, 38, 38);    // Rose 600
        public static Color DangerBg = Color.FromArgb(254, 242, 242);     // Rose 50
        public static Color DangerText = Color.FromArgb(185, 28, 28);     // Rose 700

        public static Color Info = Color.FromArgb(2, 132, 199);           // Sky 600
        public static Color InfoHover = Color.FromArgb(3, 105, 161);      // Sky 700
        public static Color InfoBg = Color.FromArgb(240, 249, 255);       // Sky 50
        public static Color InfoText = Color.FromArgb(3, 105, 161);       // Sky 700

        public static Color Purple = Color.FromArgb(139, 92, 246);        // Purple 500
        public static Color PurpleBg = Color.FromArgb(245, 243, 255);     // Purple 50
        public static Color PurpleText = Color.FromArgb(109, 40, 217);    // Purple 700

        public static Color NeutralBg = Color.FromArgb(241, 245, 249);    // Slate 100
        public static Color NeutralText = Color.FromArgb(71, 85, 105);    // Slate 600

        // Text Scale
        public static Color TextPrimary = Color.FromArgb(15, 23, 42);     // Slate 900
        public static Color TextSecondary = Color.FromArgb(100, 116, 139); // Slate 500
        public static Color TextMuted = Color.FromArgb(148, 163, 184);    // Slate 400
        public static Color TextWhite = Color.White;

        // Typography Hierarchy
        public static Font HeaderFont = new Font("Segoe UI", 15F, FontStyle.Bold);
        public static Font SubHeaderFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static Font BodyFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font BodyFontBold = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static Font SmallFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static Font SmallFontBold = new Font("Segoe UI", 9F, FontStyle.Bold);
    }
}
