# Глоссарий

---

## 🎯 Цели контестов

- Поддерживать ум в тонусе через регулярную практику.
- Прокачка алгоритмического мышления, логики и навыков backend-разработки на C#.
- Углублённое понимание внутренних механизмов .NET и CLR.

---

## 🧠 Уровни сложности

- **Лёгкий** — задачи-разминки, не требуют сложных знаний, но требуют внимательности.
- **Средний** — задачи уровня опытного C# backend-разработчика, на логику и прикладные знания .NET.
- **Insane** — нестандартные, глубоко технические задачи, требующие глубокого понимания устройства CLR, памяти и поведения языка.

---

## 📜 Правила

- ❌ Не подглядывать в чужие решения.
- ❓ Все вопросы по условиям — только в личных сообщениях (не в общем чате).
- 🧠 Не использовать ИИ-агентов или поисковые системы для нахождения готовых решений.
- ✅ Цель — честная прокачка, а не "найти ответ".

---


# Контест от 2025.06.06

---

## Задача 1: Максимальное число из последовательности по позиции

**Уровень сложности:** Лёгкий

### Условие:

Дана последовательность различных цифр от 1 до *n* включительно в виде массива. Из этих цифр можно составить *k*-значное число без повторений (где *k* равен длине максимального числа, составляемого из массива чисел). Требуется вернуть цифру, находящуюся на заданной позиции в этом числе.

**Формат входных данных:**

* Массив `digits` — массив целых чисел от 1 до *n*.
* Целое число `position` — позиция, от 1 до *k*, которую нужно вернуть.

**Формат выходных данных:**

* Целое число — цифра на позиции `position` в составленном числе.

**Важно:**

* Позиции нумеруются с 1 (не с 0).
* Все цифры в массиве уникальны.

### Пример:

```text
Вход:
digits = [1, 2, 3, 4, 5]
position = 1

Результат: 5
```

```text
Вход:
digits = [1, 2, 3, 4, 5]
position = 4

Результат: 2
```
**Шаблон:** FindNumberAtPositionBySequenceTemplate.cs

---

## Задача 2: **Поиск сопоставлений между именами столбцов и свойствами с помощью регулярных выражений**

**Уровень сложности:** Средний

### Условие задачи

В SQL-запросе могут встречаться выражения вида:

```sql
SELECT
    column_name AS property_name
```

Требуется составить **регулярное выражение**, которое найдёт такие пары и создаст **две именованные группы**:
- `column` — имя столбца в запросе (до `AS`);
- `property` — имя алиаса (после `AS`).

Регулярное выражение должно быть применимо к тексту запроса и поддерживать множественные совпадения (флаг `/g`) в нечувствительном к регистру (`/i`) и мультистрочном (`/m`) режиме.

### Требования

- Вернуть **строку**, содержащую регулярное выражение.
- Регулярное выражение должно использовать **именованные группы** `column` и `property`.
- Алиасы и имена столбцов могут быть разделены произвольным количеством пробелов и табуляции.

### Формат выходных данных

- Одна строка — регулярное выражение в виде `string`.

### Пример

На входе — SQL-запрос в составе C#-метода:

```csharp
SELECT
    id AS id, 
    code AS code, 
    payer_account_number AS payeraccountnumber,
    legal_entity AS legalentity
FROM read.account_contracts
```

**Результат сопоставления:**
- Группы `column`: `["id", "code", "payer_account_number", "legal_entity"]`
- Группы `property`: `["id", "code", "payeraccountnumber", "legalentity"]`

### Шаблон

Имя шаблона: `FindColumnToPropertyMappingsTemplate.cs`

---

## Задача 3: **Тень всемогущества**

**Уровень сложности:** Insane

### Цель

Реализовать метод:

```csharp
public void DoIt(string source, string target)
```

Так, чтобы после его выполнения значение переменной `source` в вызывающем методе стало равно `target`, либо его префиксу — **в зависимости от длины**.

