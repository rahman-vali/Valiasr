﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplicationClient.PersonServiceReference;

namespace WindowsFormsApplicationClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PersonServiceClient client = new PersonServiceClient();
            PersonDto personDto = client.GetPerson(textBox1.Text);
            button1.Text = personDto.Firstname;
        }
    }
}
