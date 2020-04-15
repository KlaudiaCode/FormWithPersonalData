using System;
using System.Windows.Forms;
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
            public override string ToString()
            {
                return  this.Name + ", " + this.Surname + ", Birth date: " + this.Date;
            }
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

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string[] lines = File.ReadAllLines(filePath);
                Person[] people = new Person[lines.Length];

                int errorCount = 0;
                int correctCount = lines.Length;

                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        people[i] = JsonConvert.DeserializeObject<Person>(lines[i]);
                        savedUsersListBox.Items.Add(people[i]);

                    }
                    catch
                    {
                        MessageBox.Show("Error in data type. Line: " + (i + 1));
                        errorCount++;
                    }

                }

                MessageBox.Show("Summary: \n -correct count: " + (correctCount - errorCount) + " \n -error count: " + errorCount);
            }
        }
    }
}
