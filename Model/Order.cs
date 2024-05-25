namespace Sample_AP.Model;

public class Order
{
    public Order() { }

    public Order(
    int orderID,
    string customerID,
    int employeeID,
    DateTime orderDate,
    DateTime requiredDate,
    DateTime? shippedDate,
    int shipVia,
    decimal freight,
    string shipName,
    string shipAddress,
    string shipCity,
    string shipRegion,
    string shipPostalCode,
    string shipCountry)
    {
        OrderID = orderID;
        CustomerID = customerID;
        EmployeeID = employeeID;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        ShipVia = shipVia;
        Freight = freight;
        ShipName = shipName;
        ShipAddress = shipAddress;
        ShipCity = shipCity;
        ShipRegion = shipRegion;
        ShipPostalCode = shipPostalCode;
        ShipCountry = shipCountry;
    }
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public int EmployeeID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public int ShipVia { get; set; }
    public decimal Freight { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }
}
