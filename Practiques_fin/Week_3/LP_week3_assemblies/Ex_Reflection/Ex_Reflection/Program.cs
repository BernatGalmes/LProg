using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Reflection
{
    class Program
    {
        const String PATH_ASSEMBLY = "C:\\Users\\berna\\OneDrive\\Documentos\\Visual Studio 2015\\Projects\\Ex_Reflection\\Ex_Reflection\\Ex_assembly.dll";

        static void Main(string[] args)
        {
            Asmbly.show_loadedAssemblies();

            //Load an assembly dynamically (Assembly from previous exercise)
            Asmbly assemblyLoaded = new Asmbly(PATH_ASSEMBLY);

            //showing assemblies
            assemblyLoaded.show_assemblyTypes();

            //loading an assembly dinamicaly
            Type t_person = assemblyLoaded.show_type_info("Ex_assembly.Person");
           
            //create an object invoking constructor
            Type[] c_args = new Type[1];
            c_args[0] = typeof(string);
            ConstructorInfo constr = t_person.GetConstructor(c_args);
            Object person = constr.Invoke(new object[1] { "bernat" });

            //get the value of a property of the oject
            PropertyInfo prop_Name = t_person.GetProperty("Name");
            object Name = prop_Name.GetValue(person);

            Console.WriteLine("property called: " + prop_Name.ToString()+" have the value: "+Name.ToString());

            //call tostring method
            MethodInfo method_ts = t_person.GetMethod("ToString");
            String str_persona =(string) method_ts.Invoke(person, new Object[0]);
            Console.WriteLine("ToString method return: " +str_persona);

            Console.ReadLine();
        }
    }

}
