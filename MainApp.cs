namespace Expense_Tracker_App;

class MainApp {
    private readonly List<Receipts> _data;
    private MenuFunctions menuFunction = new();
    Receipts? newReceipt = new();
    private bool endApp = false; 
    private int selector = -1;

    public MainApp(List<Receipts> data) {
        _data = data;
    }

    public void RunApp() {
        while (!endApp) {
            Console.Clear();
            selector = menuFunction.MainMenu();

            switch (selector) {
                case 0:
                    menuFunction.ExitApp();
                    endApp = true;
                    break;
                case 1:
                    menuFunction.ShowExpenses(_data);
                    Console.WriteLine("\nPress Enter to return to the main menu.");
                    Console.ReadLine();
                    break;
                case 2:
                    newReceipt = menuFunction.AddExpense(_data);
                    break;
                case 3:
                    newReceipt = menuFunction.RemoveExpense(_data);
                    break;
                default:
                    break;
            }
        }
    }
}