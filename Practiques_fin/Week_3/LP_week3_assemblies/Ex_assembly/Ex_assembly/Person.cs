using System;
//comando para generar dll
//csc /target:library /out:Ex_assembly.DLL Person.cs

namespace Ex_assembly
{

    //delegate declaration
    public delegate void NameChangedEvent(Person p, string oldName);

    public class Person
    {

        static public event NameChangedEvent NameChanged;

        private String name;

        public Person(String name)
        {
            this.name = name;
        }

        public String Name
        {

            set
            {
                if (value == "Pancuoto")
                {
                    throw new TooUglyNameException();
                }
                String old = name;
                name = value;
                OnChanged(old);
            }

            get
            {
                return name;
            }

        }

        public override string ToString()
        {
            return "name: " + this.name;
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(String oldName)
        {
            if (NameChanged != null)
                NameChanged(this, oldName);
        }
    }

    class EventListener
    {
        private Person P;

        public EventListener(Person p)
        {
            P = p;

            // Add "ListChanged" to the Changed event on "List".
            Person.NameChanged += new NameChangedEvent(funcNameChanged);
        }

        // This will be called whenever the list changes.
        private void funcNameChanged(Person p, string oldName)
        {
            Console.WriteLine("You have changed the name of {0} to {1}", oldName, p.Name);
        }

        public void Detach()
        {
            // Detach the event and delete the list
            Person.NameChanged -= new NameChangedEvent(funcNameChanged);
            P = null;
        }
    }

    /// <summary>
    /// Exception when the name is too ugly
    /// </summary>
    public class TooUglyNameException : Exception
    {
        public string msg = "nombre muy feo";

        public override String Message
        {
            get
            {
                return msg;
            }
        }
    }
}
