module OOXMLNamespaces

[<Literal>]
let mceNamespace = "http://schemas.openxmlformats.org/markup-compatibility/2006"

let strictOOXMLNamespaces =
    [("s-a",  "http://purl.oclc.org/ooxml/drawingml/main");
    ("s-cdr",  "http://purl.oclc.org/ooxml/drawingml/chartDrawing");
    ("s-dchrt",  "http://purl.oclc.org/ooxml/drawingml/chart");
    ("s-ddgrm",  "http://purl.oclc.org/ooxml/drawingml/diagram");
    ("s-dlckcnv",  "http://purl.oclc.org/ooxml/drawingml/lockedCanvas");
    ("s-dpct",  "http://purl.oclc.org/ooxml/drawingml/picture");
    ("s-m",  "http://purl.oclc.org/ooxml/officeDocument/math");
    ("s-o",  "urn:schemas-microsoft-com:office:office");
    ("s-p",  "http://purl.oclc.org/ooxml/presentationml/main");
    ("s-r",  "http://purl.oclc.org/ooxml/officeDocument/relationships");
    ("s-s",  "http://purl.oclc.org/ooxml/officeDocument/sharedTypes");
    ("s-shdCstm",  "http://purl.oclc.org/ooxml/officeDocument/customProperties");
    ("s-shdDcEP",  "http://purl.oclc.org/ooxml/officeDocument/extendedProperties");
    ("s-shrdBib",  "http://purl.oclc.org/ooxml/officeDocument/bibliography");
    ("s-shrdChr",  "http://purl.oclc.org/ooxml/officeDocument/characteristics");
    ("s-sl",  "http://purl.oclc.org/ooxml/schemaLibrary/main");
    ("s-v",  "urn:schemas-microsoft-com:vml");
    ("s-vt",  "http://purl.oclc.org/ooxml/officeDocument/docPropsVTypes");
    ("s-w",  "http://purl.oclc.org/ooxml/wordprocessingml/main");
    ("s-w10",  "urn:schemas-microsoft-com:office:word");
    ("s-wp",  "http://purl.oclc.org/ooxml/drawingml/wordprocessingDrawing");
    ("s-x",  "urn:schemas-microsoft-com:office:excel");
    ("s-xdr",  "http://purl.oclc.org/ooxml/drawingml/spreadsheetDrawing")]

let transitionalOOXMLNamespaces = 
    [("t-a", "http://schemas.openxmlformats.org/drawingml/2006/main");
    ("t-cdr", "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing");
    ("t-dchrt", "http://schemas.openxmlformats.org/drawingml/2006/chart");
    ("t-ddgrm", "http://schemas.openxmlformats.org/drawingml/2006/diagram");
    ("t-dlckcnv", "http://schemas.openxmlformats.org/drawingml/2006/lockedCanvas");
    ("t-dpct", "http://schemas.openxmlformats.org/drawingml/2006/picture");
    ("t-m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
    ("t-o", "urn:schemas-microsoft-com:office:office");
    ("t-p", "http://schemas.openxmlformats.org/presentationml/2006/main");
    ("t-pvml", "urn:schemas-microsoft-com:office:powerpoint");
    ("t-r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
    ("t-s", "http://schemas.openxmlformats.org/officeDocument/2006/sharedTypes");
    ("t-shdCstm", "http://schemas.openxmlformats.org/officeDocument/2006/custom-properties");
    ("t-shdDcEP", "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties");
    ("t-shrdBib", "http://schemas.openxmlformats.org/officeDocument/2006/bibliography");
    ("t-shrdChr", "http://schemas.openxmlformats.org/officeDocument/2006/characteristics");
    ("t-sl", "http://schemas.openxmlformats.org/schemaLibrary/2006/main");
    ("t-v", "urn:schemas-microsoft-com:vml");
    ("t-vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
    ("t-w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
    ("t-w10", "urn:schemas-microsoft-com:office:word");
    ("t-wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
    ("t-x", "urn:schemas-microsoft-com:office:excel");
    ("t-xdr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
    ("t-sml",  "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]

let otherNamespaces = 
    [("ds",    "http://www.w3.org/2000/09/xmldsig#");
    ("xsd",    "http://www.w3.org/2001/XMLSchema");
    ("mce" ,   "http://schemas.openxmlformats.org/markup-compatibility/2006");
    ("xmlns", "http://www.w3.org/2000/xmlns/")];


let allOOXMLNamespaces = 
    List.append 
        (List.append strictOOXMLNamespaces transitionalOOXMLNamespaces) 
        otherNamespaces

let findPrefix uri =
    let attempt = List.tryFind (fun (_, y) -> y = uri) allOOXMLNamespaces
    match attempt with
    | Some(x) -> fst x
    | None -> uri

