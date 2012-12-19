using System;
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
            button1.Enabled = false;
            PersonServiceClient client = new PersonServiceClient();
            PersonDTO personDTO = client.GetPerson(textBox1.Text);
            if (personDTO != null)
             button1.Text = personDTO.Firstname;
            else
             button1.Text = "not found";
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
