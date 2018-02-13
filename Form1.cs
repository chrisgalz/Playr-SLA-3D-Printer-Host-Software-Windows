using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;


namespace Playr
{
    public partial class Form1 : Form
    {
        Form2 frm;
        String imagePath;
        int totalLayers;
        int currentLayer;
        int bottomLayers;
        int bottomLayerTime;
        int layerTime;
        int interim;
        System.Windows.Threading.DispatcherTimer dt;
        System.Windows.Threading.DispatcherTimer dt2;

        public Form1()
        {
            InitializeComponent();

            Console.Write("Hello!");

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        String txt16 = "";

        delegate void SetTextCallback(string text);

        private void SetText16(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox16.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText16);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                Console.Write(text);
                this.Invoke(new Action(() => ThreadProcSafe(text)));
               
                this.textBox16.Text = this.textBox16.Text += text;
                this.textBox16.SelectionStart = textBox16.TextLength;
                this.textBox16.ScrollToCaret();
            }
           
        }

        private void ThreadProcSafe(String text)
        {
            StringComparison comp = StringComparison.InvariantCultureIgnoreCase;

            if (text.Contains2("next", comp))
            {
                

            }
            else if (text.Contains2("clearScreen", comp))
            {
                Console.Write("contains clear");
                
            }
            
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            SetText16(serialPort1.ReadExisting().ToString());
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = textBox15.Text;
            serialPort1.Open();

            String serialOpen = (serialPort1.IsOpen)? "Open" : "Closed";

            label9.Text = "Serial Port " + serialOpen;

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Send serial command
            String commandString = textBox17.Text;
            serialPort1.WriteLine(commandString);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Set Down
            String commandString = "direction down";
            serialPort1.WriteLine(commandString);
            String commandString2 = "speed " + textBox18.Text;
            serialPort1.WriteLine(commandString2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Set Up
            String commandString = "direction up";
            serialPort1.WriteLine(commandString);
            String commandString2 = "speed " + textBox18.Text;
            serialPort1.WriteLine(commandString2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Step
            String commandString = "step " + textBox9.Text;
            serialPort1.WriteLine(commandString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Save Settings

            serialPort1.WriteLine("setTankDepth " + textBox1.Text + "\n");
            serialPort1.WriteLine("setCalibrationDistance " + textBox2.Text + "\n");
            serialPort1.WriteLine("setLayerThickness " + textBox4.Text + "\n");
            serialPort1.WriteLine("setLayerTime " + textBox5.Text + "\n");
            serialPort1.WriteLine("setBottomLayers " + textBox7.Text + "\n");
            serialPort1.WriteLine("setBottomLayerTime " + textBox6.Text + "\n");
            serialPort1.WriteLine("setG1Distance " + textBox10.Text + "\n");
            serialPort1.WriteLine("setG1Speed " + textBox11.Text + "\n");
            serialPort1.WriteLine("setG2Distance " + textBox12.Text + "\n");
            serialPort1.WriteLine("setG2Speed " + textBox13.Text + "\n");
            serialPort1.WriteLine("setInterim " + textBox14.Text + "\n");
            serialPort1.WriteLine("totalLayers " + textBox8.Text + "\n");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Calibrate Reset
            serialPort1.WriteLine("resetCalibration");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Print
            serialPort1.WriteLine("print");

            //show first layer

            bottomLayers = Convert.ToInt32(textBox7.Text.ToString());
            bottomLayerTime = Convert.ToInt32(textBox6.Text.ToString());
            layerTime = Convert.ToInt32(textBox5.Text.ToString());
            interim = Convert.ToInt32(textBox14.Text.ToString());

            currentLayer = 0;

            nextLayer(null, null);

        }

        private void nextLayer(object sender, EventArgs e)
        {
            if (dt2 != null) dt2.Stop();
            if (currentLayer < totalLayers)
            {
                int time = (currentLayer < bottomLayers) ? bottomLayerTime : layerTime;
                currentLayer++;
                this.Invoke((MethodInvoker)delegate {
                    progressBar1.Value = (int)(((float)currentLayer / (float)totalLayers) * 1000);
                    label19.Text = currentLayer + " of " + totalLayers;
                });
                frm.nextFrame();

                dt = new System.Windows.Threading.DispatcherTimer();
                dt.Stop();
                dt.Interval = TimeSpan.FromMilliseconds(time);
                dt.Tick += new EventHandler(clearNextScreen);
                dt.Start();
            }

        }

        private void clearNextScreen(object sender, EventArgs e)
        {
            dt.Stop();
            frm.clearPreview();

            dt2 = new System.Windows.Threading.DispatcherTimer();
            dt2.Stop();
            dt2.Interval = TimeSpan.FromMilliseconds(interim+2000);
            dt2.Tick += new EventHandler(nextLayer);
            dt2.Start();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Calibrate
            serialPort1.WriteLine("calibrate");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("testFunction");
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("stopPrint");
            dt.Stop();
            dt2.Stop();
            currentLayer = 999999999;
            frm.clearPreview();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            frm = new Form2(this);
            Screen[] screens = Screen.AllScreens;
            setFormLocation(frm, screens[1]);
            frm.Show();
        }

        private void setFormLocation(Form form, Screen screen)
        {
            form.StartPosition = FormStartPosition.Manual;
            Rectangle bounds = screen.Bounds;
            form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (frm != null) frm.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (frm != null) frm.clearPreview();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK && frm != null)
            {
                imagePath = folderBrowserDialog1.SelectedPath;
                frm.setImagePath(imagePath);
            }
        }

        public void setNumberOfLayers(int layers)
        {
            textBox8.Text = "" + layers;
            label19.Text = "0 of " + layers;
            totalLayers = layers;
            currentLayer = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frm.previousFrame();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            frm.nextFrame();
        }

        private void label19_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public static class StringExtensions
    {
        public static bool Contains2(this String str, String substring,
                                    StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                                "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                            "comp");

            return str.IndexOf(substring, comp) >= 0;
        }
    }
}
