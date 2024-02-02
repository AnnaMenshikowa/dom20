using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace domasnyaa1
{
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Contact(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
        public override string ToString()
        {
            return $"{Name} - {PhoneNumber}";
        }
    }
    public partial class Form1 : Form
    {
        private List<Contact> contacts;
        public Form1()
        {
            InitializeComponent();
            contacts = new List<Contact>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;

            Contact contact = new Contact(name, phoneNumber);
            contacts.Add(contact);
            UpdateContactList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (contactListBox.SelectedItem != null)
            {
                Contact selectedContact = (Contact)contactListBox.SelectedItem;
                contacts.Remove(selectedContact);
                UpdateContactList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (contactListBox.SelectedItem != null)
            {
                Contact selectedContact = (Contact)contactListBox.SelectedItem;
                string name = nameTextBox.Text;
                string phoneNumber = phoneNumberTextBox.Text;

                selectedContact.Name = name;
                selectedContact.PhoneNumber = phoneNumber;
                UpdateContactList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchName = searchTextBox.Text;
            foreach (Contact contact in contacts)
            {
                if
                    (contact.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    contactListBox.SelectedItem = contact;
                    return;
                }
            }
            MessageBox.Show("Контакт не найден", "Результат поиска");
        }
        private void UpdateContactList()
        {
            contactListBox.DataSource = null;
            contactListBox.DataSource = contacts;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Сохранить контакты в .txt файл",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    foreach (Contact contact in contacts)
                    {
                        writer.WriteLine($"{contact.Name} {contact.PhoneNumber}");
                    }
                }
            }
            MessageBox.Show("Контакты сохранены в файл .txt!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Сохранить контакты в .csv файл",
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "CSV files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    writer.WriteLine("Name, PhoneNumber");
                    foreach (Contact contact in contacts)
                    {
                        writer.WriteLine($"{contact.Name} {contact.PhoneNumber}");
                    }
                }
            }
            MessageBox.Show("Контакты сохранены в файл .csv!");
        }
    }
    
}
