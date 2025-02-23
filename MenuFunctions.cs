namespace Expense_Tracker_App;
public class MenuFunctions {
    // Returns a value that indicates which menu option is choosen.
    // Continues to run till a proper selection is made.
    public int MainMenu() {
        int selector = -1;
        string readInput;
        bool correctValue = false;

        Console.Clear();
        while (!correctValue) {
            Console.WriteLine("Main Menu");
            Console.WriteLine("".PadLeft(25,'_'));
            Console.WriteLine("0) Exit App");
            Console.WriteLine("1) Show List of Expenses");
            Console.WriteLine("2) Add New Expense");
            Console.WriteLine("3) Remove an Expense");
            Console.WriteLine("".PadLeft(25,'_'));
            Console.WriteLine("Enter a number to select an action, only enter the number");

            readInput = Console.ReadLine() ?? "";
            correctValue = int.TryParse(readInput, out selector);

            if (!correctValue) {
                Console.Write("Please enter a valid character, \npress enter to continue...");
                Console.ReadLine();
                Console.Clear();
                continue;
            }

            if (selector < 0 || selector > 3) {
                Console.Write("Please enter a valid character, \npress enter to continue...");
                Console.ReadLine();
                Console.Clear();
                correctValue = false;
                continue;
            }
        }

        return selector;
    }

    // Shows the list of expenses given some list of data.
    public void ShowExpenses(List<Receipts> _data) {
        Console.Clear();
        Console.WriteLine("\nID, Date, Category, Vendor, Payment, SubTotal, Total, Location, Details");
        Console.WriteLine("".PadLeft(120,'-'));
        for (int i = 0; i < _data.Count; i++) {
            Console.Write($"{_data[i].ID}) ");
            Console.WriteLine($"{_data[i].Date}| {_data[i].Category}| {_data[i].Vendor}| {_data[i].Payment}| {_data[i].SubTotal}| {_data[i].Total}| {_data[i].Location}| {_data[i].Details} ");
            Console.WriteLine("".PadLeft(120,'-'));
        }
    }

    // Returns a null value or object that is to be added to the list of receipts.
    // It will display the receipt as if it were added to the list and DB already
    // but that is not the purpose of this method that will be handled else where.
    public Receipts? AddExpense(List<Receipts> _data) {
        Receipts? newReceipt = new();
        bool correctFormat = false;
        string readInput;
        string[] format =  ["Date (mm/dd/yyyy)", "Category", "Vendor", "Payment", "SubTotal", "Total", "Location", "Details"];
        
        ShowExpenses(_data);
        Console.WriteLine("\nEnter Details of receipt in the following format:");
        
        for (int i = 0; i < 8; i++) {
            Console.Write($"{format[i]}: ");
            readInput = Console.ReadLine() ?? "";
            
            switch (format[i]) {
                case "Date (mm/dd/yyyy)":
                    correctFormat = DateTime.TryParseExact(readInput, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date);

                    if (correctFormat) {
                        newReceipt.Date = date;
                    }
                    else {
                        Console.WriteLine("Invalid date entered, use this format mm/dd/yyyy.");
                        i--;
                    }
                    break;
                case "Category":
                    newReceipt.Category = readInput;
                    break;
                case "Vendor":
                    newReceipt.Vendor = readInput;
                    break;
                case "Payment":
                    newReceipt.Payment = readInput;
                    break;
                case "SubTotal":
                    correctFormat = decimal.TryParse(readInput, out decimal subtotal);
                    if (correctFormat && subtotal > 0) {
                        newReceipt.SubTotal = subtotal;
                    }
                    else {
                        Console.WriteLine("Invalid price entered, please enter real dollar value.");
                        i--;
                    }
                    break;
                case "Total":
                    correctFormat = decimal.TryParse(readInput, out decimal total);
                    if (correctFormat && total > 0) {
                        newReceipt.Total = total;
                    }
                    else {
                        Console.WriteLine("Invalid price entered, please enter real dollar value.");
                        i--;
                    }
                    break;
                case "Location":
                    newReceipt.Location = readInput;
                    break;
                case "Details":
                    newReceipt.Details = readInput;
                    break;
            }

            correctFormat = false;
        } 

        Console.Clear();
        Console.WriteLine("\nReview Receipt details");
        Console.WriteLine("".PadLeft(25,'-'));
        for (int i = 0; i < 8; i++) {
            switch (i) {
                case 0:
                    Console.WriteLine($"{format[i]}: {newReceipt.Date}");
                    break;
                case 1:
                    Console.WriteLine($"{format[i]}: {newReceipt.Category}");
                    break;
                case 2:
                    Console.WriteLine($"{format[i]}: {newReceipt.Vendor}");
                    break;
                case 3:
                    Console.WriteLine($"{format[i]}: {newReceipt.Payment}");
                    break;
                case 4:
                    Console.WriteLine($"{format[i]}: {newReceipt.SubTotal}");
                    break;
                case 5:
                    Console.WriteLine($"{format[i]}: {newReceipt.Total}");
                    break;
                case 6:
                    Console.WriteLine($"{format[i]}: {newReceipt.Location}");
                    break;
                case 7:
                    Console.WriteLine($"{format[i]}: {newReceipt.Details}");
                    break;
            }
        }

        Console.WriteLine("".PadLeft(25,'-'));
        while(!correctFormat) {
            Console.WriteLine("Confirm receipt details, enter y to confirm and n to cancel");
            readInput = Console.ReadLine() ?? "";
            correctFormat = char.TryParse(readInput, out char confrimation);

            if (!correctFormat) {
                Console.WriteLine("Please enter a valid input");
                continue;
            }

            if (confrimation == 'y' || confrimation == 'Y') {
                newReceipt.ID = _data.Count + 1;
                _data.Add(newReceipt);
                Console.Clear();
                ShowExpenses(_data);
                Console.Write("Press enter to return to menu");
                Console.ReadLine();
                Console.Clear();
                break;
            }
            else if (confrimation == 'n' || confrimation == 'N') {
                Console.Clear();
                ShowExpenses(_data);
                Console.Write("Press enter to return to menu");
                Console.ReadLine();
                Console.Clear();
                newReceipt = null;
                break;
            }
            else {
                Console.WriteLine("Please enter a valid input");
                correctFormat = false;
                continue;
            }
        }

        return newReceipt;
    }

