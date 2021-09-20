namespace Automated_AI_Video_Processing.UserInterface.Menus
{
    public static class SettingsMenu
    {
        public static CliMenuItem[] menuItems = new[]
        {
            new CliMenuItem("VEAI Options", VeaiSettingsMenu.menu.DisplayMenu)
        };
        
        public static CliMenu menu = new CliMenu("Settings Menu", menuItems);
    }
}