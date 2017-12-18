using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Matching_Game
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        //sound players. SoundPlayer Objects are initialized with .wav files
        SoundPlayer match = new SoundPlayer(@"C: \Users\Michael\Documents\Visual Studio 2015\Projects\Udemy Bignner CSharp projects\Matching game\Matching Game\Ding.wav");
        SoundPlayer win = new SoundPlayer(@"C: \Users\Michael\Documents\Visual Studio 2015\Projects\Udemy Bignner CSharp projects\Matching game\Matching Game\Win.wav");


        //random object
        Random random = new Random();
        //list of strings to be put into the form lables to create icons.
        List<string> icons = new List<string>()
        {
            "L", "L","Z","Z","K","K","R","R","G","G","A","A",
            "w","w","x","x"
        };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            //loops through each control object, the labels, in the table layout panel.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //create lable object and assign it to the current control in loop
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    //generates random numbmber and uses it to pull a char out of the 
                    //icon list and assigns it to a label
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    //hides the icons by chinging the foreground color to the background color
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }

        }

        private void label_click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;//prevents clicks while timer is running

            Label clickedLabel = sender as Label;
            if (clickedLabel !=null)
            {
                //if label has been clicked allready. exit method, do nothing
                if (clickedLabel.ForeColor == Color.Black)
                    return; //stop exicuteing the code

                //if label has been clicked, reveal icon
                //clickedLabel.ForeColor = Color.Black;

                //First guess.
                //if the player has not selected a card. reveal icon.
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                //second guess. Both cards are revealed
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                checkForWinner();

                //if match is found first and second clicked are null. 
                //method is exited without calling timer1_tick
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    match.Play();
                    return;

                }
                
                timer1.Start(); //if no match is found start timer
            }
        }

        //Timer is used to display selections. Only called when there is no match.
        //A second passes where both labels are displayed then they are hidden.
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked  = null;
            secondClicked = null;
        }

        private void checkForWinner()
        {
            //Loops through each control in the form 
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                
                if(iconLabel != null) //if icon label has content. Is this Nessassary?
                {
                    //if an icon is hidden exit method
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            win.Play();
            MessageBox.Show("A winner is you!");
            Close();
        }

        private void InitializeSound()
        {
            
        }
    }
}
