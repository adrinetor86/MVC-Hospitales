using System.Data;
using Microsoft.Data.SqlClient;
using MvcCrudPlantilla.Models;

namespace MvcCrudPlantilla.Repository;



// CREATE PROCEDURE SP_PLANTILLA_UPSERT(@hosp_cod as int,@sala_cod as int,@empleado_no as int,@apellido as nvarchar(50),@funcion as nvarchar(50),@turno as nvarchar(50),@salario as int)
// AS
// if((select COUNT(*) from plantilla where EMPLEADO_NO=@empleado_no)>0)
// begin
// 			
//     update plantilla set HOSPITAL_COD=@hosp_cod,SALA_COD=@sala_cod,APELLIDO=@apellido,FUNCION=@funcion,T=@turno,SALARIO=@salario where EMPLEADO_NO=@empleado_no;
// end
// else
// begin
//     insert into plantilla values(@hosp_cod,@sala_cod,@empleado_no,@apellido,@funcion,@turno,@salario);
// end
//     GO

public class RepositoryPlantilla
{

    DataTable tablaPlantilla;
    
    private SqlConnection cn;
    private SqlCommand com;
    private SqlDataReader reader;
    
    public RepositoryPlantilla()
    {
        string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;User ID=sa;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        string sql = "SELECT * FROM PLANTILLA";
        
        
        cn = new SqlConnection(connectionString);
        com = new SqlCommand();
        com.Connection = cn;
        
        SqlDataAdapter ad = new SqlDataAdapter(sql,connectionString);
        tablaPlantilla = new DataTable();

        ad.Fill(tablaPlantilla);
        
        
    }


    public List<Plantilla> GetPlantillas()
    {
        var consulta = from datos in tablaPlantilla.AsEnumerable()
            select datos;
        if (consulta.Count() == 0)
        {
            return null;
        }
        else
        {
            List<Plantilla> plantillas = new List<Plantilla>();
        
            foreach (var row in consulta)
            {
                Plantilla plantilla = new Plantilla
                {
                    Hospital_Cod = row.Field<int>("HOSPITAL_COD"),
                    Sala_Cod = row.Field<int>("SALA_COD"),
                    Empleado_No = row.Field<int>("EMPLEADO_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Funcion = row.Field<string>("FUNCION"),
                    Turno = row.Field<string>("T"),
                    Salario = row.Field<int>("SALARIO"),
                };
                plantillas.Add(plantilla);
            }

            return plantillas;
        }
    }

    public Plantilla FindPlantillaById(int empleado_no)
    {
        var consulta = from datos in tablaPlantilla.AsEnumerable()
            where datos.Field<int>("EMPLEADO_NO")==empleado_no 
            select datos;
        if (consulta.Count() == 0)
        {
            return null;
        }
        
        
        Plantilla plantilla = new Plantilla();
            foreach (var row in consulta)
            {
                plantilla.Hospital_Cod=row.Field<int>("HOSPITAL_COD");
                plantilla.Sala_Cod=row.Field<int>("SALA_COD");
                plantilla.Empleado_No=row.Field<int>("EMPLEADO_NO");
                plantilla.Apellido=row.Field<string>("APELLIDO");
                plantilla.Funcion=row.Field<string>("FUNCION");
                plantilla.Turno=row.Field<string>("T");
                plantilla.Salario=row.Field<int>("SALARIO");
            }
            return plantilla;
        
    }
    
    
    public async Task InsertPlantillaAsync(
        int hosp_cod, int sala_cod, int empleado_no, string apellido,string funcion,string turno,int salario)
    {

        string sql = "SP_PLANTILLA_UPSERT";
            
        com.CommandType = CommandType.StoredProcedure;
        com.CommandText = sql;
        com.Parameters.AddWithValue("@hosp_cod", hosp_cod);
        com.Parameters.AddWithValue("@sala_cod", sala_cod);
        com.Parameters.AddWithValue("@empleado_no", empleado_no);
        com.Parameters.AddWithValue("@apellido", apellido);
        com.Parameters.AddWithValue("@funcion", funcion);
        com.Parameters.AddWithValue("@turno", turno);
        com.Parameters.AddWithValue("@salario", salario);
        
        await cn.OpenAsync();

        
        await com.ExecuteNonQueryAsync();

        await cn.CloseAsync();
        com.Parameters.Clear();
    }
    
    public async Task UpdatePlantillaAsync(
        int hosp_cod, int sala_cod, int empleado_no, string apellido,string funcion,string turno,int salario)
    {

        string sql = "SP_PLANTILLA_UPSERT";
            
        com.CommandType = CommandType.StoredProcedure;
        com.CommandText = sql;
        com.Parameters.AddWithValue("@hosp_cod", hosp_cod);
        com.Parameters.AddWithValue("@sala_cod", sala_cod);
        com.Parameters.AddWithValue("@empleado_no", empleado_no);
        com.Parameters.AddWithValue("@apellido", apellido);
        com.Parameters.AddWithValue("@funcion", funcion);
        com.Parameters.AddWithValue("@turno", turno);
        com.Parameters.AddWithValue("@salario", salario);
        
        await cn.OpenAsync();

        
        await com.ExecuteNonQueryAsync();

        await cn.CloseAsync();
        com.Parameters.Clear();
    }

    public ResumenPlantilla GetPlantillasByFuncion(string funcion)
    {
        var consulta = from datos in this.tablaPlantilla.AsEnumerable()
            where datos.Field<string>("FUNCION") == funcion
            select datos;
        
        if (consulta.Count() == 0)
        {
            ResumenPlantilla resumen = new ResumenPlantilla();
            resumen.MaxSalario = 0;
            resumen.SumSalario = 0;
            resumen.MediaSalario = 0;
            resumen.Plantillas = null;
            return resumen;
        }
        else
        {
           // consulta = consulta.OrderBy(x => x.Field<int>("SALARIO"));

            int maximo = consulta.Max(x => x.Field<int>("SALARIO"));
            int suma= consulta.Sum(x=>x.Field<int>("SALARIO"));
            double media= consulta.Average(x=>x.Field<int>("SALARIO"));

            List<Plantilla> plantillas = new List<Plantilla>();
            foreach (var row in consulta)
            {
                Plantilla plantilla = new Plantilla
                {
                    Hospital_Cod = row.Field<int>("HOSPITAL_COD"),
                    Sala_Cod = row.Field<int>("SALA_COD"),
                    Empleado_No = row.Field<int>("EMPLEADO_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Funcion = row.Field<string>("FUNCION"),
                    Turno = row.Field<string>("T"),
                    Salario = row.Field<int>("SALARIO"),
                };
                plantillas.Add(plantilla);
            }
            ResumenPlantilla resumen = new ResumenPlantilla();
            resumen.MaxSalario = maximo;
            resumen.SumSalario = suma;
            resumen.MediaSalario = media;
            resumen.Plantillas = plantillas;

            return resumen;
        }
    }

    
    
    public List<string> GetFunciones()
    {
        var consulta = (from datos in
                tablaPlantilla.AsEnumerable()
            select datos.Field<string>("FUNCION")).Distinct();

        return consulta.ToList();
    }
    
}