using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Data.Model
{
    internal class Creature
    {
        private int Id;
        private string Archclass;
        private string Class;
        private string Race;
        private string Name;
        private int UserId;
        private int Mana;

        public Creature(int Id, string Archclass, string Class, string Race, string Name, int UserId, int Mana)
        {
            this.Id = Id;
            this.Archclass = Archclass;
            this.Class = Class;
            this.Race = Race;
            this.Name = Name;
            this.UserId = UserId;
            this.Mana = Mana;
        }

        //Setery

        public void SetId(int Id)
        {
            this.Id=Id;
        }

        public void SetArchclass(string Archclass)
        {
            this.Archclass=Archclass;
        }

        public void SetClass(string Class)
        {
            this.Class=Class;
        }

        public void SetRace(string Race)
        {
            this.Race=Race;
        }

        public void SetName(string Name)
        {
            this.Name=Name;
        }

        public void SetUserId(int UserId)
        {
            this.UserId=UserId;
        }

        public void SetMana(int Mana)
        {
            this.Mana=Mana;
        }

        //Getery
        public int GetId()
        {
            return this.Id;
        }

        public string GetArchclass()
        {
            return this.Archclass;
        }

        public string GetClass()
        {
            return this.Class;
        }

        public string GetRace()
        {
            return this.Race;
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetUserId()
        {
            return this.UserId;
        }

        public int GetMana()
        {
            return this.Mana;
        }

    }
}
