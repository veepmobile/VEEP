import {IAddress,IAddressField} from '../models/IAddress'

export default class IssuerUserAddressesAPI
{
    

    public static Get(issuer_id: string): Promise<IAddress[]> {
        
        return new Promise<IAddress[]>((resolve, reject) => {
            var xhr = new XMLHttpRequest();
            var params='issuer_id='+issuer_id;
            xhr.open('GET','/UserAddress/Get?'+params,true);
            xhr.send();

            xhr.onreadystatechange=function(){
                if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IAddress[]>JSON.parse(this.responseText));

            }
        })
    }

    public static Update(address:IAddress):Promise<IAddress>{


        return new Promise<IAddress>((resolve, reject) => {
             var xhr = new XMLHttpRequest();
             xhr.open('POST','/UserAddress/Update',true);
             xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
             xhr.send(JSON.stringify(address));

             xhr.onreadystatechange=function(){
                 if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IAddress>JSON.parse(this.responseText));
             }

        });
    }

    public static Delete(id:string): Promise<IAddress>{
        return new Promise<IAddress>((resolve, reject)=>{
            var xhr = new XMLHttpRequest();
            var params='address_id='+id;
            xhr.open('GET','/UserAddress/Delete?'+params,true);
            xhr.send();

            xhr.onreadystatechange=function(){
                 if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IAddress>JSON.parse(this.responseText));
             }
        });
    }
}