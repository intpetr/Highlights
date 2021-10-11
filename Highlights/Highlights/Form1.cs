using Gma.System.MouseKeyHook;
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

namespace Highlights
{
    public partial class Form1 : Form
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        private string session = "";
        private string output = "";
        private Boolean isRunning = false;
        private int momentNumber = 0;
       // private Boolean isSaved = false;
        private Boolean isPaused = false;
        private Boolean firstStart = false;
        private IKeyboardMouseEvents m_GlobalHook;



        private string SessionName = "";

        public Form1()
        {
            
            InitializeComponent();
           // timer.Start();

            m_GlobalHook = Hook.GlobalEvents();
            //m_GlobalHook += onGlobalHookEvent();


            Hook.GlobalEvents().OnCombination(new Dictionary<Combination, Action>
{

    {Combination.FromString("Alt+F9"), () => { startSession(); }},
                
    {Combination.FromString("Alt+F10"), () => { addMoment(); }}
});




        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            //mondjal egy szint pls marko pls halo




        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = timer.Elapsed.ToString();

            saveMoments();






        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "Session name: " + textBox1.Text;
            SessionName = textBox1.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void startSession()
        {
            if (isPaused)
            {
                label4.Text = "Session On";
                isPaused = false;
                timer.Start();
                isRunning = true;
            }
            else
            {
                if (!firstStart)
                {
                    label4.Text = "Session On";
                    isPaused = false;
                    timer.Start();
                    firstStart = true;
                    isRunning = true;
                }
                else
                {
                    label4.Text = "Session Paused";
                    isPaused = true;
                    timer.Stop();
                    
                }

                

            }

            

        }


        private void saveMoments()
        {

            string allMoments = SessionName+"  Time: "+timer.Elapsed;
            allMoments = allMoments + "\n" + output;
            File.WriteAllText(@"C:\Moments\"+SessionName+".txt", allMoments);
        }







        private void addMoment()
        {
            momentNumber++;

            output = output + "\n"+momentNumber+ ": "+timer.Elapsed;
            label3.Text = timer.Elapsed.ToString();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            startSession();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {


                DialogResult dialogResult = MessageBox.Show("Are you sure you want to stop the ongoing session without saving? The data will be lost.", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {


                    timer.Reset();
                   // timer = new System.Diagnostics.Stopwatch();
                    output = "";
                    isRunning = false;
                    momentNumber = 0;
                    textBox1.Text = "";
                    session = "";
                    label4.Text = "No ongoing session";
                    label2.Text = "Session name";

                }
                else if (dialogResult == DialogResult.Cancel)
                {

                }
            }
        }

       
    }
}
