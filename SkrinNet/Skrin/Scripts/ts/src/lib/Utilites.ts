export function GetStyleExtention(extention: string | null | undefined) {
    let default_style_extention = "icon-doc doc";

    if (extention === null || extention === undefined) {
        return default_style_extention;
    }

    switch (extention.toLowerCase()) {
        case "jpg":
        case "bmp":
        case "tif":
        case "png":
            return "icon-file-image image";
        case "xml":
            return "icon-file-code code";
        case "doc":
        case "docx":
        case "rtf":
            return "icon-file-word word";
        case "pdf":
            return "icon-file-pdf pdf";
        case "xls":
        case "xlsx":
            return "icon-file-excel excel";
        case "zip":
        case "rar":
            return "icon-file-archive archive";
        default:
            return default_style_extention;
    }
}

export function GenerateGUID() {
    var d = new Date().getTime();
    if (window.performance && typeof window.performance.now === "function") {
        d += performance.now(); //use high-precision timer if available
    }
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}
/*
export function GetDate() {
    var today = new Date();
    var dd = String(today.getDate());
    var mm = String(today.getMonth()+1);
    var yyyy = String(today.getFullYear());

    if(Number(dd)<10) {
        dd='0'+dd
    } 

    if(Number(mm)<10) {
        mm='0'+mm
    } 

    return dd+'.'+mm+'.'+yyyy;
}*/

export function DeepCopy<T>(original:any):T{
    return <T>JSON.parse(JSON.stringify(original));
}

export function FormatBytes(bytes:number) {
    if(bytes < 1024) return bytes + " Bytes";
    else if(bytes < 1048576) return Number((bytes / 1024).toFixed(3)).toString().replace(".",",") + " KB";
    else if(bytes < 1073741824) return Number((bytes / 1048576).toFixed(3)).toString().replace(".",",") + " MB";
    else return Number((bytes / 1073741824).toFixed(3)).toString().replace(".",",") + " GB";
};

