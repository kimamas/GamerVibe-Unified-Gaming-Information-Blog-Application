using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALClassLibrary
{
    public interface IBaseDAL
    {
        public string connectionString { get; set; }           
    }
}
