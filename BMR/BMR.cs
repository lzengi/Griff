using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace BMR
{
    public partial class BMR : Form
    {
        public BMR()
        {
            InitializeComponent();
        }

        private void BMR_Load(object sender, EventArgs e)
        {
            m_panels = new BoardPanel[64];
            m_bitBoard = new BitArray(64);
            m_bitBoard.SetAll(false);            
            whiteSquare = true;
            whiteSquareColor = Color.FromArgb(100,200,195,101);
            blackSquareColor = Color.FromArgb(100,80,150,90);
            currentSquareColor = whiteSquareColor;
            cx = this.ClientRectangle.Left+20;
            cy = this.ClientRectangle.Top+20;
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    cy += 20;
                    cx = this.ClientRectangle.Left+20;
                    whiteSquare = !whiteSquare;
                }
                else
                {
                    cx += 20;
                }
                whiteSquare = !whiteSquare;                
                if (whiteSquare)
                {
                    currentSquareColor = whiteSquareColor;
                }
                else
                {
                    currentSquareColor = blackSquareColor;
                }

                m_panels[i] = new BoardPanel(currentSquareColor,(uint)i);                                         
                m_panels[i].Location = new Point(cx,cy);
                m_panels[i].Change += new BitEventHandler(BMR_Change);
                this.Controls.Add(m_panels[i]);
            }        
        }

        void BMR_Change(object sender, BitEventArgs e)
        {
            //calculate new values   
            ulong result64=0;
            m_bitBoard.Set((int)e.Position, !m_bitBoard[(int)e.Position]);
            for (int i = 0; i < 64; i++)
            {
                result64 += Convert.ToUInt64(m_bitBoard[i]) * ((ulong)Math.Pow(2, i));
            }
            this.tbDecimal.Text = result64.ToString();     
            this.tbHexaDecimal.Text = String.Format("{0:x}", result64);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private BoardPanel[] m_panels;
        private int cx;
        private int cy;
        private bool whiteSquare;
        private Color whiteSquareColor;
        private Color blackSquareColor;
        private Color currentSquareColor;
        private BitArray m_bitBoard;

        public static BitArray ConvertHexToBitArray(string hexData)
        {       
            try
            {
                var int64 = Int64.Parse(hexData, NumberStyles.HexNumber);
                var bytes = BitConverter.GetBytes(int64);
                var bitArray = new BitArray(bytes);
                return bitArray;
            }
            catch (OverflowException)
            {
                return new BitArray(64);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_bitBoard.SetAll(false);
            foreach (BoardPanel panel in m_panels)
            {                
                panel.Clear();
            }
            this.tbHexaDecimal.Clear();
            this.tbDecimal.Clear();
            Clipboard.Clear();
        }      

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, tbDecimal.Text);
        }

        private void tbHexaDecimal_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbHexaDecimal.Text))
            {
                this.m_bitBoard = new BitArray(64);
            }
            else
            {
                this.m_bitBoard = ConvertHexToBitArray(tbHexaDecimal.Text);
            }

            ProcessInput();

            ulong result64 = 0;
            for (int i = 0; i < 64; i++)
            {
                result64 += Convert.ToUInt64(m_bitBoard[i]) * ((ulong)Math.Pow(2, i));
            }
            this.tbDecimal.Text = result64.ToString();
        }

        private void tbDecimal_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbDecimal.Text))
            {
                m_bitBoard = new BitArray(64);
            }
            else
            {
                ulong value = ulong.Parse(tbDecimal.Text);
                this.m_bitBoard = new BitArray(BitConverter.GetBytes((ulong)value));
            }

            ProcessInput();

            ulong result64 = 0;
            for (int i = 0; i < 64; i++)
            {
                result64 += Convert.ToUInt64(m_bitBoard[i]) * ((ulong)Math.Pow(2, i));
            }
            this.tbHexaDecimal.Text = String.Format("{0:x}", result64);
        }      

        private void ProcessInput()
        {
            for (int i = 0; i < 64; i++)
            {
                m_panels[i].IsSelected = m_bitBoard.Get(i);
            }
        }
    }
}