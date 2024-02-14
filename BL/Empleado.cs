using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static List<ML.Empleado> GetAll()
        {
            List<ML.Empleado> empleados = new List<ML.Empleado>();
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    var query = context.EmpleadoGetAll().ToList();
                    if(query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.Area = new ML.Area();

                            empleado.IdEmpleado = item.IdEmpleado;
                            empleado.Nombre = item.NombreEmpleado;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.FechaNacimiento = item.FechaNacimiento.Value;
                            empleado.Sueldo = item.Sueldo;
                            empleado.Area.IdArea = item.IdArea;
                            empleado.Area.Nombre = item.NombreArea;

                            empleados.Add(empleado);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return empleados;
        }
        public static ML.Empleado GetById(int idEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    var query = context.EmpleadoGetById(idEmpleado).FirstOrDefault();
                    if(query != null)
                    {
                        empleado.Area = new ML.Area();
                        empleado.IdEmpleado = query.IdEmpleado;
                        empleado.Nombre = query.NombreEmpleado;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.FechaNacimiento = query.FechaNacimiento.Value;
                        empleado.Sueldo = query.Sueldo;
                        empleado.Area.IdArea = query.IdArea;
                        empleado.Area.Nombre = query.NombreArea;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return empleado;
        }
        public static bool Add(ML.Empleado empleado)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    int rowsAffected = context.EmpleadoAdd(empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.FechaNacimiento, empleado.Sueldo, empleado.Area.IdArea);
                    correct = rowsAffected > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return correct;
        }
        public static bool Update(ML.Empleado empleado)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    int rowsAffected = context.EmpleadoUpdate(empleado.IdEmpleado, empleado.Nombre, empleado.ApellidoPaterno, empleado.ApellidoMaterno, empleado.FechaNacimiento, empleado.Sueldo, empleado.Area.IdArea);
                    correct = rowsAffected > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return correct;
        }
        public static bool Delete(int idEmpleado)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    int rowsAffected = context.EmpleadoDelete(idEmpleado);
                    correct = rowsAffected > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return correct;
        }
    }
}
