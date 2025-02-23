namespace Expense_Tracker_App;
public class Receipts () {
    public int ID {get; set;}
    public DateTime Date {get; set;}
    public string Category {get; set;} = "";
    public string Vendor {get; set;} = "";
    public string Payment {get; set;} = "";
    public decimal SubTotal {get; set;}
    public decimal Total {get; set;}
    public string Location {get; set;} = "";
    public string Details {get; set;} = "";
}