using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseptron
{
    class Perceptron
    {
        public Perceptron(int layerCount, int[] neuronsPerHiddenLayers, Func<double, double> activationFuncs, Func<double, double> derivatedActivationFuncs)
        {
            //    if (neuronsPerLayers.Length != layerCount)
            //        throw new Exception("Layer count mismatch");
            m_layerCount = layerCount + 2;
            m_hiddenLayersNeuronsCount = neuronsPerHiddenLayers;
            m_weights = new double[m_layerCount][,];
            m_dw = new double[m_layerCount][,];
            m_thresholds = new double[m_layerCount][];
            m_dthr = new double[m_layerCount][];
            for (int i = 2; i < m_layerCount - 1; i++)
            {
                m_weights[i] = new double[neuronsPerHiddenLayers[i - 1], neuronsPerHiddenLayers[i - 2]];
                m_dw[i] = new double[neuronsPerHiddenLayers[i - 1], neuronsPerHiddenLayers[i - 2]];
                m_thresholds[i-1] = new double[neuronsPerHiddenLayers[i - 2]];
                m_dthr[i-1] = new double[neuronsPerHiddenLayers[i - 2]];
            }
            m_thresholds[m_layerCount - 2] = new double[neuronsPerHiddenLayers[layerCount - 1]];
            m_dthr[m_layerCount - 2] = new double[neuronsPerHiddenLayers[layerCount - 1]];
            m_activationFuncs = activationFuncs;
            m_derivatedActivationFuncs = derivatedActivationFuncs;
        }

        public void TeachBy(double[,] a_samples, double[,] a_patterns, double a_teachingSpeed, double a_moment)
        {
            int sampleCount = a_samples.GetLength(0);
            int sampleLength = a_samples.GetLength(1);
            int patternLength = a_patterns.GetLength(1);

            m_weights[1] = new double[m_hiddenLayersNeuronsCount[0], sampleLength];
            m_dw[1] = new double[m_hiddenLayersNeuronsCount[0], sampleLength];
            m_weights[m_layerCount-1] = new double[patternLength, m_hiddenLayersNeuronsCount[m_layerCount-2 -1]];
            m_dw[m_layerCount-1] = new double[patternLength, m_hiddenLayersNeuronsCount[m_layerCount-2 -1]];
            m_thresholds[m_layerCount - 1] = new double[patternLength];
            m_dthr[m_layerCount - 1] = new double[patternLength];

            GenerateWeights();
            GenerateThresholds();

            var localPotential = new double[m_layerCount][];
            var delta = new double[m_layerCount][];
            var y = new double[m_layerCount][];

            y[0] = new double[sampleLength];
            for (int t = 1; t < m_layerCount-1; t++)
                y[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            y[m_layerCount-1] = new double[patternLength];
                
            for (int t = 1; t < m_layerCount-1; t++)
                delta[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            delta[m_layerCount-1] = new double[patternLength];
            
            //for (int t = 1; t < m_layerCount-1; t++)
            //    localPotential[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            //localPotential[m_layerCount-1] = new double[patternLength];

            double[,] errors = new double[sampleCount, sampleLength];
            //double[,] prevErrors = new double[sampleCount, sampleLength];
            double[] E = new double[sampleCount];
            double eps = 0.1;

            int samplesCount = a_samples.GetLength(0);
            for (int era = 0; era <1000; era++)
            {
                for (int s = 0; s < samplesCount; s++ )
                {
                    for (int i = 0; i< sampleLength; i++)
                        y[0][i] = a_samples[s, i];

                    for (int t = 1; t < m_layerCount; t++)
                    {
                        localPotential[t] = m_thresholds[t].Clone() as double[];
                        for (int j = 0; j < m_weights[t].GetLength(0); j++)
                        {
                            for (int i = 0; i < m_weights[t].GetLength(1); i++)
                                localPotential[t][j] += m_weights[t][j, i] * y[t-1][i];
                            y[t][j] = m_activationFuncs(localPotential[t][j]);
                        }
                    }

                    for (int j = 0; j < patternLength; j++)
                    {
                        //prevErrors[s, j] = errors[s, j];
                        errors[s, j] = (a_patterns[s, j] - y[m_layerCount - 1][j]);
                        delta[m_layerCount - 1][j] = m_derivatedActivationFuncs(y[m_layerCount - 1][j]) * errors[s,j];
                    }
                    for (int t = m_layerCount - 2; t > 0; t--)
                    {
                        for (int j = 0; j < m_weights[t + 1].GetLength(1); j++)
                        {
                            double tmp_sum = 0;
                            for (int k = 0; k < m_weights[t + 1].GetLength(0); k++)
                                tmp_sum += delta[t + 1][k] * m_weights[t + 1][k, j];
                            delta[t][j] = tmp_sum * m_derivatedActivationFuncs(y[t][j]);

                            for (int k = 0; k < m_weights[t + 1].GetLength(0); k++)
                            {
                                double tmp_d = a_teachingSpeed * delta[t + 1][k];

                                m_weights[t + 1][k, j] += tmp_d * y[t][j] + a_moment * m_dw[t + 1][k, j];
                                m_thresholds[t + 1][k] += tmp_d + a_moment * m_dthr[t + 1][k];
                                
                                m_dw[t + 1][k, j] = tmp_d * y[t][j];
                                m_dthr[t + 1][k] = tmp_d;
                            }
                        }
                    }

                    for (int i = 0; i < m_weights[1].GetLength(1); i++)
                        for (int j = 0; j < m_weights[1].GetLength(0); j++)
                        {
                            double tmp_d = a_teachingSpeed * delta[1][j];

                            m_weights[1][j, i] += tmp_d * y[0][i] + a_moment * m_dw[1][j, i];
                            m_thresholds[1][j] += tmp_d + a_moment * m_dthr[1][j];

                            m_dw[1][j, i] = tmp_d * y[0][i];
                            m_dthr[1][j] = tmp_d;

                            //m_weights[1][j, i] += a_teachingSpeed * delta[1][j] * y[0][i];
                            //m_thresholds[1][j] += a_teachingSpeed * delta[1][j];
                        }
                }

                for (int i = 0; i < sampleCount; i++)
                {
                    E[i] = 0;
                    for (int j = 0; j < sampleLength; j++)
                        E[i] += errors[i, j] * errors[i, j];
                    E[i] /= 2;
                }

                double mx = E[0];
                for (int i = 1; i < sampleCount; i++)
                    if (E[i] > mx)
                        mx = E[i];
                if (mx < eps)
                    break;
            }

            using (var bw = new BinaryWriter(File.Open("W.txt", FileMode.Create)))
           //     using (var bw = new BinaryWriter(File.Open("W1.dat", FileMode.Create)))
                for (int t = 1; t < m_layerCount; t++)
                    for (int j = 0; j < m_weights[t].GetLength(0); j++)
                        for (int i = 0; i < m_weights[t].GetLength(1); i++)
                            bw.Write(m_weights[t][j, i]);

            using (var bw = new BinaryWriter(File.Open("b.txt", FileMode.Create)))
                for (int t = 1; t < m_layerCount; t++)
                    for (int j = 0; j < m_thresholds[t].GetLength(0); j++)
                        bw.Write(m_thresholds[t][j]);
        }

        public double[] Classify(double[] sample)
        {
            int sampleLength = sample.Length;
            int patternLength = m_weights[m_layerCount - 1].GetLength(0);
            var localPotential = new double[m_layerCount][];
            var delta = new double[m_layerCount][];
            var y = new double[m_layerCount][];

            y[0] = new double[sampleLength];
            for (int t = 1; t < m_layerCount - 1; t++)
                y[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            y[m_layerCount - 1] = new double[patternLength];

            for (int t = 1; t < m_layerCount - 1; t++)
                delta[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            delta[m_layerCount - 1] = new double[patternLength];

            for (int t = 1; t < m_layerCount - 1; t++)
                localPotential[t] = new double[m_hiddenLayersNeuronsCount[t - 1]];
            localPotential[m_layerCount - 1] = new double[patternLength];

            for (int t = 1; t < m_layerCount; t++)
            {
                localPotential[t] = m_thresholds[t].Clone() as double[];
                for (int j = 0; j < m_weights[t].GetLength(0); j++)
                {
                    for (int i = 0; i < m_weights[t].GetLength(1); i++)
                        localPotential[t][j] += m_weights[t][j, i] * y[t-1][i];
                    y[t][j] = m_activationFuncs(localPotential[t][j]);
                }
            }
            return y[m_layerCount - 1];
        }

        private void GenerateWeights()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int t = 1; t < m_layerCount; t++)
                for (int i = 0; i < m_weights[t].GetLength(1); i++)
                    for (int j = 0; j < m_weights[t].GetLength(0); j++)
                        m_weights[t][j, i] = -0.5 + rand.NextDouble();
        }

        private void GenerateThresholds()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int t = 1; t < m_layerCount; t++)
                for (int j = 0; j < m_thresholds[t].GetLength(0); j++)
                    m_thresholds[t][j] = -0.5 + rand.NextDouble();
        }

double[][,] m_weights;
double[][,] m_dw;
double[][] m_thresholds;
double[][] m_dthr;

        int m_layerCount;
        int[] m_hiddenLayersNeuronsCount;
        Func<double, double> m_activationFuncs;
        Func<double, double> m_derivatedActivationFuncs;


         //for (int t = 1; t<m_layerCount - 1; t++)
         //   {
         //       localPotential[t + 1] = m_thresholds[t + 1].Clone() as double[];
         //       for (int j = 0; j<m_weights[t + 1].GetLength(0); j++)
         //       {
         //           for (int i = 0; i<m_weights[t + 1].GetLength(1); i++)
         //               localPotential[t + 1][j] += m_weights[t + 1][j, i] * y[t][i];
         //           y[t + 1][j] = m_activationFuncs(localPotential[t + 1][j]);
             //    }
             //}


    }
}
