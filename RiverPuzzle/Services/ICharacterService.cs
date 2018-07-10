using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverPuzzle.Services
{
    public interface ICharacterService
    {
        string Validate(List<Character> characters);
    }
}
