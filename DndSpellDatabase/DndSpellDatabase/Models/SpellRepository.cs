using DndSpellDatabase.EntityDataModel;
using System.Collections.Generic;
using System.Linq;

namespace DndSpellDatabase.Models
{
    public class SpellRepository
    {
        private static SpellDatabaseEntities _spellDb;
        private static SpellDatabaseEntities SpellDb
        {
            get { return _spellDb ?? (_spellDb = new SpellDatabaseEntities()); }
        }

        public static IEnumerable<Spell> GetSpell()
        {
            var query = from spells in SpellDb.Spells orderby spells.Name select spells;
            return query.ToList();
        }

        public static void InsertSpell(Spell spell)
        {
            SpellDb.Spells.Add(spell);
            SpellDb.SaveChanges();
        }

        public static void DeleteSpell(int spellId)
        {
            var deleteItem = SpellDb.Spells.FirstOrDefault(p => p.Id == spellId);

            if (deleteItem != null)
            {
                SpellDb.Spells.Remove(deleteItem);
                SpellDb.SaveChanges();
            }
        }
    }
}