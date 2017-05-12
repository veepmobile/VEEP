
export default class AccessCloudAPI{

    public static CanUseCloud():Promise<boolean>{
        return new Promise<boolean>((resolve, reject) => {

            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/AccessCloud/CheckCloudUsing/', true);
            xhr.send();

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) {
                    return;
                }

                if (this.status != 200) {
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<boolean>JSON.parse(this.responseText));

            }


        });
    }

    public static ConfirmUseCloud():Promise<boolean>{
        return new Promise<boolean>((resolve, reject) => {

            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/AccessCloud/ConfirmCloudUsing/', true);
            xhr.send();

            xhr.onreadystatechange = function () {
                if (this.readyState != 4) {
                    return;
                }

                if (this.status != 200) {
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<boolean>JSON.parse(this.responseText));

            }


        });
    }
}