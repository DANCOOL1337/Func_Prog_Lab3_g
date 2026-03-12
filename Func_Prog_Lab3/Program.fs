open System

//Zadanie 1
(*
//Основная последовательность
let validBinaries = seq [ "1"; "10"; "11"; "100"; "101"
    ; "110"; "111"; "1000"; "1001" ]
// Преобразование
let toDecimal (binary: string) =
    binary
    |> Seq.rev
    |> Seq.mapi (fun i c -> 
        (int c - int '0') * int (Math.Pow(2.0, float i)))
    |> Seq.sum

// Запрос ввода 
let rec getBinaryValue index =
    printf "%d) Введите двоичное число (1-9): " index
    let input = Console.ReadLine().Trim()
    
    // Проверка
    if Seq.contains input validBinaries 
    then
        toDecimal input
    else
        printfn "Ошибка: '%s' не является 
            двоичным представлением цифр 1-9." input
        getBinaryValue index
// Запуск
let startConversionProcess () =
    printf "Введите количество чисел: "
    match Int32.TryParse(Console.ReadLine()) with
    | true, n when n > 0 ->
        printfn "Введите %d двоичных чисел от 1 до 1001:" n
        
        let results = 
            seq { 1 .. n }
            |> Seq.map getBinaryValue
            |> Seq.toList 

        printfn "\nДесятичные представления:"
        // mapi, чтобы пользоваться индексом
        results 
        |> Seq.mapi (fun i v -> (i + 1, v))
        |> Seq.iter (fun (idx, value) -> printfn "%d) %d" idx value)
    | _ -> 
        printfn "Ошибка: Введите целое положительное число."

[<EntryPoint>]
let main argv =
    startConversionProcess ()
    0
*)



//Zadanie 2

(*
// Проверка
let isValidHexChar (c: char) =
    let upperC = Char.ToUpper(c)
    (upperC >= '0' && upperC <= '9') 
        || (upperC >= 'A' && upperC <= 'F')

// Перевод символа в число (0-15)
let hexCharToInt (c: char) =
    let upperC = Char.ToUpper(c)
    if upperC >= '0' && upperC <= '9' 
    then 
        int upperC - int '0'
    else 
        int upperC - int 'A' + 10

// Ввод
let rec getValidHexChar index =
    printf "%d) " index
    let input = Console.ReadLine().Trim()
    
    // Проверка на 1 ввод
    if Seq.length input = 1 && isValidHexChar (Seq.head input) 
    then
        Char.ToUpper(Seq.head input)
    else
        printfn "Ошибка: Введите ровно один символ (0-9 или A-F)."
        getValidHexChar index

// Вычисление
let calculateHexValue (hexChars: seq<char>) =
    hexChars 
    |> Seq.fold 
        (fun state c -> state * 16L + int64 (hexCharToInt c)) 0L

//Запуск
let runHexProgram () =
    printf "Сколько 16-ричных символов вы хотите ввести? "
    match Int32.TryParse(Console.ReadLine()) with
    | true, n when n > 0 ->
        printfn "Введите %d символов по одному (0-9, A-F):" n
        
        
        let validChars = 
            seq { 1 .. n }
            |> Seq.map getValidHexChar
            |> Seq.toList 
            
        
        let finalDecimalValue = calculateHexValue validChars
        
        // Вывод
        let hexString = String.Join("", validChars)
        printfn "\n--- Результат ---"
        printfn "Введенное 16-ричное число: %s" hexString
        printfn "Десятичное значение: %d" finalDecimalValue

    | _ -> 
        printfn "Ошибка: Вводите целое положительное число."

[<EntryPoint>]
let main argv =
    runHexProgram ()
    0
*)

//Zadanie 3
//(*
open System.IO

// Получение пути
let rec getValidDirectoryPath () =
    printf "Введите полный путь к каталогу (например, C:\\Windows):"
    let path = Console.ReadLine().Trim()
    
    if Directory.Exists(path) then
        path
    else
        printfn "Ошибка: Каталог '%s' не существует." path
        getValidDirectoryPath ()

// Обработка ( возвращает имя и колво файлов)
let getSubdirectoryStats (targetDir: string) =
    // Возвращает пути
    Directory.EnumerateDirectories(targetDir)
    |> Seq.map (fun subDirPath ->
        //  Возвращаем имя каталога без полного пути
        let dirName = Path.GetFileName(subDirPath)
        
        try
            let fileCount = 
                Directory.EnumerateFiles(subDirPath) 
                |> Seq.length
            
            
            (dirName, fileCount)
        with
        // Ошибки
        | :? UnauthorizedAccessException -> 
            (dirName + " [Нет доступа]", 0)
    )

let runDirectoryAnalysis () =
    let path = getValidDirectoryPath ()
    printfn "\nАнализ подкаталогов в: %s" path
    printfn "---------------------------------------------------"

    try
        let statsSequence = getSubdirectoryStats path
        
        // Seq.isEmpty проверяет наличие элементов
        if Seq.isEmpty statsSequence then
            printfn "В этом каталоге нет подкаталогов."
        else
            // Выводим
            statsSequence
            |> Seq.iter (fun (name, count) ->
                printfn "Папка: %-25s | Файлов внутри: %d" (sprintf "'%s'" name) count)
                
    with
    | ex -> printfn "Произошла ошибка при чтении основного каталога: %s" ex.Message

[<EntryPoint>]
let main argv =
    runDirectoryAnalysis ()
    0
//*)