using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pyramid
{
    public partial class Form1 : Form
    {
        public string[] clueBank;
        public string[] clues;
        public double timeLeft;
        public bool[] completed;
        public bool[] cluePicked;
        public int cluesLeft;
        public int[] clueNums;
        public Random numGen;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numGen = new Random();
            string[] rawClueBank;
            clueBank = new string[0];
            rawClueBank = System.IO.File.ReadAllLines(@"C:\users\pirt863\pyramidClues.txt");
            for (int x = 0; x < rawClueBank.Length; x++)
            {
                if (rawClueBank[x].First() == '@')
                {

                }
                else
                {
                    clueBank = clueBank.Append(rawClueBank[x]).ToArray();
                }
            }
            //clueBank = clueBank.SkipWhile(clueBank => clueBank.First()=='@').ToArray();
            clues = new string[6];
            cluesLeft = 0;
            label2.Text = "Clues Left: " + clueBank.Length.ToString() + "/" + clueBank.Length.ToString();
            timeLeft = 60;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            completed = new bool[6];
            cluePicked = new bool[clueBank.Length];
            clueNums = new int[6];
            this.KeyPress += Form1_KeyPress;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 's':
                    button7.PerformClick();
                    break;
                case 'p':
                    button9.PerformClick();
                    break;
                case '1':
                    button4.PerformClick();
                    break;
                case '2':
                    button5.PerformClick();
                    break;
                case '3':
                    button6.PerformClick();
                    break;
                case '4':
                    button2.PerformClick();
                    break;
                case '5':
                    button3.PerformClick();
                    break;
                case '6':
                    button1.PerformClick();
                    break;
                case 'e':
                    button10.PerformClick();
                    break;
                case 'c':
                    button8.PerformClick();
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cluesLeft = 0;
            for (int x = 0; x < clueBank.Length; x++)
            {
                if (!cluePicked[x])
                {
                    cluesLeft++;
                }
            }
            if (cluesLeft<6)
            {
                MessageBox.Show("Too few clues left in the bank. Please restart.");
                return;
            }
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            completed = new bool[6];
            for (int x=0;x<6;x++)
            {
                int y = numGen.Next(clueBank.Length);
                while(cluePicked[y] || clueBank[y].First()=='@')
                {
                    y = numGen.Next(clueBank.Length);
                }
                clues[x] = clueBank[y];
                cluePicked[y] = true;
                clueNums[x] = y;
            }
            label2.Text = "Clues Left: " + (cluesLeft-6).ToString() + "/"+clueBank.Length.ToString();
            button4.Text = clues[0];
            timer1.Enabled = true;
            button7.Enabled = false;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
        }

        public void displayScore()
        {
            int score = 0;
            try
            { score += Int32.Parse(button1.Text); }
            catch(Exception e) 
            { score += 0; }
            try
            { score += Int32.Parse(button2.Text); }
            catch (Exception e)
            { score += 0; }
            try
            { score += Int32.Parse(button3.Text); }
            catch (Exception e)
            { score += 0; }
            try
            { score += Int32.Parse(button4.Text); }
            catch (Exception e)
            { score += 0; }
            try
            { score += Int32.Parse(button5.Text); }
            catch (Exception e)
            { score += 0; }
            try
            { score += Int32.Parse(button6.Text); }
            catch (Exception e)
            { score += 0; }
            if (score==1050)
            {
                score = 10000;
            }
            MessageBox.Show("You got " + score.ToString() + " points!!!");
        }

        public void recycleClues()
        {
            if(button5.Text=="")
            {
                cluePicked[clueNums[1]] = false;
            }
            if (button6.Text == "")
            {
                cluePicked[clueNums[2]] = false;
            }
            if (button2.Text == "")
            {
                cluePicked[clueNums[3]] = false;
            }
            if (button3.Text == "")
            {
                cluePicked[clueNums[4]] = false;
            }
            if (button1.Text == "")
            {
                cluePicked[clueNums[5]] = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft -= .1;
            if(Math.Round(timeLeft,1) % 1 > 0)
                label1.Text = "Time Left: " + Math.Round(timeLeft, 1);
            else
                label1.Text = "Time Left: " + Math.Round(timeLeft, 1) + ".0";
            if (completed[0]&& completed[1] && completed[2] && completed[3] && completed[4] && completed[5] )
            {
                button7.Enabled = true;
                timeLeft = 60;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                timer1.Enabled = false;
                displayScore();
            }
            if(timeLeft<=0)
            {
                button7.Enabled = true;
                timeLeft = 60;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                timer1.Enabled = false;
                displayScore();
                recycleClues();
                label1.Text = "Time Left: 0.0";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(button5.Text=="")
            {
                button5.Text = clues[1];
                button4.Text = "50";
                completed[0] = true;
            }
            else if(button6.Text=="")
            {
                button6.Text = clues[2];
                button5.Text = "100";
                completed[1] = true;
            }
            else if(button2.Text == "")
            {
                button2.Text = clues[3];
                button6.Text = "150";
                completed[2] = true;
            }
            else if(button3.Text =="")
            {
                button3.Text = clues[4];
                button2.Text = "200";
                completed[3] = true;
            }
            else if(button1.Text =="")
            {
                button1.Text = clues[5];
                button3.Text = "250";
                completed[4] = true;
            }
            else if(button1.Text != "")
            {
                button1.Text = "300";
                completed[5] = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button5.Text == "")
            {
                button5.Text = clues[1];

            }
            else if (button6.Text == "")
            {
                button6.Text = clues[2];

            }
            else if (button2.Text == "")
            {
                button2.Text = clues[3];

            }
            else if (button3.Text == "")
            {
                button3.Text = clues[4];

            }
            else if (button1.Text == "")
            {
                button1.Text = clues[5];

            }
            else if (button1.Text != "")
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button5.Text == "")
            {
                button5.Text = clues[1];
                button4.Text = "0";
                completed[0] = true;
            }
            else if (button6.Text == "")
            {
                button6.Text = clues[2];
                button5.Text = "0";
                completed[1] = true;
            }
            else if (button2.Text == "")
            {
                button2.Text = clues[3];
                button6.Text = "0";
                completed[2] = true;
            }
            else if (button3.Text == "")
            {
                button3.Text = clues[4];
                button2.Text = "0";
                completed[3] = true;
            }
            else if (button1.Text == "")
            {
                button1.Text = clues[5];
                button3.Text = "0";
                completed[4] = true;
            }
            else if (button1.Text != "")
            {
                button1.Text = "0";
                completed[5] = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!completed[0] && button4.Text !="")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?","Correct?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(x==6)
                {
                    button4.Text = "50";
                }
                else
                {
                    button4.Text = "0";
                }
                timer1.Enabled = true;
                completed[0]=true;
            }
            else if(completed[0])
            {
                MessageBox.Show(clues[0]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!completed[1] && button5.Text != "")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?", "Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == 6)
                {
                    button5.Text = "100";
                }
                else
                {
                    button5.Text = "0";
                }
                timer1.Enabled = true;
                completed[1] = true;
            }
            else if (completed[1])
            {
                MessageBox.Show(clues[1]);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!completed[2] && button6.Text != "")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?", "Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == 6)
                {
                    button6.Text = "150";
                }
                else
                {
                    button6.Text = "0";
                }
                timer1.Enabled = true;
                completed[2] = true;
            }
            else if (completed[2])
            {
                MessageBox.Show(clues[2]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!completed[3] && button2.Text != "")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?", "Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == 6)
                {
                    button2.Text = "200";
                }
                else
                {
                    button2.Text = "0";
                }
                timer1.Enabled = true;
                completed[3] = true;
            }
            else if (completed[3])
            {
                MessageBox.Show(clues[3]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!completed[4] && button3.Text != "")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?", "Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == 6)
                {
                    button3.Text = "250";
                }
                else
                {
                    button3.Text = "0";
                }
                timer1.Enabled = true;
                completed[4] = true;
            }
            else if (completed[4])
            {
                MessageBox.Show(clues[4]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!completed[5] && button1.Text != "")
            {
                timer1.Enabled = false;
                int x = (int)MessageBox.Show("Correct?", "Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (x == 6)
                {
                    button1.Text = "300";
                }
                else
                {
                    button1.Text = "0";
                }
                timer1.Enabled = true;
                completed[5] = true;
            }
            else if (completed[5])
            {
                MessageBox.Show(clues[5]);
            }
        }
    }
}
