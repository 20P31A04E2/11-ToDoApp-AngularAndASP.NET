namespace Utility
{
    public static class Constants
    {
        public const string EmployeeJsonFilePath = "C:/Users/sravanthi.k/OneDrive - Technovert/Tezo/Task_5 MS-VS/Data/employees.json";
        public const string RoleJsonFilePath = "C:/Users/sravanthi.k/OneDrive - Technovert/Tezo/Task_5 MS-VS/Data/roles.json";
    }

    public static class Prompts
    {
        public const string EmployeeMenu = "\nSelect an option.\n1. Add employee\n2. Display all\n3. Display one\n4. Edit employee\n5. Delete employee\n6. Go back";
        public const string RoleMenu = "\nSelect an option:\n1. Add role\n2. Display all\n3. Go back";
        public const string SelectedOption = "Selected option is: ";
        public const string InvalidMessage = "Invalid option. Please try again.";
        public const string ValidationErrorMessage = "Please enter Valid {0}.";
        public const string DateErrorMessage = "Please enter date in the format DD-MM-YYYY.";
        public const string NoEmployeesMessage = "No employees found.";
        public const string EmployeeIDInputMessage = "\nEnter ID of the Employee: ";
        public const string EmployeeIDErrorMessage = "Employee with the ID you provided is not found.";
        public const string EditMenu = "\nSelect the option to edit the required data.\n1. First name\n2. Last name\n3. Date of Birth\n4. Email\n5. Phone number\n6. Join Date\n7. Location\n8. Job Title\n9. Department\n10. Manager\n11. Project\n12. Go back";
        public const string EmployeeDeletedMessage = "Employee deleted successfully...!";
        public const string EmployeeUpdatedMessage = "Employee Updated successfully...!";
        public const string EmployeeAddedMessage = "Employee added successfully...!";
        public const string RoleAddedMessage = "Role added successfully...!";
        public const string NoRolesMessage = "No roles found.";
    }
}
