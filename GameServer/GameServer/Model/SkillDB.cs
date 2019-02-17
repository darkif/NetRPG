using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class SkillDB
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
        public int RoleId { get; set; }
    }
}
