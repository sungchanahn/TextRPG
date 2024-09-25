using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    interface IStatus
    {
        string? Name { get; set; }
        int Level { get; set; }
        int CharacterAttack { get; set; }
        int CharacterDefense { get; set; }
        int CharacterMaxHP { get; set; }
        int CurrentHP { get; set; }
        int Gold { get; set; }
    }

    public enum CLASSTYPE
    {
        Warrior = 1,
        Mage
    }

    internal class Player : IStatus
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public CLASSTYPE ClassType { get; set; }
        public string? CharacterClass { get; set; }
        public int CharacterAttack { get; set; }
        public int TotalAttack { get; set; }
        public int CharacterDefense { get; set; }
        public int TotalDefense { get; set; }
        public int CharacterMaxHP { get; set; }
        public int TotalMaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int Gold { get; set; }

        public List<Item> inventory = new List<Item>();
        public Item equipMainWeapon = new Item();
        public Item equipSubWeapon = new Item();
        public Item equipArmor = new Item();

        public Item warriorBasicMainWeapon = new Item(ITEMTYPE.MainWeapon, "낡은 철검", 300, 3, 0, 0, "녹슬고 무딘 철검", true);
        public Item mageBasicMainWeapon = new Item(ITEMTYPE.MainWeapon, "낡은 완드", 300, 3, 0, 0, "균열이 있는 완드", true);
        public Item basicSubWeapon = new Item(ITEMTYPE.SubWeapon, "낡은 단검", 200, 2, 0, 0, "부러질 듯한 단검", true);
        public Item basicArmor = new Item(ITEMTYPE.Armor, "낡은 갑옷", 300, 0, 3, 10, "녹이 슨 철제갑옷", true);

        void PlayerBasicStats()
        {
            if (ClassType == CLASSTYPE.Warrior)
            {
                CharacterClass = "전사";
                Level = 1;
                CharacterAttack = 10;
                CharacterDefense = 5;
                CharacterMaxHP = 100;
                CurrentHP = CharacterMaxHP;
                Gold = 1500;
            }
            else if (ClassType == CLASSTYPE.Mage)
            {
                CharacterClass = "법사";
                Level = 1;
                CharacterAttack = 14;
                CharacterDefense = 3;
                CharacterMaxHP = 80;
                CurrentHP = CharacterMaxHP;
                Gold = 1500;
            }
        }

        public void SupplyBasicItem()
        {
            if (ClassType == CLASSTYPE.Warrior)
            {
                inventory.Add(warriorBasicMainWeapon);
            }
            else if (ClassType == CLASSTYPE.Mage)
            {
                inventory.Add(mageBasicMainWeapon);
            }
            inventory.Add(basicSubWeapon);
            inventory.Add(basicArmor);
        }

        public void ChooseClass()
        {
            while (true)
            {
                Console.WriteLine("원하는 직업의 숫자를 입력하세요.");
                Console.WriteLine("[1] 전사\n[2] 법사");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "1" || input == "2")
                {
                    switch (input)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("전사를 선택하셨습니다.");
                            ClassType = CLASSTYPE.Warrior;
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("법사를 선택하셨습니다.");
                            ClassType = CLASSTYPE.Mage;
                            break;
                    }
                    PlayerBasicStats();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            TutorialChooseShow();
        }

        public void TutorialChooseShow()
        {
            Console.WriteLine();
            Console.WriteLine("직업 선택 완료!");
            Console.WriteLine();
            Console.WriteLine("기본 장비가 주어집니다. 인벤토리를 확인해보세요.");

            SupplyBasicItem();

            string input;
            while (true)
            {
                Console.WriteLine("2를 입력해 인벤토리를 볼 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[2] 인벤토리 보기");
                Console.WriteLine();
                Console.Write(">> ");
                input = Console.ReadLine();

                if (input == "2")
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            Console.Clear();
            ShowInventory();
        }

        public void ShowStatus(Item equipMainWeapon, Item equipSubWeapon, Item equipArmor)
        {
            while (true)
            {
                int itemTotalAtk = equipMainWeapon.Atk + equipSubWeapon.Atk + equipArmor.Atk;
                int itemTotalDef = equipMainWeapon.Def + equipSubWeapon.Def + equipArmor.Def;
                int itemTotalHP = equipMainWeapon.AdditionalHP + equipSubWeapon.AdditionalHP + equipArmor.AdditionalHP;
                TotalAttack = CharacterAttack + itemTotalAtk;
                TotalDefense = CharacterDefense + itemTotalDef;
                TotalMaxHP = CharacterMaxHP + itemTotalHP;

                Console.WriteLine("이름\t: " + Name);
                Console.WriteLine("직업\t: " + CharacterClass);
                Console.WriteLine("레벨\t: " + Level);
                Console.WriteLine($"공격력\t: {TotalAttack}[{CharacterAttack} +{itemTotalAtk}]");
                Console.WriteLine($"방어력\t: {TotalDefense}[{CharacterDefense} +{itemTotalDef}]");
                Console.WriteLine($"체력\t: {CurrentHP} / {TotalMaxHP}[{CharacterMaxHP} +{itemTotalHP}]");
                Console.WriteLine($"소지금\t: {Gold}G");
                Console.WriteLine();
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.Write("0을 눌러 나가면 다른 행동을 선택할 수 있습니다.\n>> ");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == "show me the money")
                {
                    Console.Clear();
                    Gold += 10000;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("나가려면 0을 입력하세요.");
                    Console.WriteLine();
                }
            }
        }

        public void ShowInventory()
        {
            while (true)
            {
                Console.WriteLine(Name + "의 인벤토리");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    string itemEquipState = "";
                    if (inventory[i] == equipMainWeapon || inventory[i] == equipArmor || inventory[i] == equipSubWeapon)
                    {
                        itemEquipState = "[E]";
                    }
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($" [{i + 1}] {itemEquipState}{inventory[i].Name}({inventory[i].ItemTypeKorean})" +
                                      $" | {inventory[i].Description}" +
                                      $" | 공격력 +{inventory[i].Atk}" +
                                      $" 방어력 +{inventory[i].Def}" +
                                      $" 추가체력 +{inventory[i].AdditionalHP}" +
                                      $" | {inventory[i].Price}G |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("아이템 번호를 입력하면 해당 아이템을 장착하거나 해제할 수 있습니다.");
                Console.WriteLine("주무기, 보조무기, 갑옷을 한 개씩 장착할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.Write(">> ");

                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0) break;
                    else if (select > 0 && select <= inventory.Count)
                    {
                        ManageItemEquip(inventory[select - 1]);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public void ManageItemEquip(Item selectItem)
        {
            if (selectItem.ItemType == ITEMTYPE.MainWeapon && selectItem != equipMainWeapon)
            {
                CurrentHP -= equipMainWeapon.AdditionalHP;
                equipMainWeapon = selectItem;
                CurrentHP += selectItem.AdditionalHP;
            }
            else if (selectItem.ItemType == ITEMTYPE.SubWeapon && selectItem != equipSubWeapon)
            {
                CurrentHP -= equipSubWeapon.AdditionalHP;
                equipSubWeapon = selectItem;
                CurrentHP += selectItem.AdditionalHP;
            }
            else if (selectItem.ItemType == ITEMTYPE.Armor && selectItem != equipArmor)
            {
                CurrentHP -= equipArmor.AdditionalHP;
                equipArmor = selectItem;
                CurrentHP += selectItem.AdditionalHP;
            }
            else if (selectItem == equipMainWeapon)
            {
                equipMainWeapon = new Item();
                CurrentHP -= selectItem.AdditionalHP;
            }
            else if (selectItem == equipSubWeapon)
            {
                equipSubWeapon = new Item();
                CurrentHP -= selectItem.AdditionalHP;
            }
            else if (selectItem == equipArmor)
            {
                equipArmor = new Item();
                CurrentHP -= selectItem.AdditionalHP;
            }
        }

        public void Rest()
        {
            while (true)
            {
                Console.WriteLine("여관에서 500G를 내고 휴식을 취해 체력을 회복할 수 있습니다.");
                Console.WriteLine($"현재 소지금: {Gold}G");
                Console.WriteLine();
                Console.WriteLine("[1] 휴식하기");
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.WriteLine("원하는 행동을 입력해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    if (Gold >= 500)
                    {
                        if (CurrentHP < TotalMaxHP)
                        {
                            Gold -= 500;
                            CurrentHP += 80;
                            if (CurrentHP > TotalMaxHP)
                            {
                                CurrentHP = TotalMaxHP;
                            }
                            Console.Clear();
                            Console.WriteLine("500G를 내고 휴식을 취했습니다.");
                            Console.WriteLine("체력을 80 회복했습니다.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("이미 최대 체력 상태입니다.");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("소지금이 부족합니다.");
                        Console.WriteLine();
                    }
                }
                else if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine();
                }
            }
        }
    }
}
