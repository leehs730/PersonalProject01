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
    private static Inventory inventory;

    static void Main(string[] args)
    {
        // 플레이어와 아이템의 데이터 셋팅
        GameDataSetting();
        // 게임 시작시 준비된 첫화면 보여주기
        DisplayGameFirstIntro();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이텀 정보를 넣을 인벤토리 클래스 생성 
        inventory = new Inventory();

        inventory.AddItem(new Item(1, "무쇠갑옷", "방어력", 5, ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다"));
        inventory.AddItem(new Item(2, "낡은 검", "공격력", 2, ItemType.Weapon, "쉽게 볼 수 있는 낡은 검 입니다"));
        inventory.AddItem(new Item(3, "목제투구", "방어력", 1, ItemType.Armor, "간단히 구할 수 있는 모자 방어구입니다."));
        inventory.AddItem(new Item(4, "헥사곤단검", "공격력", 10, ItemType.Weapon, "이 시대에서 확인되지 못한 물질로 만든 단검입니다."));
        inventory.AddItem(new Item(5, "귀족의 바지", "방어력", 4, ItemType.Armor, "비싼데 왠지 사기를 당한거 같습니다."));
        inventory.AddItem(new Item(6, "용의 장갑", "방어력", 8, ItemType.Armor, "귀한 용의 가죽으로 만든 단단한 장갑입니다."));
    }

    // 각 아이템 클래스의 isEquipped 값에 따라 플레이어의 스탯 업데이트
    static void UpdateStats()
    {
        player.UpdateStats(inventory.GetEquippedItems());
    }

    static void DisplayGameFirstIntro()
    {
        Console.Clear();

        Console.WriteLine();
        Console.WriteLine("  ,------.   ,--. ,--. ,--.  ,--.  ,----.    ,------.  ,-----.  ,--.  ,--. ");
        Console.WriteLine("  |  .-.  \\  |  | |  | |  ,'.|  | '  .-./    |  .---' '  .-.  ' |  ,'.|  |  ");
        Console.WriteLine("  |  |  \\  : |  | |  | |  |' '  | |  | .---. |  `--,  |  | |  | |  |' '  |   ");
        Console.WriteLine("  |  '--'  / '  '-'  ' |  | `   | '  '--'  | |  `---. '  '-'  ' |  | `   |   ");
        Console.WriteLine("  `-------'   `-----'  `--'  `--'  `------'  `------'  `-----'  `--'  `--'   ");
        Console.WriteLine(",--------. ,------. ,--.   ,--. ,--------.     ,------.  ,------.   ,----. ");
        Console.WriteLine("'--.  .--' |  .---'  \\  `.'  /  '--.  .--'     |  .--. ' |  .--. ' '  .-./ ");
        Console.WriteLine("   |  |    |  `--,    .'    \\      |  |        |  '--'.' |  '--' | |  | .---.");
        Console.WriteLine("   |  |    |  `---.  /  .'.  \\     |  |        |  |\\  \\  |  | --'  '  '--'  | ");
        Console.WriteLine("   `--'    `------' '--'   '--'    `--'        `--' '--' `--'       `------'  ");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("            =======================================================");
        Console.WriteLine("                               PRESS TO ANY KEY                    ");
        Console.WriteLine("            =======================================================");
        Console.ReadKey();
    }

    // 게임 시작시 첫 화면 보여주기
    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write("스파르타");
        Console.ResetColor();
        Console.WriteLine(" 마을에 오신 여러분 환영합니다.");
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

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("상태보기");
        Console.ResetColor();
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
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

    // 인벤토리 아이템 목록 업데이트 
    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 \n보유 중인 아이템을 관리할 수 있습니다.\n");
        Console.WriteLine("                       [아이템 목록]");
        Console.WriteLine("============================================================");
        inventory.Display();
        Console.WriteLine("============================================================");
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("2. 아이템 정렬");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력하세요.");

        int input = CheckValidInput(0, 2);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                SelectItem();
                break;

            case 2:
                SortInventory();
                break;

        }
    }

    // 아이템을 장착 및 해제를 할 수 있는 기능 메서드
    static void SelectItem()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착관리\n장착 및 해체할 아이템을 선택하세요.\n");
        Console.WriteLine("                       [아이템 목록]");
        Console.WriteLine("============================================================");
        inventory.Select();
        Console.WriteLine("============================================================");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(0, inventory.items.Count);
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

    // 아이템을 유형에 따라 정렬할 수 있습니다.
    static void SortInventory()
    {
        Console.Clear();

        Console.WriteLine("[아이템 정렬]\n원하시는 아이템의 정렬을 선택하세요.");

        Console.WriteLine("1. 이름");
        Console.WriteLine("2. 장비 유형");
        Console.WriteLine("3. 스탯");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 유형을 입력해 주십시오.");

        int input = CheckValidInput(0, 3);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;

            case 1:
                SortedName(ref inventory.items);
                break;

            case 2:
                SortedType(ref inventory.items);
                break;
            case 3:
                SortedStats(ref inventory.items);
                break;
        }
    }

    // 아이템 정렬기능
    // 아이템 정렬 기준의 구성 -> (오름차순,내림차순) 이름순, 타입순, 스탯순
    // 아이템의 정렬 선택 순서 -> 이름, 아이템타입, 스탯 중 하나 선택 -> 오름차순 내림차순 선택 후 아이템 정렬-> 다시 목록 보여주기

    public static void SortedName(ref List<Item> items)
    {
        Console.WriteLine("원하시는 정렬 방식을 선택해 주세요.");
        Console.WriteLine();
        Console.WriteLine("1. 오름차순");
        Console.WriteLine("2. 내림차순");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                items = items.OrderBy(x => x.Name).ToList();
                SortInventory();
                break;

            case 2:
                items = items.OrderByDescending(x => x.Name).ToList();
                SortInventory();
                break;

        }
    }

    public static void SortedType(ref List<Item> items)
    {
        Console.WriteLine("원하시는 정렬 방식을 선택해 주세요.");
        Console.WriteLine();
        Console.WriteLine("1. 오름차순");
        Console.WriteLine("2. 내림차순");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                items = items.OrderBy(x => x.IType).ToList();
                SortInventory();
                break;

            case 2:
                items = items.OrderByDescending(x => x.IType).ToList();
                SortInventory();
                break;

        }
    }

    public static void SortedStats(ref List<Item> items)
    {
        Console.WriteLine("원하시는 정렬 방식을 선택해 주세요.");
        Console.WriteLine();
        Console.WriteLine("1. 오름차순");
        Console.WriteLine("2. 내림차순");
        Console.WriteLine();
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                items = items.OrderBy(x => x.Stat).ToList();
                SortInventory();
                break;

            case 2:
                items = items.OrderByDescending(x => x.Stat).ToList();
                SortInventory();
                break;

        }
    }

    // 목록 및 선택 가능한 숫자의 범위를 넘지 않도록 제한을 두는 메서드
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

