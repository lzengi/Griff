using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace BMR
{
    class BoardPanel : Panel
    {
        public BoardPanel(Color backColor, uint position)
        {
            m_backColor = backColor;
            this.BackColor = m_backColor;
            this.IsSelected = false;
            this.BorderStyle = BorderStyle.FixedSingle;            
            this.Width = 20;
            this.Height = 20;
            this.m_position = position;
        }
        protected override void OnClick(EventArgs e)
        {
            if (!this.IsSelected)
            {
                IsSelected = true;
            }
            else
            {
                this.IsSelected = false;
            }
            OnChanged(new BitEventArgs(m_position));
        }

        public void Clear()
        {
            this.BackColor = m_backColor;
            this.IsSelected = false;
        }

        private void OnChanged(BitEventArgs e)
        {
            if (Change != null)
            {
                Change(null, e);
            }
        }
        private Color m_backColor;
        public bool IsSelected
        {
            get
            {
                return m_selected;
            }
            set
            {
                m_selected = value;
                this.BackColor = m_backColor;
                if (m_selected)
                {
                    this.BackColor = Color.Red;
                }
            }
        }
        public event BitEventHandler Change;
        private uint m_position;
        private bool m_selected;
    }

    class BitEventArgs : EventArgs
    {
        public BitEventArgs(uint position)
        {
            m_position = position; 
        }
        private uint m_position;
        public uint Position
        {
            get
            {
                return m_position;
            }
        }
    }

    delegate void BitEventHandler(object sender, BitEventArgs e);    
}
