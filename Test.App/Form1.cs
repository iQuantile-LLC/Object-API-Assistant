using System.Collections.Generic;
using System.Windows.Forms;
using ObjectAPIAssistant;

namespace Test.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

             ApiAssistant<List<Country>> assistant=new ApiAssistant<List<Country>>("http://battuta.medunes.net/api/");
             
           

        }
    }
}