// 캐릭터 스텟 정보 클래스 
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

    // 아이템 장착에 따른 플레이어 스텟의 변화
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

// 아이템 정보 클래스 
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


// 인벤토리의 기능을 구연한 클래스
class Inventory
{
    // 준비된 아이템 정보들을 리스트에 저장
    public List<Item> items = new List<Item>();

    // 리스트에 아이템 정보를 추가하는 메서드
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    // 리스트에 있는 아이템 정보들을 제거하는 메서드
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // 리스트에 있는 아이템 정보 목록들을 출력해주는 메서드, 추가로 아이템 장착 유무에 따른 표시 기능 구현
    public void Display()
    {
        foreach(var item in items)
        {
            string equipMark = item.IsEquipped ? "[E]" : "";
            Console.WriteLine($"- {equipMark} {item.Name, -7}  | {item.StatInfo} + {item.Stat} | {item.Description, -40:C}");
        }
    }

    // 리스트에 있는 아이템 정보들을 번호와 함깨 출력함
    public void Select()
    {
        for(int i = 0; i< items.Count; i++)
        {
            string equipMark = items[i].IsEquipped ? "[E]" : "";
            Console.WriteLine($"{i + 1}. {equipMark} {items[i].Name,-7}  | {items[i].StatInfo} + {items[i].Stat} | {items[i].Description, -40:C}");
        }
    }

    // 아이템을 장착 및 해제 할 수 있는 메서드
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


    // 아이템 클래스의 isEquipped 변수의 값들을 가져옴
    public List<Item> GetEquippedItems()
    {
        return items.FindAll(item => item.IsEquipped);
    }
}
