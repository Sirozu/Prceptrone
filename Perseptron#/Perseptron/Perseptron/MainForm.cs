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

namespace Perseptron
{
    public partial class MainForm : Form
    {
        List<NumericUpDown> numUpDowns = new List<NumericUpDown>();

        public MainForm()
        {
            InitializeComponent();
            AddSliceControls();

            //var prc = new Perceptron(2, new int[] { 1000, 1000 }, new Func<double, double>(x => -0.5 + 1.0 / (1 + Math.Exp(-x))), new Func<double, double>(y => y* (1 - y)) );
            var prc = new Perceptron(1, new int[] { 10000 }, new Func<double, double>(x => -0.5 + 1.0 / (1 + Math.Exp(-x))), new Func<double, double>(y => y* (1 - y)) );
            var disampl = new DirectoryInfo("D:/Samples");
            var dipat = new DirectoryInfo("D:/Patterns");

            var sampFiles = disampl.GetFiles();
            var patFiles = dipat.GetFiles();

            long sampleLength = sampFiles[0].Length / sizeof(double);

            var samples = new double[sampFiles.Length/2, sampleLength];
            var patterns = new double[patFiles.Length/2, 10];

            var zeroSample = new double[sampleLength];
            var nineSample = new double[sampleLength];


            for (int k = 0; k < sampFiles.Length/2; k++)
                using (var br = new BinaryReader(sampFiles[k].OpenRead()))
                    for (int i = 0; i < sampleLength; i++)
                        samples[k, i] = br.ReadDouble();

            for (int k = 0; k < patFiles.Length/2; k++)
                using (var br = new BinaryReader(patFiles[k].OpenRead()))
                    for (int i = 0; i < 10; i++)
                        patterns[k, i] = br.ReadInt32();

            for (int i = 0; i< sampleLength; i++)
            {
                zeroSample[i] = samples[0, i];
                nineSample[i] = samples[sampFiles.Length/2 - 1, i];
            }

            prc.TeachBy(samples, patterns, 0.1, 0.9);
            var zero = prc.Classify(zeroSample);
            var nine = prc.Classify(nineSample);
            var a = 0;
        }

        private void AddSliceControls()
        {
            const int step = 30;
            const int maxSliceCount = 6;

            int i = numUpDowns.Count;

            var sliceLabel = new Label();
            sliceLabel.AutoSize = true;
            sliceLabel.Font = new Font(FontFamily.GenericSansSerif, 12);
            sliceLabel.Location = new System.Drawing.Point(30, shadowSlicesLabel.Top + shadowSlicesLabel.Height + 10 + step * i);
            sliceLabel.Name = "sliceLabel";
            //sliceLabel.Size = new System.Drawing.Size(41, 13);
            sliceLabel.Text = "Слой " + (i+1).ToString() + ":";

            var sliceSizeNumericUpDown = new NumericUpDown();
            sliceSizeNumericUpDown.Location = new System.Drawing.Point(30 + sliceLabel.Width + 5, shadowSlicesLabel.Top + shadowSlicesLabel.Height + 10 + step * i);
            sliceSizeNumericUpDown.Name = "sliceSizeNumericUpDown";
            sliceSizeNumericUpDown.Size = new System.Drawing.Size(47, 20);

            var tmpLabel = new Label();
            tmpLabel.AutoSize = true;
            tmpLabel.Font = new Font(FontFamily.GenericSansSerif, 12);
            tmpLabel.Location = new System.Drawing.Point(sliceSizeNumericUpDown.Left + sliceSizeNumericUpDown.Width + 5, shadowSlicesLabel.Top + shadowSlicesLabel.Height + 10 + step * i);
            tmpLabel.Name = "tmpLabel";
            tmpLabel.Text = "нейронов";

            if (i < maxSliceCount-1)
                addSliceButton.Top += step;
            else
                addSliceButton.Dispose();

            archTabPage.Controls.Add(sliceLabel);
            archTabPage.Controls.Add(sliceSizeNumericUpDown);
            archTabPage.Controls.Add(tmpLabel);
            
            numUpDowns.Add(sliceSizeNumericUpDown);
        }

        private void addSliceButton_Click(object sender, EventArgs e)
        {
            AddSliceControls();
        }

        private void autosizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autosizeCheckBox.Checked)
            {
                inLengthNumericUpDown.Enabled = false;
                outLengthNumericUpDown.Enabled = false;
            }
            else
            {
                inLengthNumericUpDown.Enabled = true;
                outLengthNumericUpDown.Enabled = true;
            }
        }
    }
}