### Ключевое ограничение

В C# строки:

* **immutable** (неизменяемые);
* передаются в методы **по значению**, даже если это ссылочный тип.

**Тем не менее, цель задачи — добиться того, чтобы значение `source` изменилось в вызывающем коде.**

### Правила трансформации

* Если длина `source` ≥ длины `target`, нужно полностью заменить `source` на `target`.
* Если длина `source` < длины `target`, заменить `source` на первые `source.Length` символов из `target`.

### Пример теста

```csharp
[Fact]
public void Test()
{
    var text = "this is a magic bro";
    DoIt(text, "good luck have fun!");
    Assert.Equal("good luck have fun!", text); // ← этот тест должен пройти

    var shortText = "abc";
    DoIt(shortText, "hello world");
    Assert.Equal("hel", shortText); // ← длина исходной строки 3
}
```

### Требуется

* Метод `DoIt`, изменяющий `source` настолько глубоко, что изменения отражаются **в вызывающем контексте**.
* Это возможно только с применением нестандартных техник.

### Шаблон

Имя шаблона: `ShadowOfAlmightyTemplate.cs`

---

# Контест от 2025.02.20
## Задача 1
*Первая задача будет разминкой для ума. Она является очень легкой и поэтому тут отсутсвуют бенчмарки и тесты.*<br>
<br>**Задание:**<br>
Дан массив возрастающих чисел от 0 до n в котором отсутствует одно число.
Необходимо найти отсутствующее число. Метод принимает массив чисел, необходимо вернуть отсутствующее число.
<br>**Пример:**  [0, 1, 2, 4, 5] => 3
<br>Решение оставляем в папке FindMissingNumber за основу берем FindMissingNumberTemplate.cs

## Задача 2
*Эта задача является небольшим вызовом и откроет для Вас что-то новое и интересное. Предстоит заняться самостоятельно ресерчем - что является частью задания. Желаю успехов.*<br>
<br>**Задание:**<br>
У нас есть проект *Problems.Analyzers.Sample*, в котором в папке *RenameContracts* находятся dtoшки которые не соответствуют правилам наименования:
* имя record должно содержать в себе: rootFolder + baseName + currentFolder
* rootFolder - корневая папка, в нашем случае - **CreateOrder**
* baseName - название dto
* currentFolder - папка в которой лежит dto: в нашем случае **Request** или **Response**
<br>

Необходимо с помощью Roslyn api написать анализатор и code-fix. Анализатор будет находить такие dto, а с помощью code-fix будет происходить исправление наименований.
Ожидается что code-fix исправит наименования так:
* папка Request
  * CreateOrder -> CreateOrderRequest
  * CreateOrderHeader -> CreateOrderHeaderRequest
  * CreateOrderHeaderParameters -> CreateOrderHeaderParametersRequest
* папка Response
  * CreateOrder -> CreateOrderResponse
  * Line -> CreateOrderLineResponse

Исправление и анализ должен производится **только** в папке *RenameContracts*.
<br>Принцип работы: 
* анализатор и код фикс размещаем в проекте Problems.Analyzers
* очищаем решение (solution clean)
* билдим Problems.Analyzers
* билдим Problems.Analyzers.Sample
* ide подсвечивает проблемные dto и предлагает исправление
* принимаем исправления
* при необходимости Problems.Analyzers.Tests используйте для тестов

Текста для анализатора:
```csharp
    public const string DiagnosticId = "AB0001";

    private static readonly LocalizableString Title = new LocalizableResourceString(
        nameof(Resources.AB0001Title),
        Resources.ResourceManager,
        typeof(Resources));
    
    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(
            nameof(Resources.AB0001MessageFormat),
            Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString Description =
        new LocalizableResourceString(
            nameof(Resources.AB0001Description),
            Resources.ResourceManager,
            typeof(Resources));
    
    private const string Category = "Naming";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        Title,
        MessageFormat,
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: Description);
```

