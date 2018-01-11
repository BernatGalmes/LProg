using DataModel;

namespace Practice_2.Classes
{
    public class Student_item
    {
        private student st;
        private degree deg;

        public student St
        {
            get
            {
                return st;
            }

            set
            {
                st = value;
            }
        }

        public degree Deg
        {
            get
            {
                return deg;
            }

            set
            {
                deg = value;
            }
        }

        public Student_item(student st, degree d)
        {
            St = st;
            Deg = d;
            
        }

        public string nameDegree()
        {
            if(Deg == null)
            {
                return "";
            }
            return Deg.code + "-" + Deg.name;
        }

    }
}
