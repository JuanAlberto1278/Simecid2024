using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    /*
     * Clase con el instructivo de lo que tiene que hacer el sql
     * (RECETA)
     */
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> Parameters { get; set; }

        public SqlOperation()
        {
            Parameters = new List<SqlParameter>();
        }

        // Metodo utilitarios para agregar parametros

        public void AddVarcharParam(string paramName, string paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        public void AddIntParam(string paramName, int paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        public void AddDecimalParam(string paramName, decimal paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        public void AddDoubleParam(string paramName, double paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        public void AddDatetimeParam(string paramName, DateTime birthDate)
        {
            Parameters.Add(new SqlParameter(paramName, birthDate));
        }
    }
}

