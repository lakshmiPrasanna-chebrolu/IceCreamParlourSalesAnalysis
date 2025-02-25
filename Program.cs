
string filePath = "D:\\InsureProAssessment\\IceCreamParlourSalesAnalysis\\Data\\sales-data.txt";
List<SaleInfo> sales = ReadSalesData(filePath);
Dictionary<string, double> monthWiseSales = new();
Dictionary<string, Dictionary<string, int>> monthWiseItemSales = new();
Dictionary<string, Dictionary<string, double>> monthWiseSalesRevenue = new();

double totalSales = 0;
foreach (var sale in sales)
{
    totalSales += sale.Revenue;
    if (!monthWiseSales.ContainsKey(sale.Month))
    {
        monthWiseSales[sale.Month] = 0;
    }
    monthWiseSales[sale.Month] += sale.Revenue;

    if (!monthWiseItemSales.ContainsKey(sale.Month))
        monthWiseItemSales[sale.Month] = new();
    if (!monthWiseItemSales[sale.Month].ContainsKey(sale.ItemName))
        monthWiseItemSales[sale.Month][sale.ItemName] = 0;
    monthWiseItemSales[sale.Month][sale.ItemName] += sale.Quantity;

    if (!monthWiseSalesRevenue.ContainsKey(sale.Month))
        monthWiseSalesRevenue[sale.Month] = new();
    if (!monthWiseSalesRevenue[sale.Month].ContainsKey(sale.ItemName))
        monthWiseSalesRevenue[sale.Month][sale.ItemName] = 0.0;
    monthWiseSalesRevenue[sale.Month][sale.ItemName] += sale.Revenue;

}
Console.WriteLine("Total Sales: " + totalSales);
Console.WriteLine("\nMonth-wise Sales Totals:");
foreach (var month in monthWiseSales)
    Console.WriteLine(month.Key + ": " + month.Value);

Console.WriteLine("\nMost Popular Items Per Month:");
foreach (var month in monthWiseItemSales)
{
    string popularItem = "";
    int maxQty = 0;
    foreach (var item in month.Value)
    {
        if (item.Value > maxQty)
        {
            maxQty = item.Value;
            popularItem = item.Key;
        }
    }
    Console.WriteLine(month.Key + ": " + popularItem + " (" + maxQty + " orders)");
}

Console.WriteLine("\nHighest Revenue Items Per Month:");
foreach (var month in monthWiseSalesRevenue)
{
    string topItem = "";
    double maxRevenue = 0;
    foreach (var item in month.Value)
    {
        if (item.Value > maxRevenue)
        {
            maxRevenue = item.Value;
            topItem = item.Key;
        }
    }
    Console.WriteLine(month.Key + ": " + topItem + " (" + maxRevenue + " INR)");
}

Console.WriteLine("\nOrder Statistics for Most Popular Items:");
foreach (var month in monthWiseItemSales)
{
    string popularItem = "";
    int maxQty = 0;
    foreach (var item in month.Value)
    {
        if (item.Value > maxQty)
        {
            maxQty = item.Value;
            popularItem = item.Key;
        }
    }
    int minOrders = int.MaxValue, maxOrders = 0, totalOrders = 0, count = 0;
    foreach (var sale in sales)
    {
        if (sale.Month == month.Key && sale.ItemName == popularItem)
        {
            minOrders = Math.Min(minOrders, sale.Quantity);
            maxOrders = Math.Max(maxOrders, sale.Quantity);
            totalOrders += sale.Quantity;
            count++;
        }
    }
    double avgOrders = count > 0 ? (double)totalOrders / count : 0;
    Console.WriteLine(month.Key + ": " + popularItem + " - Min: " + minOrders + ", Max: " + maxOrders + ", Avg: " + Math.Round(avgOrders, 2));
}


static List<SaleInfo> ReadSalesData(string filePath)
{
    List<SaleInfo> sales = new();
    bool firstLine = true;
    foreach (var line in File.ReadLines(filePath))
    {
        if (firstLine)
        {
            firstLine = false;
            continue;
        }
        var parts = line.Split(',');
        if (parts.Length != 5) continue;
        string month = parts[0].Trim().Substring(0, 7);
        string itemName = parts[1].Trim();
        double price = double.Parse(parts[2].Trim());
        int quantity = int.Parse(parts[3].Trim());
        double revenue = double.Parse(parts[4].Trim());

        sales.Add(new SaleInfo
        {
            Month = month,
            ItemName = itemName,
            Quantity = quantity,
            Price = price,
            Revenue = revenue
        });
    }
    return sales;

}

class SaleInfo
{
    public string Month;
    public string ItemName;
    public int Quantity;
    public double Price;
    public double Revenue;
}






