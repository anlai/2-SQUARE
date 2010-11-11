using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _2SQUARE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public AdminOnlyAttribute()
        {
            Roles = RoleNames.RoleAdmin;    //Set the roles prop to a comma delimited string of allowed roles
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ProjectManagerOnlyAttribute : AuthorizeAttribute
    {
        public ProjectManagerOnlyAttribute()
        {
            Roles = RoleNames.RoleProjectManager;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirementsEngineerAttribute : AuthorizeAttribute
    {
        public RequirementsEngineerAttribute()
        {
            Roles = RoleNames.RoleRequirementsEngineer;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StakeholderAttribute : AuthorizeAttribute
    {
        public StakeholderAttribute()
        {
            Roles = RoleNames.RoleStakeholder;
        }
    }

    public static class RoleNames
    {
        public static readonly string RoleAdmin = "Admin";
        public static readonly string RoleProjectManager = "ProjectManager";
        public static readonly string RoleRequirementsEngineer = "RequirementsEngineer";
        public static readonly string RoleStakeholder = "Stakeholder";
    }

}