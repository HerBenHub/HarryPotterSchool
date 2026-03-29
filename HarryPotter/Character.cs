using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotter
{
    public class Character
    {
        public int Index { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string HogwardsHouse { get; set; }
        public string InterpretedBy { get; set; }
        public List<Child> Children { get; set; }
        public string Image { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Spell> KnownSpells { get; set; }
    }
}
