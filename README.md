# 개인과제 마크다운 문서 작성

# Chapter2 개인과제 설명_이형석

## 개인과제 기능 구현 완료 리스트

1. [필수] 게임 시작 화면(O)
2. [필수] 플레이어의 상태 보기(O)
3. [필수] 인벤토리 목록 구현(O)
3-1. [필수] 장착 관리 구현(O)
4. [선택] 아이템 정보를 클래스/ 구조체로 활용(O)
5. [선택] 아이템 추가하기(O)

## 기능별 코드 설명

### 플레이어 및 아이템의 클래스 선언

플레이어 및 아이템의 클래스 선언은 플레이어 스텟 및 각각의 아이템들의 정보들을 따로 선언하도록 할 수 있습니다. 추가로 플레이어 클래스의 UpdateStats 메서드는 입력된 아이템 정보의 isEquipped의 bool 값에 따라서 아이템 스텟을 플레이어의 스탯에 반영하도록 함

```csharp
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
```

### 인벤토리 기능 구현

아이템들의 정보 선언 후 리스트에 저장 및 수정 할 수 있는 인벤토리 기능을 하는 클래스를 추가하였습니다. 아이템 추가, 제거, 목록 출력, 아이템 장착 및 제거할 수 있는 기능이 구현되어 있습니다.

- EquipItem 메서드 : 이 메서드는 장비 장착 선택 시 해당 아이템의 구조체 Bool 변수 값 isEquipped에 따라서 아이템 이름 옆의 장착 표시를 결정짓도록 바꿀 수 있습니다.

```csharp
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
```

### (위의 플레이어, 아이템 클래스에서)
아이템 장착에 따른 스탯 정보 업데이트 표시

플레이어의 스탯 변화는 아이템 장착에 따라서 업데이트가 되는 기능이 있는 메서드입니다. 이 메서드는 parameter로 넣을 Item 타입의 리스트를 foreach 문으로 탐색해 안에 있는 구조체 변수들 중 isEquipped의 true 값이 있다면 플레이어의 스탯 값도 더해지도록 업데이트가 되어서, 플레이어의 상태를 표시하면 업데이트된 플레이어의 스텟 값을 알 수 있습니다.

```csharp
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
```

TO BE CONTINUE...
 
