using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Reflection
{
    class Asmbly
    {

        private Assembly assemb;

        /// <summary>
        /// Load the assembly from the given file
        /// </summary>
        /// <param name="path_dll"> path of the dll file</param>
        public Asmbly(String path_dll)
        {
            assemb = Assembly.LoadFrom(path_dll);
        }

        /// <summary>
        /// Show all the types of the assembly
        /// </summary>
        public void show_assemblyTypes()
        {
            Type[] types = assemb.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine(type);
            }
        }

        public Type show_type_info(String type_name)
        {
            //loading an assembly dinamicaly
            Type t_person = assemb.GetType(type_name);

            //show information about given type
            show_classInfo(t_person);

            return t_person;
        }

        /// <summary>
        /// Show information about the given type
        /// </summary>
        /// <param name="t"></param>
        static void show_classInfo(Type t)
        {
            TypeInfo info = t.GetTypeInfo();

            PropertyInfo[] properties = info.GetProperties();
            MethodInfo[] methods = info.GetMethods();
            ConstructorInfo[] constructors = info.GetConstructors();

            Console.WriteLine("\n-- Constructors: --");
            foreach (ConstructorInfo c in constructors)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("\n-- Properties: --");
            foreach (PropertyInfo p in properties)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\n-- Methods --");
            foreach (MethodInfo m in methods)
            {
                Console.WriteLine(m);
            }
        }

        /// <summary>
        /// Show all loaded assemblies
        /// </summary>
        public static void show_loadedAssemblies()
        {
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

            //show all loaded assemblies
            Console.WriteLine("\n-- Loaded assemblies: --");
            foreach (Assembly a in asms)
            {
                Console.WriteLine(a);
            }
        }
    }
}
