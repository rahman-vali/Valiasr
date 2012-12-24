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
            PersonDTO personDto = client.GetPerson(textBox1.Text);
            if (personDto != null)
             button1.Text = personDto.Firstname;
            else
             button1.Text = "not found";
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            PersonServiceClient client = new PersonServiceClient();
            PersonDTO personDto = new PersonDTO
            {

                CustomerId = 10,
                ShobehCode = 1,
                Firstname = "ali",
                Lastname = "ahmadi",
                MelliIdentity = "70",
                HomeAddress = "babol", 
                HomeTelno = "12435" ,
            };
            client.AddPerson(personDto);
            button2.Enabled = true;
        }
    }
}
