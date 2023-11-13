namespace PersonalProject01;

public enum ItemType
{
    Weapon,
    Armor
}

internal class Program
{
    // 플레이어 변수 선언
    private static Character player;
    private static Item steelarmor;
    private static Item rustysword;
    private static Item woodhelmet;
    private static Item hexagondagger;
    private static Item noblepants;
    private static Item dragonglove;
    private static Inventory inventory;

    static void Main(string[] args)
    {
        // 플레이어와 아이템의 데이터 셋팅
        GameDataSetting();
        // 게임 시작시 준비된 첫화면 보여주기
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        steelarmor = new Item(1, "무쇠갑옷", "방어력", 5, ItemType.Armor,  "무쇠로 만들어져 튼튼한 갑옷입니다");
        rustysword = new Item(2, "낡은 검", "공격력", 2, ItemType.Weapon, "쉽게 볼 수 있는 낡은 검 입니다");
        woodhelmet = new Item(3, "목제투구", "방어력", 1, ItemType.Armor, "간단히 구할 수 있는 모자 방어구입니다.");
        hexagondagger = new Item(4, "헥사곤단검", "공격력", 10, ItemType.Weapon, "이 시대에서 확인되지 못한 물질로 만든 단검입니다.");
        noblepants = new Item(5, "귀족의 바지", "방어력", 4, ItemType.Armor, "비싼데 왠지 사기를 당한거 같습니다.");
        dragonglove = new Item(6, "용의 장갑", "방어력", 8, ItemType.Armor, "귀한 용의 가죽으로 만든 단단한 장갑입니다.");

        // 아이텀 정보를 넣을 인벤토리 클래스 생성
        inventory = new Inventory();

        inventory.AddItem(steelarmor);
        inventory.AddItem(rustysword);
        inventory.AddItem(woodhelmet);
        inventory.AddItem(hexagondagger);
        inventory.AddItem(noblepants);
        inventory.AddItem(dragonglove);
    }

    static void UpdateStats()
    {
        player.UpdateStats(inventory.GetEquippedItems());
    }

    // 게임 시작시 첫 화면 보여주기
    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        // 숫자 입력하기 
        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    // 플레이어의 상태창 표시 
    static void DisplayMyInfo()
    {
        
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.Write($"공격력 : {player.Atk}");
        if (player.Atk != player.AlphaAtk) Console.WriteLine($" (+{player.Atk - player.AlphaAtk})");
        else Console.WriteLine();
        Console.Write($"방어력 : {player.Def}");
        if (player.Def != player.AlphaDef) Console.WriteLine($" (+{player.Def - player.AlphaDef})");
        else Console.WriteLine();
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 \n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("[아이템 목록]");
        inventory.Display();
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력하세요.");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                SelectItem();
                break;

        }
    }

    static void SelectItem()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착관리\n장착 및 해체할 아이템을 선택하세요.\n");
        Console.WriteLine("[아이템 목록]");
        inventory.Select();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(0, Inventory.items.Count);
        if(input == 0)
        {
            DisplayInventory();
            return;
        }
        else
        {
            inventory.EquipItem(input-1);
            UpdateStats();
            SelectItem();

        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

// 캐릭터 스텟 정보 참
public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; private set; }
    public int AlphaAtk { get; }
    public int Def { get; private set; }
    public int AlphaDef { get; }
    public int Hp { get; }
    public int Gold { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        AlphaAtk = atk;
        Def = def;
        AlphaDef = def;
        Hp = hp;
        Gold = gold;
    }

    public void UpdateStats(List<Item> equippedItems)
    {
        int totalAttack = AlphaAtk;
        int totalDefence = AlphaDef;

        foreach(var item in equippedItems)
        {
            if(item.IType == ItemType.Weapon && item.IsEquipped == true)
            {
                totalAttack += item.Stat;
            }
            else if(item.IType == ItemType.Armor && item.IsEquipped == true)
            {
                totalDefence += item.Stat;
            }
        }

        Atk = totalAttack;
        Def = totalDefence;
    }
}

public class Item
{
    public int Index { get; }
    public string Name { get; }
    public string StatInfo { get; }
    public int Stat { get; }
    public string Description { get; }
    public ItemType IType { get; }
    public bool IsEquipped { get; set; }

    public Item(int index, string name, string statInfo, int stat, ItemType iType, string description)
    {
        Index = index;
        Name = name;
        StatInfo = statInfo;
        Stat = stat;
        IType = iType;
        Description = description;
        IsEquipped = false;
    }
}



class Inventory
{
    public static List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void Display()
    {
        foreach(var item in items)
        {
            string equipMark = item.IsEquipped ? "[E]" : "";
            Console.WriteLine($"- {equipMark} {item.Name}  | {item.StatInfo} + {item.Stat} | {item.Description}");
        }
    }

    public void Select()
    {
        for(int i = 0; i< items.Count; i++)
        {
            string equipMark = items[i].IsEquipped ? "[E]" : "";
            Console.WriteLine($"{i + 1}. {equipMark} {items[i].Name}  | {items[i].StatInfo} + {items[i].Stat} | {items[i].Description}");
        }
    }

    public void EquipItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item itemToEquip = items[index];
            if (itemToEquip.IsEquipped == false)
            { 
                itemToEquip.IsEquipped = true;
            }
            else if (itemToEquip.IsEquipped == true)
            {
                itemToEquip.IsEquipped = false;
            }
            Console.WriteLine($"{itemToEquip.Name} 이(가) 장착되었습니다.");
        }
        else
        {
            Console.WriteLine("다시 입력해주세요.");
        }
    }

    public List<Item> GetEquippedItems()
    {
        return items.FindAll(item => item.IsEquipped);
    }
}
