using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Week49MiniProject;

MyDbContext Context = new MyDbContext();

string mainMenuOption = "";
bool showMainMenuAgain = true;
string option = "";
bool showAssetMenuAgain = true;
string editOption = "";
bool showEditMenuAgain = true;


List<Asset> Assets = Context.Assets.ToList();


// Main menu 
void ShowMainMenu()
{
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Asset Tracking Menu");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("To Add a New Asset - enter \"N\"");
    Console.WriteLine("To Edit an Asset - enter \"E\"");
    Console.WriteLine("To See Asset List Sorted By Type - enter \"T\"");
    Console.WriteLine("To See Asset List Sorted By Office - enter \"O\"");
    Console.WriteLine("To Exit - enter \"Q\"");
    mainMenuOption = Console.ReadLine();
}

// Asset menu
void ShowAddAssetMenu()
{
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Add New Asset");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Select Type: \"P\" for Phone, \"C\" for Computer or \"Q\" to Exit");
    option = Console.ReadLine();
}

// Edit menu
void ShowEditAssetMenu()
{
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Update or Delete Asset");
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("To Update Asset - enter \"U\"");
    Console.WriteLine("To Delete Asset - enter \"D\"");
    Console.WriteLine("To Exit - enter \"Q\"");
    editOption = Console.ReadLine();
}


// Add New Phone to List
void CreateNewPhone()
{
    Asset Phone1 = new Asset();

    string type = "Phone";

    Console.WriteLine("Enter Phone Brand:");
    string brand = Console.ReadLine();

    Console.WriteLine("Enter Phone Model:");
    string model = Console.ReadLine();

    Console.WriteLine("Enter Purchase Date (MM/DD/YYYY):");
    string purchaseDate = Console.ReadLine();
    Regex dateFormat = new Regex(@"((([0][1-9])|([1][0-2]))\/(([0-2][0-9])|([3][0-1]))\/([1-2][0,1,9][0-9][0-9]))$");
    if (!dateFormat.IsMatch(purchaseDate))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Wrong Date Format");
        Console.ResetColor();
        AddNewAsset();
    }

    Console.WriteLine("Enter Price in USD:");
    double price = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("In which office is the phone in?");
    Console.WriteLine("Enter \"Sw\" for Sweden, \"Sp\" for Spain or \"U\" for USA");
    double localPrice = 0;
    string currency = "";
    string office = Console.ReadLine();
    if (office.ToLower().Trim() == "sw")
    {
        office = "Sweden";
        currency = "SEK";
        localPrice = price * 10.89;
    }
    else if (office.ToLower().Trim() == "sp")
    {
        office = "Spain";
        currency = "EUR";
        localPrice = price * 0.99;
    }
    else if (office.ToLower().Trim() == "u")
    {
        office = "USA";
        currency = "USD";
        localPrice = price;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Not a valid office");
        Console.ResetColor();
        AddNewAsset();
    }

    // Save to database
    Phone1.Type = type;
    Phone1.Brand = brand;
    Phone1.Model = model;
    Phone1.PurchaseDate = purchaseDate;
    Phone1.PurchaseDateTime = Convert.ToDateTime(purchaseDate);
    Phone1.Price = price;
    Phone1.LocalPrice = localPrice;
    Phone1.Currency = currency;
    Phone1.Office = office;
    Context.Assets.Add(Phone1);
    Context.SaveChanges();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{Phone1.Brand} {Phone1.Model} was successfully added!");
    Console.ResetColor();

    ShowAddAssetMenu();
}

// Add New Computer to List
void CreateNewComputer()
{
    Asset Computer1 = new Asset();
    string type = "Computer";

    Console.WriteLine("Enter Computer Brand:");
    string brand = Console.ReadLine();

    Console.WriteLine("Enter Computer Model:");
    string model = Console.ReadLine();

    Console.WriteLine("Enter Purchase Date (MM/DD/YYYY):");
    string purchaseDate = Console.ReadLine();
    Regex dateFormat = new Regex(@"((([0][1-9])|([1][0-2]))\/(([0-2][0-9])|([3][0-1]))\/([1-2][0,1,9][0-9][0-9]))$");
    if (!dateFormat.IsMatch(purchaseDate))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Wrong Date Format");
        Console.ResetColor();
        AddNewAsset();
    }

    Console.WriteLine("Enter Price in USD:");
    double price = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("In which Office is the Computer in?");
    Console.WriteLine("Enter \"Sw\" for Sweden, \"Sp\" for Spain or \"U\" for USA");
    double localPrice = 0;
    string currency = "";
    string office = Console.ReadLine();
    if (office.ToLower().Trim() == "sw")
    {
        office = "Sweden";
        currency = "SEK";
        localPrice = price * 10.89;
    }
    else if (office.ToLower().Trim() == "sp")
    {
        office = "Spain";
        currency = "EUR";
        localPrice = price * 0.99;
    }
    else if (office.ToLower().Trim() == "u")
    {
        office = "USA";
        currency = "USD";
        localPrice = price;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Not a valid office");
        Console.ResetColor();
        AddNewAsset();
    }

    Computer1.Type = type;
    Computer1.Brand = brand;
    Computer1.Model = model;
    Computer1.PurchaseDate = purchaseDate;
    Computer1.PurchaseDateTime = Convert.ToDateTime(purchaseDate);
    Computer1.Price = price;
    Computer1.LocalPrice = localPrice;
    Computer1.Currency = currency;
    Computer1.Office = office;
    Context.Assets.Add(Computer1);
    Context.SaveChanges();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{Computer1.Brand} {Computer1.Model} was successfully added!");
    Console.ResetColor();

    ShowAddAssetMenu();
}


