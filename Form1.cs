using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaceholderTextBox;
using Newtonsoft.Json;
using System.IO;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public class Adress
        {
            public string StreetName { get; set; }
            public string StreetNumber { get; set; }
            public string City { get; set; }
        }
        public class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Date { get; set; }
            public Adress Adress { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Person person = new Person();

            person.Name = nameTextBox.Text;
            person.Surname = surnameTextBox.Text;
            person.Date = birthDateTimePicker.Value.ToString("dd-MM-yyyy");


            Adress adress = new Adress();

            adress.StreetName = streetTextBox.Text;
            adress.StreetNumber = streetNumTextBox.Text;
            adress.City = cityTextBox.Text;

            person.Adress = adress;


            string json = JsonConvert.SerializeObject(person);


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt";
            sfd.Filter = "txt files (*.txt) |*.txt";
            sfd.OverwritePrompt = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    File.AppendAllText(sfd.FileName, json + "\n");
                    MessageBox.Show("Your data is saved!");

                }
                catch 
                {
                    MessageBox.Show("Error: Your data was not saved! Try again.");
                }
            }

        }
    }
}
