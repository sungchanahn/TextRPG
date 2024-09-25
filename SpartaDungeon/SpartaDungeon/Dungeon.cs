using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    internal class Dungeon
    {
        public int RewardGold { get; set; }

        Random random = new Random();

        public void EnterDungeon(Player player)
        {
            while (true)
            {
                Console.WriteLine("이곳은 던전 입구입니다. 던전 난이도를 선택하면 해당 던전으로 이동합니다.");
                Console.WriteLine();
                Console.WriteLine($"플레이어 현재 체력 : {player.CurrentHP} / {player.TotalMaxHP}");
                Console.WriteLine();
                Console.WriteLine("[1] Easy   - 권장 방어력 : 5");
                Console.WriteLine("[2] Normal - 권장 방어력 : 10");
                Console.WriteLine("[3] Hard   - 권장 방어력 : 18");
                Console.WriteLine($"플레이어 현재 방어력 : {player.TotalDefense}");
                Console.WriteLine();
                Console.WriteLine("[0] 나가기");
                Console.WriteLine();
                Console.WriteLine("원하는 난이도를 선택해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                if (player.CurrentHP > 10)
                {
                    if (input == "0")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (input == "1")
                    {
                        Console.Clear();
                        EasyDungeon(player);
                    }
                    else if (input == "2")
                    {
                        Console.Clear();
                        NormalDungeon(player);
                    }
                    else if (input == "3")
                    {
                        Console.Clear();
                        HardDungeon(player);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("플레이어가 [빈사] 상태입니다. 마을로 돌아가 휴식을 취해주세요.");
                    Console.WriteLine();
                }
            }
        }

        public void EasyDungeon(Player player)
        {
            Console.WriteLine("Easy 던전에 입장하였습니다.");
            Console.WriteLine("스켈레톤 5마리가 등장합니다.");

            List<Monster> stageMonsterList = new List<Monster>();
            RewardGold = 500;
            for (int i = 0; i < 5; i++)
            {
                int randomLevel = random.Next(1, 4);
                Monster skeleton = new Monster("스켈레톤 병사", randomLevel,
                                               3 + randomLevel,
                                               1 + randomLevel / 2,
                                               25 + randomLevel * 2,
                                               200 + randomLevel * 100);
                stageMonsterList.Add(skeleton);
                skeleton.ShowMonsterState();
            }
            int turn = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("[전투 중 ...]");

                bool isPlayerDead = false;
                int deadCount = 0;
                int obtainGold = 0;
                foreach (Monster skeleton in stageMonsterList)
                {
                    int playersDamage = player.TotalAttack > skeleton.CharacterDefense ? player.TotalAttack - skeleton.CharacterDefense : 1;
                    int skeletonsDamage = skeleton.CharacterAttack > player.CharacterDefense ? skeleton.CharacterAttack - player.CharacterDefense : 1;
                    skeleton.CurrentHP -= playersDamage;
                    player.CurrentHP -= skeletonsDamage;
                    skeleton.ShowMonsterState();
                    if (skeleton.IsDead)
                    {
                        deadCount++;
                        obtainGold += skeleton.DropGold;
                    }
                    if (player.CurrentHP <= 10)
                    {
                        isPlayerDead = true;
                        break;
                    }
                }
                if (deadCount == stageMonsterList.Count)
                {
                    player.Gold += obtainGold + RewardGold;
                    Console.WriteLine($"승리! {obtainGold + RewardGold}G 를 획득했습니다.");
                    Console.WriteLine($"현재 체력은 {player.CurrentHP} / {player.TotalMaxHP} 입니다.");
                    Console.WriteLine();
                    Console.WriteLine("퇴장하려면 Enter를 입력해주세요.");
                    Console.Write(">> ");
                    string? input = Console.ReadLine();
                    if (input != null)
                    {
                        Console.Clear();
                        break;
                    }
                }
                if (isPlayerDead)
                {
                    Console.Clear();
                    Console.WriteLine("패배... 플레이어가 빈사상태입니다. 던전 입구로 복귀합니다.");
                    Console.WriteLine();
                    break;
                }
                ++turn;
                Console.WriteLine();
                Console.WriteLine($"턴 {turn} 종료! 전투를 계속 진행하려면 Enter를 입력해주세요.");
                Console.Write(">> ");
                string? nextTurn = Console.ReadLine();
            }
        }

        public void NormalDungeon(Player player)
        {
            Console.WriteLine("Easy 던전에 입장하였습니다.");
            Console.WriteLine("스켈레톤 5마리가 등장합니다.");

            List<Monster> stageMonsterList = new List<Monster>();
            RewardGold = 500;
            for (int i = 0; i < 5; i++)
            {
                int randomLevel = random.Next(1, 4);
                Monster skeleton = new Monster("스켈레톤 병사", randomLevel,
                                               3 + randomLevel,
                                               1 + randomLevel / 2,
                                               25 + randomLevel * 2,
                                               200 + randomLevel * 100);
                stageMonsterList.Add(skeleton);
                skeleton.ShowMonsterState();
            }
            int turn = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("[전투 중 ...]");

                bool isPlayerDead = false;
                int deadCount = 0;
                int obtainGold = 0;
                foreach (Monster skeleton in stageMonsterList)
                {
                    int playersDamage = player.TotalAttack > skeleton.CharacterDefense ? player.TotalAttack - skeleton.CharacterDefense : 1;
                    int skeletonsDamage = skeleton.CharacterAttack > player.CharacterDefense ? skeleton.CharacterAttack - player.CharacterDefense : 1;
                    skeleton.CurrentHP -= playersDamage;
                    player.CurrentHP -= skeletonsDamage;
                    skeleton.ShowMonsterState();
                    if (skeleton.IsDead)
                    {
                        deadCount++;
                        obtainGold += skeleton.DropGold;
                    }
                    if (player.CurrentHP <= 10)
                    {
                        isPlayerDead = true;
                        break;
                    }
                }
                if (deadCount == stageMonsterList.Count)
                {
                    player.Gold += obtainGold + RewardGold;
                    Console.WriteLine($"승리! {obtainGold + RewardGold}G 를 획득했습니다.");
                    Console.WriteLine($"현재 체력은 {player.CurrentHP} / {player.TotalMaxHP} 입니다.");
                    Console.WriteLine();
                    Console.WriteLine("퇴장하려면 Enter를 입력해주세요.");
                    Console.Write(">> ");
                    string? input = Console.ReadLine();
                    if (input != null)
                    {
                        Console.Clear();
                        break;
                    }
                }
                if (isPlayerDead)
                {
                    Console.Clear();
                    Console.WriteLine("패배... 플레이어가 빈사상태입니다. 던전 입구로 복귀합니다.");
                    Console.WriteLine();
                    break;
                }
                ++turn;
                Console.WriteLine();
                Console.WriteLine($"턴 {turn} 종료! 전투를 계속 진행하려면 Enter를 입력해주세요.");
                Console.Write(">> ");
                string? nextTurn = Console.ReadLine();
            }
        }

        public void HardDungeon(Player player)
        {
            Console.WriteLine("Easy 던전에 입장하였습니다.");
            Console.WriteLine("스켈레톤 5마리가 등장합니다.");

            List<Monster> stageMonsterList = new List<Monster>();
            RewardGold = 500;
            for (int i = 0; i < 5; i++)
            {
                int randomLevel = random.Next(1, 4);
                Monster skeleton = new Monster("스켈레톤 병사", randomLevel,
                                               3 + randomLevel,
                                               1 + randomLevel / 2,
                                               25 + randomLevel * 2,
                                               200 + randomLevel * 100);
                stageMonsterList.Add(skeleton);
                skeleton.ShowMonsterState();
            }
            int turn = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("[전투 중 ...]");

                bool isPlayerDead = false;
                int deadCount = 0;
                int obtainGold = 0;
                foreach (Monster skeleton in stageMonsterList)
                {
                    int playersDamage = player.TotalAttack > skeleton.CharacterDefense ? player.TotalAttack - skeleton.CharacterDefense : 1;
                    int skeletonsDamage = skeleton.CharacterAttack > player.CharacterDefense ? skeleton.CharacterAttack - player.CharacterDefense : 1;
                    skeleton.CurrentHP -= playersDamage;
                    player.CurrentHP -= skeletonsDamage;
                    skeleton.ShowMonsterState();
                    if (skeleton.IsDead)
                    {
                        deadCount++;
                        obtainGold += skeleton.DropGold;
                    }
                    if (player.CurrentHP <= 10)
                    {
                        isPlayerDead = true;
                        break;
                    }
                }
                if (deadCount == stageMonsterList.Count)
                {
                    player.Gold += obtainGold + RewardGold;
                    Console.WriteLine($"승리! {obtainGold + RewardGold}G 를 획득했습니다.");
                    Console.WriteLine($"현재 체력은 {player.CurrentHP} / {player.TotalMaxHP} 입니다.");
                    Console.WriteLine();
                    Console.WriteLine("퇴장하려면 아무 키나 눌러주세요.");
                    Console.Write(">> ");
                    string? input = Console.ReadLine();
                    if (input != null)
                    {
                        Console.Clear();
                        break;
                    }
                }
                if (isPlayerDead)
                {
                    Console.Clear();
                    Console.WriteLine("패배... 플레이어가 빈사상태입니다. 던전 입구로 복귀합니다.");
                    Console.WriteLine();
                    break;
                }
                ++turn;
                Console.WriteLine();
                Console.WriteLine($"턴 {turn} 종료! 전투를 계속 진행하려면 Enter를 입력해주세요.");
                Console.Write(">> ");
                string? nextTurn = Console.ReadLine();
            }
        }
    }
}
