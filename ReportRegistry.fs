module ReportRegistry
open System.Collections.Generic


let createRegistry() =
    let countDict = new Dictionary<string*string*bool,int>();
    let fileNameDict = new Dictionary<string*string*bool,Set<string>>();
    let registerer (uri: string) (qname: string) fileName strictFlag =
        let key = (uri, qname, strictFlag)
        match countDict.TryGetValue key with
        | true, c  ->
            countDict.[key] <- c + 1
        | false,_ ->
            countDict.[key] <- 1
        match fileNameDict.TryGetValue key with
        | true, st  ->
            fileNameDict.[key] <- Set.add fileName st
        | false,_ ->
            fileNameDict.[key] <- Set.singleton fileName
    let registryPrinter sw =
        let sortedKVs = 
            [for kv in countDict -> kv] 
            |> List.sortBy (fun (kv: KeyValuePair<_,_>) -> - kv.Value)
        for KeyValue((uri, qname, strictFlag), count) in sortedKVs do 
            if strictFlag then
                fprintf sw "!%s,%s,%d" uri qname count
            else 
                fprintf sw "%s,%s,%d" uri qname count
            if count < 10 then
                for fn in fileNameDict.[(uri, qname, strictFlag)] do
                    fprintf sw ", %s" fn
            sw.WriteLine()
    (registerer, registryPrinter)
    