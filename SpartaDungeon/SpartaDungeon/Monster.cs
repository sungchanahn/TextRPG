using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Monster : IStatus
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public int CharacterAttack { get; set; }
        public int CharacterDefense { get; set; }
        public int CharacterMaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int DropGold { get; set; }
        public bool IsDead => CurrentHP <= 0;

        public Monster(string name, int level, int monsterAttack, int monsterDefense, int monsterMaxHP, int dropGold)
        {
            Name = name;
            Level = level;
            CharacterAttack = monsterAttack;
            CharacterDefense = monsterDefense;
            CharacterMaxHP = monsterMaxHP;
            CurrentHP = CharacterMaxHP;
            DropGold = dropGold;
        }

        public void ShowMonsterState()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            string deadState = "";
            if (IsDead) deadState = "[처치!]";
            Console.WriteLine($"{deadState} {Name}(Lv.{Level}) |" +
                              $" 공격력 {CharacterAttack}" +
                              $" 방어력 {CharacterDefense}" +
                              $" 체력 {CurrentHP} / {CharacterMaxHP} |" +
                              $" 처치 시 획득 골드: {DropGold}");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
    }
}
