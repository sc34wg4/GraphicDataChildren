// Learn more about F# at http://fsharp.org

open System.IO
open EnumerateOoxmlFiles
open ScanOoxmlFile
open ReportRegistry

    
let scanComputer sw =
    for ooxmlFileInfo in  allOoxmlFilesOnThisComputer () do
        scanOOXML (ooxmlFileInfo.FullName) sw
    
[<EntryPoint>]
let main argv =
    let temp = System.Environment.GetEnvironmentVariable("TEMP")
    let allPath = temp + "\\AllGraphicDataChildren.csv"
    File.Delete(allPath)
    use outputFileStream = File.OpenWrite(allPath)
    use sw = new StreamWriter(outputFileStream)
    sw.WriteLine("uri,qname,count")
    let (registerer, registryPrinter) = createRegistry()
    scanComputer registerer
    registryPrinter sw
    0 // return an integer exit code

