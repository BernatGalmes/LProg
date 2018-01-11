using System;

namespace LP_week3_events
{

    //delegate declaration
    public delegate void NameChangedEvent(Person p, string oldName);

    public class Person
    {
        
        static public event NameChangedEvent NameChanged;

        private String name;

        public Person (String name)
        {
            this.name = name;
            
        }

        public String Name {

            set {
                if (value == "Pancuato")
                {
                    throw new TooUglyNameException();
                }
                String old = name;
                name = value;
                this.OnChanged(old);
            }

            get {
                return name;
            }

        }
        
        /// <summary>
        /// Invoke the Changed event; called whenever list changes
        /// </summary>
        /// <param name="oldName"></param>
        protected virtual void OnChanged(String oldName)
        {
            if(!this.Name.Equals(oldName))
                NameChanged?.Invoke(this, oldName);
        }
    }

    class EventListener
    {
        private Person P;

        public EventListener(Person p)
        {
            this.P = p;

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
            this.P = null;
        }
    }

    public class TooUglyNameException : Exception
    {

    }
}
