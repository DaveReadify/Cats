using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services
{
    public interface IJsonService
    {
        Task<List<Person>> GetPeople();
    }
}