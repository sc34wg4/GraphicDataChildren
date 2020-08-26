module ScanOoxmlFile

open System
open System.IO
open System.IO.Compression;
open System.Xml;
open OOXMLNamespaces

let getQName (reader: XmlReader) =
    let nsname = reader.NamespaceURI
    if nsname = "" then
        reader.LocalName
    else 
        let prefix = findPrefix nsname
        prefix +  ":" + reader.LocalName


let printQnameWhenGraphicDataChild qname  registerer stack =
    match stack with
    | uri::"s-a:graphicData"::_  ->
        registerer uri qname true
    | uri::"t-a:graphicData"::_ ->
        registerer uri qname false
    | _ -> ()


let rec readElement (reader: XmlReader)  registerer stack =
    let mutable endFlag = false
    while not(endFlag) && reader.Read() do
        match reader.NodeType with
        | XmlNodeType.Element ->
            if reader.IsEmptyElement then
                    if reader.NamespaceURI <> mceNamespace then 
                        let qname = getQName reader
                        printQnameWhenGraphicDataChild qname registerer stack
            else 
                let newStack =
                    if reader.NamespaceURI <> mceNamespace then 
                        let qname = getQName reader
                        printQnameWhenGraphicDataChild qname registerer stack
                        match getQName(reader) with 
                        | "s-a:graphicData" | "t-a:graphicData" as xx -> 
                            let uri = reader.GetAttribute("uri")
                            uri::xx::stack
                        | x -> x::stack
                    else 
                        stack
                readElement reader registerer newStack
        | XmlNodeType.EndElement -> 
            endFlag <- true
        | _ -> ()


let readDocument (entry: ZipArchiveEntry)   registerer  =
    try
        use stream = entry.Open()
        use sr = new StreamReader(stream)
        use reader = XmlReader.Create(sr)
        while not(reader.NodeType = XmlNodeType.Element) && reader.Read() do ()

        [getQName(reader)]
        |> readElement reader registerer 
     with
        | e -> ()


let scanOOXML zipFileName registerer =
    try
        let zipArchive = 
          ZipFile.OpenRead(zipFileName)
        let entries = zipArchive.Entries
        for entry in entries do
                if entry.Name.EndsWith(".xml") then
                    readDocument  entry  registerer
     with
        | e -> ()

