module ReportRegistry
open System.Collections.Generic


let createRegistry() =
    let dict = new Dictionary<string*string*bool,int>();
    let registerer (uri: string) (qname: string) strictFlag =
        let key = (uri, qname, strictFlag)
        match dict.TryGetValue key with
        | true, c  ->
            dict.[key] <- c + 1
        | false,_ ->
            dict.[key] <- 1
    let registryPrinter sw =
        for KeyValue((uri, qname, strictFlag), count) in dict do 
            if strictFlag then
                fprintfn sw "!%s,%s,%d" uri qname count
            else 
                fprintfn sw "%s,%s,%d" uri qname count
    (registerer, registryPrinter)
    