using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IronMountainDemo
{
    public partial class IronMountainDemo : Form
    {
        private clsEmployeeManager employeeManager = new clsEmployeeManager();
        bool bReset = false;

        public IronMountainDemo()
        {
            InitializeComponent();
            Reset(false);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sFirstName = txtFirstName.Text;
                if (sFirstName == "")
                {
                    sFirstName = null;
                }

                employeeManager.AddEmployee(txtEmployeeID.Text, sFirstName, txtLastName.Text, ConvertDOB());
                string sResult = "Employee Added:\n" + txtEmployeeID.Text + "\n";
                if (sFirstName != "")
                {
                    sResult += sFirstName + " ";
                }
                sResult += txtLastName.Text + "\n";
                sResult += ConvertDOB();
                lblResult.Text = sResult;
                Reset(true);
                bReset = false;
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void btnTextFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (employeeManager.GetEmployees().Count > 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {


                        System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                        file.WriteLine(DBToText());
                        file.Close();
                    }
                }
                else
                {
                    lblResult.Text = "No employees in database";
                }
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void btnXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (employeeManager.GetEmployees().Count > 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    saveFileDialog1.Filter = "XML-File | *.xml";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {


                        System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                        file.WriteLine(DBToXML());
                        file.Close();
                    }
                }
                else
                {
                    lblResult.Text = "No employees in database";
                }
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            if(bReset == false)
            {
                ValidateEmployeeID();
                ValidateForm();
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if(bReset == false)
            {
                ValidateLastName();
                ValidateForm();
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if(bReset == false)
            {
                ValidateFirstName();
                ValidateForm();
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            if(bReset == false)
            {
                ValidateForm();
            }
        }

        private bool ValidateEmployeeID()
        {
            lblEmpIDError.Text = "";
            Regex rLength = new Regex(@"[0-9]{8}");
            Regex rNum = new Regex(@"^[0-9]*$");
            bool validLength= rLength.IsMatch(txtEmployeeID.Text);
            bool validNum = rNum.IsMatch(txtEmployeeID.Text);
            if (!validLength && !validNum)
            {
                // Show error message
                lblEmpIDError.Text = "Error: Numeric only, 8 digits required";
                return false;
            }
            else if ((!validLength && validNum) || (validLength && !validNum))
            {
                if(!validLength)
                {
                    lblEmpIDError.Text = "8 digits required";
                    return false;
                }
                else if(!validNum)
                {
                    lblEmpIDError.Text = "Error: Numeric only";
                    return false;
                }
                return false;
            }
            else if(!employeeManager.CheckValidEmployeeID(txtEmployeeID.Text))
            {
                lblEmpIDError.Text = "Employee ID is already in use";
                return false;
            }
            else
            {
                // Clear error message
                lblEmpIDError.Text = "";
                return true;
            }
        }

        private bool ValidateFirstName()
        {
            lblFirstNameError.Text = "";
            if(txtFirstName.Text != "")
            {
                Regex rLength = new Regex(@"[a-zA-Z\s\,]{1,30}");
                Regex rValues = new Regex(@"^[a-zA-Z\s\,]*$");
                bool validLength = rLength.IsMatch(txtFirstName.Text);
                bool validValues = rValues.IsMatch(txtFirstName.Text);
                if (!validLength && !validValues)
                {
                    // Show error message
                    lblFirstNameError.Text = "Error: Letters and commas only, 30 characters max";
                    return false;
                }
                else if ((!validLength && validValues) || (validLength && !validValues))
                {
                    if (!validLength)
                    {
                        lblFirstNameError.Text = "30 characters max";
                        return false;
                    }
                    else if (!validValues)
                    {
                        lblFirstNameError.Text = "Error: Letters and commas only";
                        return false;
                    }
                    return false;
                }
                else
                {
                    // Clear error message
                    lblFirstNameError.Text = "";
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool ValidateLastName()
        {
            lblLastNameError.Text = "";
            Regex rLength = new Regex(@"[a-zA-Z\s\,]{1,30}");
            Regex rValues = new Regex(@"^[a-zA-Z\s\,]*$");
            bool validLength = rLength.IsMatch(txtLastName.Text);
            bool validValues = rValues.IsMatch(txtLastName.Text);
            if (!validLength && !validValues)
            {
                // Show error message
                lblLastNameError.Text = "Error: Letters and commas only, 30 characters max";
                return false;
            }
            else if ((!validLength && validValues) || (validLength && !validValues))
            {
                if (!validLength)
                {
                    lblLastNameError.Text = "1 character min, 30 characters max";
                    return false;
                }
                else if (!validValues)
                {
                    lblLastNameError.Text = "Error: Letters and commas only";
                    return false;
                }
                return false;
            }
            else
            {
                // Clear error message
                lblLastNameError.Text = "";
                return true;
            }
        }

        private string ConvertDOB()
        {
            string sDOB = "";
            if (dtpDOB.Value.Month < 10)
            {
                sDOB += "0" + dtpDOB.Value.Month.ToString();
            }
            else
            {
                sDOB += dtpDOB.Value.Month.ToString();
            }

            if (dtpDOB.Value.Day < 10)
            {
                sDOB += "0" + dtpDOB.Value.Day.ToString();
            }
            else
            {
                sDOB += dtpDOB.Value.Day.ToString();
            }

            sDOB += dtpDOB.Value.Year.ToString();

            return sDOB;
        }


        private void ValidateForm()
        {
            bool ValidForm = ValidateEmployeeID() && ValidateLastName() && ValidateFirstName();
            btnSubmit.Enabled = ValidForm;
        }

        private void Reset(bool afterSubmit)
        {
            try
            {
                btnSubmit.Enabled = false;
                lblEmpIDError.Text = "";
                lblLastNameError.Text = "";
                lblFirstNameError.Text = "";
                lblDOBError.Text = "";

                if (!afterSubmit)
                {
                    lblResult.Text = "";
                }
                else
                {
                    bReset = true;
                }

                dtpDOB.MaxDate = DateTime.Now.AddYears(-17);
                dtpDOB.Value = DateTime.Now.AddYears(-18);
                txtEmployeeID.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";

                if (employeeManager.GetEmployees().Count > 0)
                {
                    btnTextFile.Enabled = true;
                    btnXMLFile.Enabled = true;
                }
                else
                {
                    btnTextFile.Enabled = false;
                    btnXMLFile.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private string DBToText()
        {
            try
            {
                string sText = "";
                List<clsEmployee> employees = employeeManager.GetEmployees();
                for (int i = 0; i < employees.Count; i++)
                {
                    sText += employees[i].SEmployeeID + "|" + employees[i].SLastName + "|" + employees[i].SFirstName 
                        + "|" + employees[i].SDateOfBirth.Substring(0,2) + "/" + employees[i].SDateOfBirth.Substring(2, 2) + "/" + employees[i].SDateOfBirth.Substring(4) + "\n";
                }
                return sText;
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private string DBToXML()
        {
            try
            {
                string sXML = "";
                List<clsEmployee> employees = employeeManager.GetEmployees();
                sXML += "<Employees>\n";
                for (int i = 0; i < employees.Count; i++)
                {
                    sXML += "\t<Employee ID=\"" + employees[i].SEmployeeID + "\">\n";
                    sXML += "\t\t<LastName>" + employees[i].SLastName + "</LastName>\n";
                    sXML += "\t\t<FirstName>" + employees[i].SFirstName + "</FirstName>\n";
                    sXML += "\t\t<DateOfBirth>" + employees[i].SDateOfBirth.Substring(0, 2) + "/" + employees[i].SDateOfBirth.Substring(2, 2) + "/" + employees[i].SDateOfBirth.Substring(4) + "</DateOfBirth>\n";
                    sXML += "\t</Employee>\n";
                }
                sXML += "</Employees>";
                return sXML;
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
