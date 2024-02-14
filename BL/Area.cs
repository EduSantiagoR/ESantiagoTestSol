using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static List<ML.Area> GetAll()
        {
            List<ML.Area> areas = new List<ML.Area>();
            try
            {
                using(DL.ESantiagoTestSolEntities context = new DL.ESantiagoTestSolEntities())
                {
                    var query = context.AreaGetAll().ToList();
                    if(query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            areas.Add(area);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return areas;
        }
    }
}
