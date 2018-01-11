using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataModel;
using LinqToDB;
using Practice_2.Classes;

namespace Practice_2
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int COL_ID = 0;
        const int COL_NAME = 1;
        const int COL_SURNAME = 2;
        const int COL_DEGREE = 3;
        const int COL_GID = 4;

        public List<Student_item> students;

        public MainWindow()
        {
            InitializeComponent();
            this.dg_students.AllowDrop = false;
            
            using (var db = new lppDB())
            {

                students = (from s in db.students
                            from d in db.degrees.Where(d => d.id == s.degree_id).DefaultIfEmpty()
                            select new Student_item(s, d)).ToList<Student_item>();
                List<degree> degrees = (from d in db.degrees select d).ToList();
                degrees.Add(new degree());//allow to select an empty degree
                this.fil_degree.ItemsSource = degrees;
                this.dg_students.ItemsSource = students;
                this.cb_ns_deg.ItemsSource = degrees;
                this.columnDegree.ItemsSource = degrees;

                db.Close();
            }

            dg_students.AllowDrop = false;
            dg_students.CanUserReorderColumns = false;
            
            dg_students.CellEditEnding +=  Dg_students_CellEditEnding;
        }
        
        /// <summary>
        /// Function called when you finish to edit a datagrid cell
        /// Is the function that update the database with the new value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dg_students_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox t = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes

            Student_item st_edit = (Student_item)dg_students.SelectedValue;

            switch (e.Column.DisplayIndex)
            {
                case COL_ID:
                    MessageBox.Show("Are not allowed to change the id!");
                    break;

                case COL_NAME:
                    if (t.Text.Equals(st_edit.St.name))
                    {
                        MessageBox.Show("No changes");
                        return;
                    }

                    using (var db = new lppDB())
                    {
                        db.students.Where(st => st.id == st_edit.St.id).Set(p => p.name, t.Text).Update();
                        db.Close();
                    }
                    MessageBox.Show("The name has been changed from " +
                        st_edit.St.name + " to " + t.Text);
                    
                    break;

                case COL_SURNAME:
                    if (t.Text.Equals(st_edit.St.surname))
                    {
                        MessageBox.Show("No changes");
                        return;
                    }
                    using (var db = new lppDB())
                    {
                        db.students.Where(st => st.id == st_edit.St.id).Set(p => p.surname, t.Text).Update();
                        db.Close();
                    }
                   
                    MessageBox.Show("The surname has been changed from " +
                        st_edit.St.surname + " to " + t.Text);
                    break;

                case COL_DEGREE:
                    ComboBox cb_deg = e.EditingElement as ComboBox;
                    degree d_selected = (degree)cb_deg.SelectedItem;
                    if (cb_deg.SelectedIndex == -1 || d_selected.Equals(st_edit.St.degree))
                    {
                        MessageBox.Show("No changes");
                        return;
                    }

                    this.updateDegree(st_edit.St, d_selected);

                    MessageBox.Show("The degree has been changed from " +
                        st_edit.St.degree + " to " + cb_deg.SelectedItem);
                    break;

                case COL_GID:
                    if (t.Text.Equals(st_edit.St.govern_identifier))
                    {
                        MessageBox.Show("No changes");
                        return;
                    }
                    using (var db = new lppDB())
                    {
                        db.students.Where(st => st.id == st_edit.St.id).Set(p => p.govern_identifier, t.Text).Update();
                        db.Close();
                    }
                    
                    MessageBox.Show("The govern id has been changed from " +
                        st_edit.St.govern_identifier + " to " + t.Text);
                    break;

                default:
                    MessageBox.Show("Wrong action");
                    break;
            }
            
        }

        //update the degree in the database
        private bool updateDegree(student std, degree new_degree)
        {
            long? id_new_deg;
            if (new_degree.isEmpty())
            {
                id_new_deg = null;
            }
            else
            {
                id_new_deg = new_degree.id;
            }

            using (var db = new lppDB())
            {  
                db.students.Where(st => st.id == std.id).Set(p => p.degree_id, id_new_deg).Update();
                db.Close();
            }

            return true;
        }

        /// <summary>
        /// Change the datagrid content with the content specified in the filter boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterResults(object sender, RoutedEventArgs e)
        {
            string f_name, f_surname;
            f_name = this.fil_name.Text;
            f_surname = this.fil_surname.Text;
            degree f_degree = (degree)this.fil_degree.SelectedItem;
            this.dg_students.ItemsSource = students.Where(x=> filterStudent(x, f_name, f_surname, f_degree));
            this.fil_degree.SelectedIndex = -1;
        }

        /// <summary>
        /// Check if the student specified has to be chown in the datagrid
        /// </summary>
        /// <param name="st">student to check</param>
        /// <param name="f_name"></param>
        /// <param name="f_surname"></param>
        /// <param name="f_degree"></param>
        /// <returns></returns>
        private Boolean filterStudent(Student_item st, string f_name, string f_surname, degree f_degree)
        {
            
            if (f_name.Length != 0)
            {
                if (!st.St.name.Contains(f_name))
                {
                    return false;
                }                    
            }

            if (f_surname.Length != 0)
            {
                if (!st.St.surname.Contains(f_surname))
                {
                    return false;
                }
            }

            if (f_degree != null)
            {
                if (f_degree.isEmpty())
                {
                    return st.Deg == null;
                }

                if(st.Deg == null)
                {
                    return false;
                }

                if (!st.Deg.name.Equals(f_degree.name) || !st.Deg.code.Equals(f_degree.code))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Insert a new student in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_submitInsert_Click(object sender, RoutedEventArgs e)
        {
            string name, surname, govern_identifier;
            degree deg;

            name = this.tb_name.Text;
            surname = this.tb_surname.Text;
            govern_identifier = this.tb_gov_id.Text;
            deg = (degree) this.cb_ns_deg.SelectedItem;
            
            student std = new student();
            std.name = name;
            std.surname = surname;
            std.govern_identifier = govern_identifier;
            if(deg != null) 
            {
                if (!deg.isEmpty())//esrit aixi per seguritat
                    std.degree_id = deg.id;
            }

            //insert the new student in the database
            using (var db = new lppDB())
            {
                db.Insert(std);
                students = (from s in db.students
                            from d in db.degrees.Where(d => d.id == s.degree_id).DefaultIfEmpty()
                            select new Student_item(s, d)).ToList<Student_item>();
                db.Close();
            }
           
            //update all the windows items
            this.dg_students.ItemsSource = null;
            this.dg_students.ItemsSource = students;

            //crear the form
            this.tb_name.Text = "";
            this.tb_surname.Text = "";
            this.tb_gov_id.Text = "";
            this.cb_ns_deg.SelectedIndex = -1;

            MessageBox.Show("Item insert success!");
        }
        
    }
}
