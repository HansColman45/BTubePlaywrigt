using Beast.Domain.Entities;
using CMDB.Testing.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beast.Testing.Builders.EntityBuilders
{
    public class UserBuilder: GenericBogusEntityBuilder<User>
    {
        public UserBuilder()
        {
            SetDefaultRules((f, u) =>
            {
                u.FirtName = f.Person.FirstName;
                u.LastName= f.Person.LastName;
                u.EMailAddress = f.Person.Email;
                u.Password = f.Internet.Password();
            });
        }
    }
}
