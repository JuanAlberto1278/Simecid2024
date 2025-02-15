using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    /*
     * Clase u objeto que se encargar de comunicarse con la base de datos 
     *para ejecutar sentencias sql en este caso para esta arquitectura 
     *solo tendra la habilidad de ejecutar STORE PROCEDURE  
     *
     *Esta clase implementa el patron de SINGLETON para asegurar que solo existe una unica instancia
     *del SqlDAO en toda la arquitectura,con el objetivo de que este sea el unico objeto de acceso a la 
     *informacion en la base de datos.
     *Para no tener 2 objetos que puedan acceder a la misma base de datos a la vez 
     */
    public class SqlDao
    {
        //Guarda la ruta para llegar al servidor de las Bases de Datos
        private string _connectionString;

        //Paso 1 Singleton: Crear una instancia privada de la misma clase.
        private static SqlDao _instance;

        //Paso 2 Definir el constructor Privado
        private SqlDao()
        {
            //String de conexion  que obtengo de properties
            
            _connectionString = "Data Source = srv-db-carlospoltro202401.database.windows.net;" +
                "Initial Catalog = SimecidDB; Persist Security Info = True;" +
                "User ID=sysman;Password=Cenfotec123!";

        }

        //Paso 3 Definir el metodo que expone la instancia de la clase SqlDao
        //Si el objeto no existe lo creamos
        public static SqlDao GetInstace()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        //Paso 4 Metodo que nos va a permitir conectarnos a la base de datos y ejecutar el StoreProcedure(SP)
        public void ExecuteProcedure(SqlOperation sqlOperation)

        {
            //Aqui estamos definiedo la instancia del objeto de la conexion
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure //hace el store procedure en vez de hacer un query en la bd
                    //Incluye los campos en la tabla
                }
                )
                {
                    //Agregar los parametros a la ejecucion 
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    //Ejecutar la sentencia "contra la base de datos"
                    conn.Open();
                    command.ExecuteNonQuery();//Por que no vamos a esperar una respuesta
                }
            }
        }
        //Metodo para recuper info desde la base de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResults = new List<Dictionary<string, object>>();

            //Aqui estamos definiedo la instancia del objeto de la conexion
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure //hace el store procedure en vez de hacer un query en la bd
                    //Incluye los campos en la tabla
                }
                )
                {
                    //Agregar los parametros a la ejecucion 
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    //Ejecutar la sentencia "contra la base de datos"
                    conn.Open();

                    //Aqui varia el metodo respecto al anterior a partir de este punto 
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var rowDict = new Dictionary<string, object>(); //Por cada fila un diccionario
                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                rowDict[key] = value;
                            }
                            lstResults.Add(rowDict);
                        }
                    }
                }
            }


            return lstResults;

        }
    }
}
