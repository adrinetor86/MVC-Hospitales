namespace MvcCrudPlantilla.Models;

public class ResumenPlantilla
{
   public int MaxSalario { get; set; } 
   public int SumSalario { get; set; } 
   public double MediaSalario { get; set; } 
   
   public List<Plantilla> Plantillas { get; set; }
}