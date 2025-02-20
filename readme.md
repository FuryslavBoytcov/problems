# Текущий контест
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

