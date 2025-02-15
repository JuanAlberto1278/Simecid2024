using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class DiagnosticManager
    {

        public void Create(Diagnostic diagnostic)
        {
            // Se crea una instancia de el CrudFactory para interactuar con la capa de acceso a datos (DAO)
            var dg = new DiagnosticCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            dg.Create(diagnostic);
        }

        public void Update(Diagnostic diagnostic)
        {
            var dg = new DiagnosticCrudFactory();

            // Aquí se realizan validaciones 
            //

            //Aqui se ejecuta el metodo del crud
            dg.Update(diagnostic);
        }

        public void Delete(Diagnostic diagnostic)
        {
            var dg = new DiagnosticCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            dg.Delete(diagnostic);
        }

        public List<Diagnostic> RetrieveAll()
        {
            var dg = new DiagnosticCrudFactory();

            return dg.RetrieveAll<Diagnostic>();
        }

        public List<Diagnostic> RetrieveDiagnosticByEmail(string userEmail)
        {
            var pc = new DiagnosticCrudFactory();
            return pc.RetrieveDiagnosticByEmail<Diagnostic>(userEmail);
        }

    }
}
