using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmenControls
{
    public class TextBoxHinter
    {
        private TextBox tb = null;
        private string placeholder = string.Empty;
        private string lastText = "";
        private Color placeForeColor = Color.Gray;
        private Color OrigForeColor;

        public TextBoxHinter(TextBox textBox, string Placeholder, Color ForeColor)
        {
            if (textBox != null && !textBox.IsDisposed && !string.IsNullOrEmpty(Placeholder))
            {
                tb = textBox; placeholder = Placeholder; OrigForeColor = tb.ForeColor; placeForeColor = ForeColor; lastText = tb.Text;
                tb.Text = placeholder;
                tb.ForeColor = placeForeColor;
                tb.GotFocus += tb_GotFocus;
                tb.LostFocus += tb_LostFocus;
                tb.TextChanged += tb_TextChanged;
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            if(tb.Focused) lastText = tb.Text;
        }

        void tb_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = placeholder;
                tb.ForeColor = placeForeColor;
            }
        }

        void tb_GotFocus(object sender, EventArgs e)
        {
            tb.ForeColor = OrigForeColor;
            if (tb.Text == placeholder && lastText != placeholder)
            {
                tb.Text = "";
            }
        }

        /*=========================================================*/

        public static void AddPlaceholder(TextBox textBox, string Placeholder, Color ForeColor)
        {
            var c = new TextBoxHinter(textBox, Placeholder, ForeColor);
        }

        public static void AddPlaceholder(TextBox textBox, string Placeholder)
        {
            var c = new TextBoxHinter(textBox, Placeholder, Color.Gray);
        }
    }
}
