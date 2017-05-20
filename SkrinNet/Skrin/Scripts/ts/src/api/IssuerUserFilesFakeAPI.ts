import { IUserFile } from '../models/IUserFile'

function GenerateGUID() {
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

function GetDate() {
    var today = new Date();
    var dd = String(today.getDate());
    var mm = String(today.getMonth() + 1);
    var yyyy = String(today.getFullYear());

    if (Number(dd) < 10) {
        dd = '0' + dd
    }

    if (Number(mm) < 10) {
        mm = '0' + mm
    }

    return dd + '.' + mm + '.' + yyyy;
}


export default class IssuerUserFilesFakeAPI {

    private static total_size_limit = 209715200; //200MB

    private static fake_files: IUserFile[] = [
        {
            id: GenerateGUID(),
            file_name: 'dogovor.docx',
            user_id: 889,
            issuer_id: '3DC84DD11D61CC51C32567400032199E',
            update_date: '28.11.2016',
            file_size: 2411724
        },
        {
            id: GenerateGUID(),
            file_name: 'contract.pdf',
            user_id: 889,
            issuer_id: '3DC84DD11D61CC51C32567400032199E',
            update_date: '27.11.2016',
            file_size: 4718592
        }
    ];






    public static Get(issuer_id: string): Promise<IUserFile[]> {
        let files = this.fake_files.filter(file => {
            return file.issuer_id == issuer_id
        });
        return new Promise<IUserFile[]>((resolve, reject) => {
            resolve(JSON.parse(JSON.stringify(files)));
        })
    }
    /*
    public static Upload(file: File, issuer_id: string, uploaded_size: number, progress_func: (f: number) => any, complete_func: (file: IUserFile) => any, error_func: (error: any) => any): void {

        if (uploaded_size + file.size > this.total_size_limit) {
            error_func("Превышен общий размер разрешенной загрузки");
        }
        else {
            let u_file: IUserFile = {
                id: GenerateGUID(),
                file_name: file.name,
                file_size: file.size,
                user_id: 889,
                issuer_id: issuer_id,
                update_date: GetDate()
            };
            this.fake_files.push(u_file);

            for (let i = 0; i < 10; i++) {
                setTimeout(function () {
                    progress_func((i + 1) * 10);
                }, i * 300);
            }
            setTimeout(function () {
                complete_func(u_file);
            }, 3000);
        }


}*/

    public static Upload(file: File, issuer_id: string, uploaded_size: number,progress_func: (f: number) => any): Promise<IUserFile>{
        return new Promise<IUserFile>((resolve, reject) =>{
            
            if (uploaded_size + file.size > this.total_size_limit) {
                reject("Превышен общий размер разрешенной загрузки");
            }
            else
            {
                 let u_file: IUserFile = {
                    id: GenerateGUID(),
                    file_name: file.name,
                    file_size: file.size,
                    user_id: 889,
                    issuer_id: issuer_id,
                    update_date: GetDate()
                };

                this.fake_files.push(u_file);

                for (let i = 0; i < 10; i++) {
                    setTimeout(function () {
                        progress_func((i + 1) * 10);
                    }, i * 300);
                }
                setTimeout(function () {
                    resolve(u_file);
                }, 3000);
                }           
        });
    }

    public static Delete(file_id: string): Promise<IUserFile> {
        return new Promise<IUserFile>((resolve, reject) => {
            let files = this.fake_files.filter((file) => file.id === file_id);
            if (files.length !== 1) {
                reject(new Error("file not found"));
            }
            let file_index = this.fake_files.indexOf(files[0]);
            this.fake_files.splice(file_index, 1);
            resolve(JSON.parse(JSON.stringify(files[0])));
        });
    }
}