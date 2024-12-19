using SchoolDB.Data;
using SchoolDB.Views;

namespace SchoolDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the database context
            using (var context = new SchoolDBContext())
            {
                // Initialize the main menu with the database context
                var menu = new MainMenu(context);

                // Display the main menu to the user
                menu.ShowMainMemu();
            }
        }
    }
}
