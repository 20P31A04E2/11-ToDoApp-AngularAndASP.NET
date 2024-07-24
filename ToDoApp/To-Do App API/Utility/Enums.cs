namespace Utility
{
    public enum MenuOption
    {
        EmployeeManagement = 1,
        RoleManagement,
        Exit
    }

    public enum EmployeeMenu
    {
        AddEmployee = 1,
        ViewAllEmployees,
        ViewAnEmployee,
        EditEmployee,
        DeleteEmployee,
        IsEmployeeManagement
    }

    public enum EditEmployeeMenu
    {

        FirstName = 1,
        LastName,
        DateOfBirth,
        Email,
        Phone,
        JoinDate,
        Location,
        JobTitle,
        Department,
        Manager,
        Project,
        ContinueEditing
    }

    public enum RoleManagementMenu
    {
        AddRole = 1,
        DisplayAll,
        IsRoleMenu
    }
}
