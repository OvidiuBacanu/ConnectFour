using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace connect_four
{
    public partial class Form1 : Form
    {
        private Rectangle[] tabla;
        private int[,] matrice;
        private int turn,ct;

        public Form1()
        {
            InitializeComponent();
            this.tabla= new Rectangle[7];
            this.matrice = new int[6, 7];
            this.turn = 1;
            this.ct = 0;
        }
     
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 30, 30, 450, 400);
            for(int i=0;i<6;i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        this.tabla[j] = new Rectangle(64 * j + 40, 40, 40, 365);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 64 * j + 40, 64 * i + 40, 40, 40);
                }
            }
        }

        private int nrcoloana(Point mouse)
        {
            for(int i=0;i<this.tabla.Length;i++)
            {
                if(mouse.X>=this.tabla[i].X && mouse.Y>=this.tabla[i].Y)
                {
                    if(mouse.X<this.tabla[i].X+this.tabla[i].Width && mouse.Y < this.tabla[i].Y + this.tabla[i].Height)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int nrrand(int coloana)
        {
            for(int i=5;i>=0;i--)
            {
                if(this.matrice[i,coloana]==0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int coloana = this.nrcoloana(e.Location);
            if(coloana!=-1)
            {
                int rand = nrrand(coloana);
                if(rand!=-1)
                {
                    this.matrice[rand, coloana] = this.turn;
                    if(this.turn==1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Red, 64 * coloana + 40, 64 * rand + 40, 40, 40);
                    }
                    if(this.turn == 2)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Gold, 64 * coloana + 40, 64 * rand + 40, 40, 40);
                    }
                    int winner = castigator(this.turn);
                    ct++;
                    if(winner!=-1)
                    {
                        MessageBox.Show("PLAYER " + winner.ToString() + " WINS!");
                        Application.Restart();
                    }
                    if(ct==42)
                    {
                        MessageBox.Show("IT'S A TIE!");
                        Application.Restart();
                    }
                    if (this.turn == 1)
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Gold, 600, 300, 70, 70);
                        label1.Visible = false;
                        label3.Visible = true;
                        this.turn = 2;
                    }
                    else
                    {
                        Graphics g = this.CreateGraphics();
                        g.FillEllipse(Brushes.Red, 600, 300, 70, 70);
                        label1.Visible = true;
                        label3.Visible = false;
                        this.turn = 1;
                    }
                        
                }
            }
        }
        private bool egalitate(int nr1, int nr2, int nr3, int nr4)
        {
            if (nr1 == nr2 && nr2 == nr3 && nr3 == nr4 && nr1!=0)
                return true;
            else
                return false;
        }

        private int castigator(int player)
        {
            //vertical
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<7;j++)
                {
                    if (egalitate(matrice[i, j], matrice[i + 1, j], matrice[i + 2, j], matrice[i + 3, j]) == true)
                        return player;
                }
            }
            
            //orizontal
            for(int i=0;i<6;i++)
            {
                for(int j=0;j<4;j++)
                {
                    if (egalitate(matrice[i, j], matrice[i , j+1], matrice[i , j+2], matrice[i , j+3]) == true)
                        return player;
                }
            }

            //diagonala spre dreapta( / )
            for (int i = 0; i<3; i++)
            {
                for (int j = 3; j <7 ; j++)
                {
                    if (egalitate(matrice[i, j], matrice[i+1, j - 1], matrice[i+2, j - 2], matrice[i+3, j - 3]) == true)
                    {
                        return player;
                    }
                }
            }

            //diagonala spre stanga( \ )
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (egalitate(matrice[i, j], matrice[i + 1, j + 1], matrice[i + 2, j + 2], matrice[i + 3, j + 3]) == true)
                        return player;
                }
            }
            return -1;
        }

    }
}