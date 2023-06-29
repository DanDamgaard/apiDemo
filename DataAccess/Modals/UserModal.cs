using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Modals;

public class UserModal
{
    public int id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
