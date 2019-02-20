using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    public class InventoryItemDB
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int Count { get; set; }
        public int RoleId { get; set; }
        public bool IsDressed { get; set; }
    }
}
