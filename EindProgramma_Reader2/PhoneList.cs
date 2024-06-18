namespace PhoneSharp;
public class Phones
{
    public int id;
    public string brand;
    public string model;
    public int memory;
    public double price;
    public int stock;
    public static List <Phones> PhoneList = new List <Phones>();
    
    public Phones(int id, string brand, string model, int memory, double price, int stock)
    {
        this.id = id;
        this.brand = brand;
        this.model = model;
        this.memory = memory;
        this.price = price;
        this.stock = 500;

    }
    public Phones()
    {
        
    }
    public static void BigList()
    {
        PhoneList.Add(new Phones(1, "Simsang","HF 410", 16000,129.95, 500));
        PhoneList.Add(new Phones(2,"Pear","XM 600", 32000,224.95, 500));
        PhoneList.Add(new Phones(3,"Hoeowoei", "Z3",8000,79.95, 500));
        PhoneList.Add(new Phones(4,"OneMillion+","3001",16000, 124.95, 500));
        PhoneList.Add(new Phones(5,"UnfairPhone","NXT12",32000,159.05, 500));
        
    }
    
}