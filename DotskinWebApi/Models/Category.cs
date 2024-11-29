namespace DotskinWebApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Code { get; set; }  
    public string NameEt { get; set; }  
    public string NameEn { get; set; } 
    public string NameRu { get; set; }
    
    public string Name { get; set; } // Это свойство для временного хранения переведенного названия

    
    public List<Product> Products { get; set; } = new List<Product>();

}