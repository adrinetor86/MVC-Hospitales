using System.Data;
using Microsoft.Data.SqlClient;
using MvcCrudPlantilla.Models;

namespace MvcCrudPlantilla.Repository;


// alter view VistaActividadesUser 
//     as 
//     select 
// u.idusuario,
// u.nombre,
// u.apellidos,
// u.email, 
// u.imagen,
// a.nombre as actividad,
// e.fecha_evento,
// i.quiere_ser_capitan,
// c.nombre as curso
// --select *
//     from USUARIOSTAJAMAR as u
//     INNER JOIN CURSOSTAJAMAR AS c ON u.IDCURSO=c.IDCURSO
// INNER JOIN INSCRIPCIONES i ON u.IDUSUARIO = i.id_usuario
// INNER JOIN EVENTO_ACTIVIDADES ea ON i.IdEventoActividad = ea.IdEventoActividad
// INNER JOIN ACTIVIDADES a ON ea.IdActividad = a.id_actividad
// INNER JOIN EVENTOS e ON ea.IdEvento = e.id_evento;
// go

public class RepositoryUsuarios
{

    private DataTable tablaUsuario;
    private DataTable tablaUsuarioV;
    private SqlConnection cn;
    private SqlCommand com;
    private SqlDataReader reader;
    

    public RepositoryUsuarios()
    {
        string sql = "Select * from VistaActividadesUser";
        
        
        string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;User ID=sa;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";

        cn = new SqlConnection(connectionString);
        com = new SqlCommand();
        com.Connection = cn;
        
        SqlDataAdapter ad = new SqlDataAdapter(sql,connectionString);
        tablaUsuario = new DataTable();  

        ad.Fill(tablaUsuario);
    }


    public List<Usuario> GetUsuarios()
    {
        var consulta = from datos in tablaUsuario.AsEnumerable()
            select datos;
        List<Usuario> usuarios = new List<Usuario>();
        
        foreach (var row in consulta)
        {
            Usuario usuario = new Usuario
            {
                IdUsuario = row.Field<int>("IDUSUARIO"),
                Nombre = row.Field<string>("NOMBRE"),
                Apellidos = row.Field<string>("APELLIDOS"),
                Email = row.Field<string>("EMAIL"),
                Imagen = row.Field<string>("IMAGEN"),
            };
            usuarios.Add(usuario);
        }

        return usuarios;
        
    }


    public Usuario GetDatosUsuario(int idUsuario)
    {
        
       var consulta= from datos in tablaUsuario.AsEnumerable()
            where datos.Field<int>("IDUSUARIO")==idUsuario
            select datos;

       if (consulta.Count() == 0)
       {
           return null;
       }
       else
       {
           var row = consulta.First();
           Usuario usuario = new Usuario();
   
           usuario.IdUsuario = row.Field<int>("IDUSUARIO");
           usuario.Nombre = row.Field<string>("NOMBRE");
           usuario.Apellidos = row.Field<string>("APELLIDOS");
           usuario.Email = row.Field<string>("EMAIL");
           usuario.Imagen = row.Field<string>("IMAGEN");
           usuario.Actividad = row.Field<string>("ACTIVIDAD");
           usuario.FechaEvento = row.Field<DateTime>("FECHA_EVENTO");
           usuario.QuiereCapitan = row.Field<Boolean>("QUIERE_SER_CAPITAN");
           usuario.Curso = row.Field<string>("CURSO");
        
           return usuario;   
       }
        
    }
}