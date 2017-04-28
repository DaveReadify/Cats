using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public interface IPet
    {
        string Type { get; }
        string Name { get; }
    }

    public class Pet : IPet
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public PetType PetType => (PetType)Enum.Parse(typeof(PetType), Type, true);
    }
    
}
