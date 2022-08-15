using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Media;
using System.Reflection;
using System.Data;
using System.Data.OleDb;

namespace IronMountainDemo
{
    class clsEmployeeManager
    {
        #region Attributes
        /// <summary>
        /// Data Access variable used to access the database.
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// DataSet variable used to store the set of employees returned from the database.
        /// </summary>
        private DataSet dsEmployees;

        /// <summary>
        /// Integer variable used to store the number of employees returned from the database.
        /// </summary>
        private int iEmployeeRet = 0;

        /// <summary>
        /// List used to store and interact with information from the dsEmployees dataset.
        /// </summary>
        public List<clsEmployee> lstEmployeeList;
        #endregion

        #region Properties
        #endregion


        #region Methods
        /// <summary>
        /// Constructor for the clsEmployeeManager class. The constructor instantiated the clsDataAccess class to provide
        /// access to the database. It also retrieves the dsEmployees datasets, instantiates lstEmployeeList list and populates it.
        /// </summary>
        public clsEmployeeManager()
        {
            try
            {
                db = new clsDataAccess();

                dsEmployees = db.ExecuteSQLStatement("SELECT * FROM Employees", ref iEmployeeRet);
                lstEmployeeList = new List<clsEmployee>();
                clsEmployee Employee;


                for (int i = 0; i < iEmployeeRet; i++)
                {
                    Employee = new clsEmployee();
                    Employee.SEmployeeID = dsEmployees.Tables[0].Rows[i]["EmployeeID"].ToString();
                    Employee.SFirstName = dsEmployees.Tables[0].Rows[i]["EmployeeFirstName"].ToString();
                    Employee.SLastName = dsEmployees.Tables[0].Rows[i]["EmployeeLastName"].ToString();
                    Employee.SDateOfBirth = dsEmployees.Tables[0].Rows[i]["DateOfBirth"].ToString();

                    lstEmployeeList.Add(Employee);
                }
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to return the value of the lstEmployeeList variable.
        /// </summary>
        /// <returns></returns>
        public List<clsEmployee> GetEmployees()
        {
            try
            {
                dsEmployees = db.ExecuteSQLStatement("SELECT * FROM Employees", ref iEmployeeRet);
                lstEmployeeList = new List<clsEmployee>();
                clsEmployee Employee;


                for (int i = 0; i < iEmployeeRet; i++)
                {
                    Employee = new clsEmployee();
                    Employee.SEmployeeID = dsEmployees.Tables[0].Rows[i]["EmployeeID"].ToString();
                    Employee.SFirstName = dsEmployees.Tables[0].Rows[i]["EmployeeFirstName"].ToString();
                    Employee.SLastName = dsEmployees.Tables[0].Rows[i]["EmployeeLastName"].ToString();
                    Employee.SDateOfBirth = dsEmployees.Tables[0].Rows[i]["DateOfBirth"].ToString();

                    lstEmployeeList.Add(Employee);
                }
                return lstEmployeeList;
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to add an employee to the database.
        /// </summary>
        /// <returns></returns>
        public void AddEmployee(string sEmployeeID, string sFirstName, string sLastName, string sDOB)
        {
            try
            {
                string sSQL = "INSERT INTO Employees(EmployeeID, EmployeeFirstName, EmployeeLastName, DateOfBirth)" + "VALUES('" + sEmployeeID
                    + "','" + sFirstName + "','" + sLastName + "','" + sDOB + "')";
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to check whether an employee ID already exists.
        /// </summary>
        /// <returns></returns>
        public bool CheckValidEmployeeID(string sEmployeeID)
        {
            try
            {
                if(lstEmployeeList.Count != 0)
                {
                    if (lstEmployeeList.Where(e => e.SEmployeeID == sEmployeeID).Count() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