// List with IDs
void ListForEditAsset()
{
    Console.WriteLine("Id".PadRight(6) + "Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(15) + "Local price today");
    Console.WriteLine("--".PadRight(5) + "----".PadRight(15) + "-----".PadRight(15) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(15) + "-----------------");

    List<Asset> editAssetsList = Context.Assets.OrderBy(asset => asset.Id).ToList();

    foreach (Asset asset in editAssetsList)
    {
        Console.WriteLine(asset.Id.ToString().PadRight(6) + asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
    }
}

// Update Asset
void UpdateAsset()
{
    ListForEditAsset();
    Console.WriteLine("Enter the Id for the Asset you want to Update");
    string updateAsset = Console.ReadLine();

    Asset AssetToUpdate = Context.Assets.FirstOrDefault(x => x.Id == Convert.ToInt32(updateAsset));

    Console.WriteLine($"Type: {AssetToUpdate.Type}");
    Console.WriteLine("Update Type? y/n");
    string typeUpdate = Console.ReadLine();

    if (typeUpdate == "y")
    {
        Console.WriteLine("Select Type: \"P\" for Phone, \"C\" for Computer");
        string select = Console.ReadLine();

        if (select.ToLower().Trim() == "p")
        {
            AssetToUpdate.Type = "Phone";
        }
        else if (select.ToLower().Trim() == "c")
        {
            AssetToUpdate.Type = "Computer";
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not a valid option");
            Console.ResetColor();

            ShowEditAssetMenu();
        }
    }

    Console.WriteLine($"Brand: {AssetToUpdate.Brand}");
    Console.WriteLine("Update Brand? y/n");
    string brandUpdate = Console.ReadLine();

    if (brandUpdate == "y")
    {
        Console.WriteLine("Enter Brand: ");
        string brand = Console.ReadLine();
        AssetToUpdate.Brand = brand;
    }

    Console.WriteLine($"Model: {AssetToUpdate.Model}");
    Console.WriteLine("Update Model? y/n");
    string modelUpdate = Console.ReadLine();

    if (modelUpdate == "y")
    {
        Console.WriteLine("Enter Model: ");
        string model = Console.ReadLine();
        AssetToUpdate.Model = model;
    }

    Console.WriteLine($"Price: {AssetToUpdate.Price}");
    Console.WriteLine("Update Price? y/n");
    string priceUpdate = Console.ReadLine();

    if (priceUpdate == "y")
    {
        Console.WriteLine("Enter Price: ");
        double price = Convert.ToDouble(Console.ReadLine());
        AssetToUpdate.Price = price;
    }

    Console.WriteLine($"Office: {AssetToUpdate.Office}");
    Console.WriteLine("Update Office? y/n");
    string officeUpdate = Console.ReadLine();

    if (officeUpdate == "y")
    {
        Console.WriteLine("Select \"Sw\" for Sweden, \"Sp\" for Spain or \"U\" for USA");
        string office = Console.ReadLine();
        if (office.ToLower().Trim() == "sw")
        {
            AssetToUpdate.Office = "Sweden";
            AssetToUpdate.Currency = "SEK";
            AssetToUpdate.LocalPrice = AssetToUpdate.Price * 10.89;
        }
        else if (office.ToLower().Trim() == "sp")
        {
            AssetToUpdate.Office = "Spain";
            AssetToUpdate.Currency = "EUR";
            AssetToUpdate.LocalPrice = AssetToUpdate.Price * 0.99;
        }
        else if (office.ToLower().Trim() == "u")
        {
            AssetToUpdate.Office = "USA";
            AssetToUpdate.Currency = "USD";
            AssetToUpdate.LocalPrice = AssetToUpdate.Price;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not a valid office");
            Console.ResetColor();
        }
    }


    // Save Changes
    Context.Assets.Update(AssetToUpdate);
    Context.SaveChanges();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{AssetToUpdate.Brand} {AssetToUpdate.Model} was successfully updated!");
    Console.ResetColor();

    ShowEditAssetMenu();
}


// Delete Asset
void DeleteAsset()
{
    ListForEditAsset();
    Console.WriteLine("Enter the Id for the Asset you want to Delete");
    string deleteAsset = Console.ReadLine();

    Asset RemoveAsset = Context.Assets.FirstOrDefault(x => x.Id == Convert.ToInt32(deleteAsset));

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Do you want to delete {RemoveAsset.Brand} {RemoveAsset.Model}? y/n");
    string remove = Console.ReadLine();
    Console.ResetColor();
    
    if (remove == "y")
    {
        Context.Assets.Remove(RemoveAsset);
        Context.SaveChanges();

        ListForEditAsset();

        ShowEditAssetMenu();
    }
    else
    {
        ShowEditAssetMenu();
    }
}


// List Sorted by Office
void ShowListByOffice()
{
    Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(15) + "Local price today");
    Console.WriteLine("----".PadRight(15) + "-----".PadRight(15) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(15) + "-----------------");

    List<Asset> sortedByOffice = Context.Assets.OrderBy(asset => asset.Office).ThenBy(asset => asset.PurchaseDateTime).ToList();

    foreach (Asset asset in sortedByOffice)
    {
        DateTime today = DateTime.Now;
        DateTime pDate = asset.PurchaseDateTime;
        TimeSpan diff = today - pDate;
        double years = diff.Days / 365.25;

        if (years >= 3.75)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
            Console.ResetColor();
        }
        else if (years >= 3.5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
        }
    }

    ShowMainMenu();
}

