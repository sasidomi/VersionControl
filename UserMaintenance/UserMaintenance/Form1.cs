using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.WriteFile;
            button3.Text = Resource1.Delete;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        BindingList<User> users = new BindingList<User>();

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var i in users)
                {
                    sw.Write(i.ID);
                    sw.Write(';');
                    sw.Write(i.FullName);
                    sw.WriteLine();
                }
            }
        }


        User selectedUser;
        private void button3_Click(object sender, EventArgs e)
        {
            var l = (from u in users
                     where u.ID == selectedUser.ID
                     select u).FirstOrDefault();

            users.Remove(l);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUser = (User)((ListBox)sender).SelectedItem;
        }
    }
}