    // Returns a null value or object that is to be removed from the list of receipts.
    // It will display the list of receipts as if it has already been removed, but the 
    // actual removal from the list and data base is to be done else where.
    public Receipts? RemoveExpense(List<Receipts> _data) {
        string readInput;
        char confrimation;
        bool correctFormat = false;
        int selector = 0;
        Receipts removedReceipt = new();

        while (!correctFormat) {
            if (_data.Count == 0) {
                Console.WriteLine("No record of receipts, press enter to return to main menu");
                Console.ReadLine();
                return null;
            }

            ShowExpenses(_data);
            Console.WriteLine("\nEnter a number to remove a receipt from the list.");
            readInput = Console.ReadLine() ?? "";
            correctFormat = int.TryParse(readInput, out selector);
            
            if (!correctFormat) {
                Console.WriteLine("Please enter an ID value from the List. \nPress enter to continue");
                Console.ReadLine();
                Console.Clear();
                continue;
            }

            if (selector < 1 || selector > _data.Count) {
                Console.WriteLine("Please enter an ID value from the List. \nPress enter to continue");
                Console.ReadLine();
                Console.Clear();
                correctFormat = false;
                continue;
            }
        }

        correctFormat = false;
        while (!correctFormat) {
            Console.Clear();
            Console.WriteLine($"Receipt number {selector} will be removed from list.");
            Console.WriteLine("".PadLeft(30, '-'));
            Console.WriteLine($"ID: {_data[selector - 1].ID}");
            Console.WriteLine($"Date: {_data[selector - 1].Date}");
            Console.WriteLine($"Category: {_data[selector - 1].Category}");
            Console.WriteLine($"Vendor: {_data[selector - 1].Vendor}");
            Console.WriteLine($"Payment: {_data[selector - 1].Payment}");
            Console.WriteLine($"Subtotal: {_data[selector - 1].SubTotal}");
            Console.WriteLine($"Total: {_data[selector - 1].Total}");
            Console.WriteLine($"Location: {_data[selector - 1].Location}");
            Console.WriteLine($"Details: {_data[selector - 1].Details}");
            Console.WriteLine("".PadLeft(30, '-'));
            Console.WriteLine($"To confirm enter y, to cancel enter n");
            readInput = Console.ReadLine() ?? "";

            correctFormat = char.TryParse(readInput, out confrimation);

            if (!correctFormat) {
                Console.WriteLine("Please enter y or n.\npress enter to continue...");
                Console.ReadLine();
                continue;
            }
            else if (confrimation == 'y' || confrimation == 'Y') {
                removedReceipt = _data[selector - 1];
                _data.RemoveAt(selector - 1);
                break;
            }
            else if (confrimation == 'n' || confrimation == 'N') {
                ShowExpenses(_data);
                Console.WriteLine($"Press enter to return to continue.");
                Console.ReadLine();
                return null;
            }
            else {
                Console.WriteLine("Please enter y or n.\npress enter to continue...");
                Console.ReadLine();
                correctFormat = false;
                continue;
            }
        }

        for (int i = 0; i < _data.Count; i++) {
            _data[i].ID = i + 1;
        }

        ShowExpenses(_data);
        Console.WriteLine($"Press enter to return to continue.");
        Console.ReadLine();
        return removedReceipt;
    }

    // This method will handle showing that the app is being closed and all changes to the 
    // database should begin from here. 
    // For now the actual use of this method is purely visual and has no impact in the program.
    public void ExitApp() {
        Console.WriteLine("Saving changes to close app...");
        Console.WriteLine("All set press any key to close!");
        Console.ReadKey(); 
        Console.Clear();
    }
}
