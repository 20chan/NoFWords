using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;

namespace NoFWords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                var sre = new SpeechRecognitionEngine(new CultureInfo("ko-KR"));
                var cmds = new Choices();
                cmds.Add("씨발", "개새끼", "미친", "나쁜놈", "노답", "쓰레기");
                var builder = new GrammarBuilder();
                builder.Append(cmds);
                //builder.AppendWildcard();
                var g = new Grammar(builder);
                
                sre.LoadGrammar(g);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러");
                Application.Exit();
            }
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            notifyIcon1.ShowBalloonTip(0, "욕하지망", $"{e.Result.Text} 이라 욕하면 안됭!!", ToolTipIcon.Warning);
        }
    }
}
