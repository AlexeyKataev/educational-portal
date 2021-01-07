using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

/* Управление ролями пользователей */

public class ChangeRoleViewModel
{
    public int UserId { get; set; }

    public string UserEmail { get; set; }

    public List<IdentityRole> AllRoles { get; set; }

    public IList<string> UserRoles { get; set; }

    public ChangeRoleViewModel()
    {
        AllRoles = new List<IdentityRole>();
        UserRoles = new List<string>();
    }
}