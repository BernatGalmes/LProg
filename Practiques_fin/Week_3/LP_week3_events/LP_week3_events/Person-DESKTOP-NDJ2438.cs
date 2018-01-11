using System;

namespace LP_week3_events
{

    //delegate declaration
    public delegate void NameChangedEvent(Person p, string oldName);

    public class Person
    {
        
        static public event NameChangedEvent NameChanged;

        private String Name;

        public Person (String name)
        {
            this.Name = name;
        }

        public String Name_person {

            set {
                if (value == "Pancuato")
                {
                    throw new TooUglyNameException();
                }
                String old = Name;
                Name = value;
                OnChanged(old);
            }

            get {
                return Name;
            }

        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(String oldName)
        {
            NameChanged?.Invoke(this, oldName);
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
            Console.WriteLine("You have changed the name of {0} to {1}", oldName, p.Name_person);
        }

        public void Detach()
        {
            // Detach the event and delete the list
            Person.NameChanged -= new NameChangedEvent(funcNameChanged);
            P = null;
        }
    }

    public class TooUglyNameException : Exception
    {

    }
}
