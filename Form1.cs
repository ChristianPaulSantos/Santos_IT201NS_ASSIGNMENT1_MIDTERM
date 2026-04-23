using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradeComputationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool TryParseScore(TextBox txtScore, TextBox txtTotal, string label,
                                   out double score, out double total)
        {
            score = 0;
            total = 0;
            try
            {
                score = double.Parse(txtScore.Text);
                total = double.Parse(txtTotal.Text);

                if (total == 0)
                    throw new DivideByZeroException();

                if (score < 0 || total < 0)
                {
                    MessageBox.Show($"Values must not be negative.\nField: {label}",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (score > total)
                {
                    MessageBox.Show($"Score cannot be greater than Total.\nField: {label}",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show($"Please enter valid numbers.\nField: {label}",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show($"Total score cannot be zero.\nField: {label}",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ComputeGroupAverage(TextBox[] scoreBoxes, TextBox[] totalBoxes,
                                         string[] labels, out double average)
        {
            int count = scoreBoxes.Length;
            double[] scores = new double[count];
            double[] totals = new double[count];

            for (int i = 0; i < count; i++)
            {
                if (!TryParseScore(scoreBoxes[i], totalBoxes[i], labels[i],
                                   out scores[i], out totals[i]))
                {
                    average = 0;
                    return false;
                }
            }

            double sum = 0;
            for (int i = 0; i < count; i++)
                sum += (scores[i] / totals[i]) * 60 + 40;

            average = sum / count;
            return true;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            string[] cpLabels = { "Assignment 1", "Assignment 2", "Seatwork 1", "Seatwork 2", "Recitation 1", "Recitation 2" };
            string[] leLabels = { "Lab 1", "Lab 2", "Lab 3", "Lab 4" };
            string[] qLabels = { "Quiz 1", "Quiz 2", "Quiz 3" };
            string[] lxLabels = { "Lab Exam 1", "Lab Exam 2" };
            string[] fpLabels = { "Presentation", "Manuscript" };
            string[] pWeLabel = { "Prelim - Written Exam" };
            string[] mWeLabel = { "Midterm - Written Exam" };
            string[] fWeLabel = { "Finals - Final Exam" };

            double avg;

            double pCP, pLE, pQ, pLX, pWE;

            if (!ComputeGroupAverage(
                    new[] { txtP_CP_S0, txtP_CP_S1, txtP_CP_S2, txtP_CP_S3, txtP_CP_S4, txtP_CP_S5 },
                    new[] { txtP_CP_T0, txtP_CP_T1, txtP_CP_T2, txtP_CP_T3, txtP_CP_T4, txtP_CP_T5 },
                    cpLabels, out pCP)) return;

            if (!ComputeGroupAverage(
                    new[] { txtP_LE_S0, txtP_LE_S1, txtP_LE_S2, txtP_LE_S3 },
                    new[] { txtP_LE_T0, txtP_LE_T1, txtP_LE_T2, txtP_LE_T3 },
                    leLabels, out pLE)) return;

            if (!ComputeGroupAverage(
                    new[] { txtP_Q_S0, txtP_Q_S1, txtP_Q_S2 },
                    new[] { txtP_Q_T0, txtP_Q_T1, txtP_Q_T2 },
                    qLabels, out pQ)) return;

            if (!ComputeGroupAverage(
                    new[] { txtP_LX_S0, txtP_LX_S1 },
                    new[] { txtP_LX_T0, txtP_LX_T1 },
                    lxLabels, out pLX)) return;

            if (!ComputeGroupAverage(
                    new[] { txtP_WE_S0 },
                    new[] { txtP_WE_T0 },
                    pWeLabel, out pWE)) return;

            double prelim = (pCP * 0.10) + (pLE * 0.10) + (pQ * 0.20) + (pLX * 0.20) + (pWE * 0.40);
            txtPrelimResult.Text = prelim.ToString("F2");

            double mCP, mLE, mQ, mLX, mWE;

            if (!ComputeGroupAverage(
                    new[] { txtM_CP_S0, txtM_CP_S1, txtM_CP_S2, txtM_CP_S3, txtM_CP_S4, txtM_CP_S5 },
                    new[] { txtM_CP_T0, txtM_CP_T1, txtM_CP_T2, txtM_CP_T3, txtM_CP_T4, txtM_CP_T5 },
                    cpLabels, out mCP)) return;

            if (!ComputeGroupAverage(
                    new[] { txtM_LE_S0, txtM_LE_S1, txtM_LE_S2, txtM_LE_S3 },
                    new[] { txtM_LE_T0, txtM_LE_T1, txtM_LE_T2, txtM_LE_T3 },
                    leLabels, out mLE)) return;

            if (!ComputeGroupAverage(
                    new[] { txtM_Q_S0, txtM_Q_S1, txtM_Q_S2 },
                    new[] { txtM_Q_T0, txtM_Q_T1, txtM_Q_T2 },
                    qLabels, out mQ)) return;

            if (!ComputeGroupAverage(
                    new[] { txtM_LX_S0, txtM_LX_S1 },
                    new[] { txtM_LX_T0, txtM_LX_T1 },
                    lxLabels, out mLX)) return;

            if (!ComputeGroupAverage(
                    new[] { txtM_WE_S0 },
                    new[] { txtM_WE_T0 },
                    mWeLabel, out mWE)) return;

            double midterm = (mCP * 0.10) + (mLE * 0.10) + (mQ * 0.20) + (mLX * 0.20) + (mWE * 0.40);
            txtMidtermResult.Text = midterm.ToString("F2");

            double fCP, fLE, fQ, fFP, fWE;

            if (!ComputeGroupAverage(
                    new[] { txtF_CP_S0, txtF_CP_S1, txtF_CP_S2, txtF_CP_S3, txtF_CP_S4, txtF_CP_S5 },
                    new[] { txtF_CP_T0, txtF_CP_T1, txtF_CP_T2, txtF_CP_T3, txtF_CP_T4, txtF_CP_T5 },
                    cpLabels, out fCP)) return;

            if (!ComputeGroupAverage(
                    new[] { txtF_LE_S0, txtF_LE_S1, txtF_LE_S2, txtF_LE_S3 },
                    new[] { txtF_LE_T0, txtF_LE_T1, txtF_LE_T2, txtF_LE_T3 },
                    leLabels, out fLE)) return;

            if (!ComputeGroupAverage(
                    new[] { txtF_Q_S0, txtF_Q_S1, txtF_Q_S2 },
                    new[] { txtF_Q_T0, txtF_Q_T1, txtF_Q_T2 },
                    qLabels, out fQ)) return;

            if (!ComputeGroupAverage(
                    new[] { txtF_FP_S0, txtF_FP_S1 },
                    new[] { txtF_FP_T0, txtF_FP_T1 },
                    fpLabels, out fFP)) return;

            if (!ComputeGroupAverage(
                    new[] { txtF_WE_S0 },
                    new[] { txtF_WE_T0 },
                    fWeLabel, out fWE)) return;

            double finals = (fCP * 0.05) + (fLE * 0.10) + (fQ * 0.20) + (fFP * 0.25) + (fWE * 0.40);
            txtFinalsResult.Text = finals.ToString("F2");

            double finalGrade = (prelim * 0.33) + (midterm * 0.33) + (finals * 0.34);
            txtFinalGradeResult.Text = finalGrade.ToString("F2");

            MessageBox.Show(
                "Computation Complete!\n\n" +
                $"Prelim Grade  : {prelim:F2}\n" +
                $"Midterm Grade : {midterm:F2}\n" +
                $"Finals Grade  : {finals:F2}\n\n" +
                $"Final Grade   : {finalGrade:F2}",
                "Results Summary",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            TextBox[] allScores = {
                txtP_CP_S0,txtP_CP_S1,txtP_CP_S2,txtP_CP_S3,txtP_CP_S4,txtP_CP_S5,
                txtP_LE_S0,txtP_LE_S1,txtP_LE_S2,txtP_LE_S3,
                txtP_Q_S0, txtP_Q_S1, txtP_Q_S2,
                txtP_LX_S0,txtP_LX_S1,
                txtP_WE_S0,
                txtM_CP_S0,txtM_CP_S1,txtM_CP_S2,txtM_CP_S3,txtM_CP_S4,txtM_CP_S5,
                txtM_LE_S0,txtM_LE_S1,txtM_LE_S2,txtM_LE_S3,
                txtM_Q_S0, txtM_Q_S1, txtM_Q_S2,
                txtM_LX_S0,txtM_LX_S1,
                txtM_WE_S0,
                txtF_CP_S0,txtF_CP_S1,txtF_CP_S2,txtF_CP_S3,txtF_CP_S4,txtF_CP_S5,
                txtF_LE_S0,txtF_LE_S1,txtF_LE_S2,txtF_LE_S3,
                txtF_Q_S0, txtF_Q_S1, txtF_Q_S2,
                txtF_FP_S0,txtF_FP_S1,
                txtF_WE_S0
            };

            TextBox[] allTotals = {
                txtP_CP_T0,txtP_CP_T1,txtP_CP_T2,txtP_CP_T3,txtP_CP_T4,txtP_CP_T5,
                txtP_LE_T0,txtP_LE_T1,txtP_LE_T2,txtP_LE_T3,
                txtP_Q_T0, txtP_Q_T1, txtP_Q_T2,
                txtP_LX_T0,txtP_LX_T1,
                txtP_WE_T0,
                txtM_CP_T0,txtM_CP_T1,txtM_CP_T2,txtM_CP_T3,txtM_CP_T4,txtM_CP_T5,
                txtM_LE_T0,txtM_LE_T1,txtM_LE_T2,txtM_LE_T3,
                txtM_Q_T0, txtM_Q_T1, txtM_Q_T2,
                txtM_LX_T0,txtM_LX_T1,
                txtM_WE_T0,
                txtF_CP_T0,txtF_CP_T1,txtF_CP_T2,txtF_CP_T3,txtF_CP_T4,txtF_CP_T5,
                txtF_LE_T0,txtF_LE_T1,txtF_LE_T2,txtF_LE_T3,
                txtF_Q_T0, txtF_Q_T1, txtF_Q_T2,
                txtF_FP_T0,txtF_FP_T1,
                txtF_WE_T0
            };

            for (int i = 0; i < allScores.Length; i++)
            {
                allScores[i].Text = "0";
                allTotals[i].Text = "100";
            }

            txtPrelimResult.Text = "";
            txtMidtermResult.Text = "";
            txtFinalsResult.Text = "";
            txtFinalGradeResult.Text = "";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
                Application.Exit();
        }

        private void gbM_WE_Enter(object sender, EventArgs e)
        {

        }

        private void lblM_WE_ScoreH_Click(object sender, EventArgs e)
        {

        }

        private void txtM_LX_T0_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblM_WE_TotalH_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void gbF_LE_Enter(object sender, EventArgs e)
        {

        }
    }
}