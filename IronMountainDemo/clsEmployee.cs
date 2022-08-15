using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Media;
using System.Reflection;

namespace IronMountainDemo
{
    class clsEmployee
    {
        #region Attributes
        /// <summary>
        /// String variable to store the employee ID.
        /// </summary>
        private string sEmployeeID;

        /// <summary>
        /// String variable to store the employee's first name.
        /// </summary>
        private string sFirstName;

        /// <summary>
        /// String variable to store the employee's last name.
        /// </summary>
        private string sLastName;

        /// <summary>
        /// String variable to store the employee's date of birth.
        /// </summary>
        private string sDateOfBirth;

        #endregion

        #region Properties
        /// <summary>
        /// Property to get and set the sEmployeeID Variable.
        /// </summary>
        public string SEmployeeID
        {
            get
            {
                return sEmployeeID;
            }
            set
            {
                sEmployeeID = value;
            }
        }

        /// <summary>
        /// Property to get and set the sFirstName Variable.
        /// </summary>
        public string SFirstName
        {
            get
            {
                return sFirstName;
            }
            set
            {
                sFirstName = value;
            }
        }

        /// <summary>
        /// Property to get and set the sLastName Variable.
        /// </summary>
        public string SLastName
        {
            get
            {
                return sLastName;
            }
            set
            {
                sLastName = value;
            }
        }

        /// <summary>
        /// Property to get and set the dtDateOfBirth Variable.
        /// </summary>
        public string SDateOfBirth
        {
            get
            {
                return sDateOfBirth;
            }
            set
            {
                sDateOfBirth = value;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method to specify how employee objects should be displayed when using the ToString method.
        /// </summary>
        /// <returns>Returns a string with the concatenation of the first name and last name.</returns>
        public override string ToString()
        {
            return sFirstName + " " + sLastName;
        }
        #endregion
    }
}
