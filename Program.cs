namespace Expense_Tracker_App;

class Program {
    static void Main(string[] args){    
        List<Receipts> data = new () {
            new Receipts { 
                ID = 1,
                Date = new DateTime(2025, 2, 7),
                Category = "Hobbies", 
                Vendor = "Games Inc", 
                Payment = "Card", 
                SubTotal = 41.56m, 
                Total = 44.99m,
                Location = "2322 E Freddy Gonzalez Dr. Edinburg, Tx 78542", 
                Details = "Entry into tournament and card sleeves"
            },
            new Receipts {
                ID = 2,
                Date = new DateTime(2025, 2, 7),
                Category = "Food", 
                Vendor = "World Market", 
                Payment = "Card", 
                SubTotal = 3.99m, 
                Total = 3.99m, 
                Location = "500 N Jackson Rd. Pharr, Tx 78577", 
                Details = "Silo Thai Green curry"
            },
            new Receipts {
                ID = 3,
                Date = new DateTime(2025, 2, 8),
                Category = "Food", 
                Vendor = "Rise and Shine Café", 
                Payment = "Card", 
                SubTotal = 4.95m,
                Total = 5.36m, 
                Location = "4001 N 23rd St. Mcallen, Tx 78504", 
                Details = "16 oz Iced Americano"
            }
        };

        MainApp mainApp = new(data);

        mainApp.RunApp();
    }
}