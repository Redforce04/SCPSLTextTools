using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMiro.Framework
{
    /// <summary>
    /// Defines the organization-level access to the board. If the board is created for a team that does not belong to an organization, the organizationAccess parameter is always set to the default value.
    /// </summary>
    public enum OrganizationAccess
    {
        _private,
        view,
        comment,
        edit
    }
}
