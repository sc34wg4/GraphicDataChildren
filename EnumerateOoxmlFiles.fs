
module EnumerateOoxmlFiles

open System.IO


let enumerateOoxmlFiles (dirInfo: DirectoryInfo) = 
    let docx =
        try dirInfo.EnumerateFiles("*.docx")
        with | e -> List.toSeq []
    let pptx =
        try dirInfo.EnumerateFiles("*.pptx")
        with | e -> List.toSeq []
    let xlsx =
        try dirInfo.EnumerateFiles("*.xlsx") 
        with | e -> List.toSeq []
    Seq.append docx (Seq.append pptx xlsx)

let rec enumerateDirectoriesRecursively (dirInfo: DirectoryInfo) = 
    let subs =
        try
            let subdirs = dirInfo.EnumerateDirectories()
            seq { for subdir in subdirs do 
                    yield! enumerateDirectoriesRecursively subdir }
        with | e -> List.toSeq []
    seq { yield dirInfo; yield! subs}

let enumrateOoxmlFilesRecursively  (dirInfo: DirectoryInfo) = 
    seq { for di in enumerateDirectoriesRecursively dirInfo do
            yield! enumerateOoxmlFiles di }
    
let allOoxmlFilesOnThisComputer () =
    let drives = DriveInfo.GetDrives()
    seq { for drive in drives do
            if drive.IsReady  then
                System.Console.WriteLine(drive.Name)
                let ooxmlFiles = enumrateOoxmlFilesRecursively(drive.RootDirectory)
                yield! ooxmlFiles }
