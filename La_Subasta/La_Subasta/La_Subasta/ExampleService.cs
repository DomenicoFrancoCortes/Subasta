using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace La_Subasta
{
    [Resource("Subasta")]
    [Role("Admin")]
    public class ExampleService
    {
        public void DoSomething(User user)
        {
            Console.WriteLine("Acceso permitido!");
        }
    }

}
