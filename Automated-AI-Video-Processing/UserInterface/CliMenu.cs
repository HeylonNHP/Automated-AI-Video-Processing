using System;

namespace Automated_AI_Video_Processing.UserInterface
{
    public class CliMenu
    {
        private CliMenuItem[] menuItemsInternal;
        private string menuTitleInternal;

        public CliMenu(string menuTitle, CliMenuItem[] menuItems)
        {
            menuItemsInternal = menuItems;
            menuTitleInternal = menuTitle;
        }

        public void DisplayMenu()
        {
            Func<int,string,string> getMenuLine = (int index, string caption) =>
            {
                return $"{index}). {caption}";
            };
            
            int choice = -1;
            while (choice > 0 || choice == -1)
            {
                // Write title
                Console.WriteLine($"<--- {menuTitleInternal} --->");
                
                Console.WriteLine(getMenuLine(0,"Quit"));

                for (int i = 0; i < menuItemsInternal.Length; ++i)
                {
                    CliMenuItem cliMenuItem = menuItemsInternal[i];

                    Console.WriteLine(getMenuLine(i+1,cliMenuItem.caption));
                }
                
                Console.Write(">>> ");
                var key = Console.ReadKey();
                Console.WriteLine();

                choice = int.Parse(key.KeyChar.ToString());
                if (choice > 0 && choice <= menuItemsInternal.Length)
                {
                    menuItemsInternal[choice - 1].executeAction();
                }else if (choice == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid choice {choice}");
                }
            }
        }

        public static string getStringInput(string defaultValue = "")
        {
            string defaultValueDisplay = defaultValue == "" ? "" : $" (Default: {defaultValue}) ";
            Console.Write($"{defaultValueDisplay}>>> ");
            string response = Console.ReadLine();
            if (response == "")
            {
                return defaultValue;
            }

            return response;
        }
    }
}