// List Sorted by Type
void ShowListByType()
{
    Console.WriteLine("Type".PadRight(15) + "Brand".PadRight(15) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15) + "Currency".PadRight(15) + "Local price today");
    Console.WriteLine("----".PadRight(15) + "-----".PadRight(15) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(15) + "------------".PadRight(15) + "--------".PadRight(15) + "-----------------");

    List<Asset> sortedByType = Context.Assets.OrderBy(asset => asset.Type).ThenBy(asset => asset.PurchaseDateTime).ToList();

    foreach (Asset asset in sortedByType)
    {
        DateTime today = DateTime.Now;
        DateTime pDate = asset.PurchaseDateTime;
        TimeSpan diff = today - pDate;
        double years = diff.Days / 365.25;

        if (years >= 3.75)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
            Console.ResetColor();
        }
        else if (years >= 3.5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine(asset.Type.PadRight(15) + asset.Brand.PadRight(15) + asset.Model.PadRight(15) + asset.Office.PadRight(15) + asset.PurchaseDate.ToString().PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Currency.ToUpper().PadRight(15) + asset.LocalPrice);
        }
    }

    ShowMainMenu();
}


// Asset Menu Options
void AddNewAsset()
{
    ShowAddAssetMenu();

    while (showAssetMenuAgain)
    {
        switch (option.ToLower().Trim())
        {
            case ("p"):
                CreateNewPhone();
                break;

            case ("c"):
                CreateNewComputer();
                break;

            case ("q"):
                MainMenu();
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a valid option");
                Console.ResetColor();
                ShowAddAssetMenu();
                break;
        }
    }
}

// Edit Asset Menu Options
void EditAsset()
{
    ShowEditAssetMenu();

    while (showEditMenuAgain)
    {
        switch (editOption.ToLower().Trim())
        {
            case ("u"):
                UpdateAsset();
                break;

            case ("d"):
                DeleteAsset();
                break;

            case ("q"):
                MainMenu();
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a valid option");
                Console.ResetColor();
                ShowEditAssetMenu();
                break;
        }
    }
}

// Main Menu Options
void MainMenu()
{
    ShowMainMenu();

    while (showMainMenuAgain)
    {
        switch (mainMenuOption.ToLower().Trim())
        {
            case "n":
                showAssetMenuAgain = true;
                AddNewAsset();
                break;
            case "e":
                showEditMenuAgain = true;
                EditAsset();
                break;
            case "o":
                ShowListByOffice();
                break;

            case "t":
                ShowListByType();
                break;

            case "q":
                Environment.Exit(0);
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a valid option");
                Console.ResetColor();
                ShowMainMenu();
                break;
        }
    }
}


// Call the Main Menu the first time
MainMenu();



class Asset
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }
    public string PurchaseDate { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; }
    public double LocalPrice { get; set; }

}

class Phone : Asset
{
    public Phone(string type, string brand, string model, string office, string purchaseDate, DateTime purchaseDateTime, double price, string currency, double localPrice)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        PurchaseDate = purchaseDate;
        PurchaseDateTime = purchaseDateTime;
        Price = price;
        Currency = currency;
        LocalPrice = localPrice;
    }
}

class Computer : Asset
{
    public Computer(string type, string brand, string model, string office, string purchaseDate, DateTime purchaseDateTime, double price, string currency, double localPrice)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        PurchaseDate = purchaseDate;
        PurchaseDateTime = purchaseDateTime;
        Price = price;
        Currency = currency;
        LocalPrice = localPrice;
    }
}

