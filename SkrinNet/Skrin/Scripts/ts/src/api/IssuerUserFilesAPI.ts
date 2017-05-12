import { IUserFile } from '../models/IUserFile'




export default class IssuerUserFilesAPI {




    public static Get(issuer_id: string): Promise<IUserFile[]> {

        return new Promise<IUserFile[]>((resolve, reject) => {

            var xhr = new XMLHttpRequest();
            var params = 'issuer_id=' + issuer_id;
            xhr.open('GET', '/UserFile/Get?' + params, true);
            xhr.send();

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) {
                    return;
                }

                if (this.status != 200) {
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IUserFile[]>JSON.parse(this.responseText));

            }


        })
    }


    public static GetFileSizeLimit():Promise<number>{
        return new Promise<number>((resolve, reject) => {

            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/UserFile/GetFileSizeLimit/', true);
            xhr.send();

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) {
                    return;
                }

                if (this.status != 200) {
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<number>JSON.parse(this.responseText));

            }


        })
    }


    public static Upload(file: File, issuer_id: string, progress_func: (f: number) => any): Promise<IUserFile> {
        return new Promise<IUserFile>((resolve, reject) => {

            let formData = new FormData();
                formData.append("file", file);
                formData.append("issuer_id", issuer_id);

                var xhr = new XMLHttpRequest();

                xhr.upload.onprogress = function (event: any) {
                    let progres = Math.floor((Number(event.loaded) / Number(event.total))*100);
                    progress_func(progres);
                }

                xhr.onreadystatechange = function () {
                    if (this.readyState != 4) {
                        return;
                    }

                    if (this.status != 200) {
                        reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                    }

                    resolve(<IUserFile>JSON.parse(this.responseText));
                }

                xhr.open("POST", "/UserFile/Upload/", true);
                xhr.send(formData);

        });
    }

    public static Delete(file_id: string): Promise<IUserFile> {
        return new Promise<IUserFile>((resolve, reject) => {
            var xhr = new XMLHttpRequest();
            var params = 'file_id=' + file_id;
            xhr.open('GET', '/UserFile/Delete?' + params, true);
            xhr.send();

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) {
                    return;
                }

                if (this.status != 200) {
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IUserFile>JSON.parse(this.responseText));
            }
        });
    }
}