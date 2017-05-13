import {IUserNote} from '../models/IUserNote'


export default class IssuerUserNotesAPI{


    public static Get(issuer_id: string): Promise<IUserNote[]> {

        return new Promise<IUserNote[]>((resolve, reject) => {

            var xhr = new XMLHttpRequest();
            var params='issuer_id='+issuer_id;
            xhr.open('GET','/UserNote/Get?'+params,true);
            xhr.send();

            xhr.onreadystatechange=function(){
                if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IUserNote[]>JSON.parse(this.responseText));

            }
            
        });

    }

    public static Update(content: string, issuer_id: string,note_id: string | null = null): Promise<IUserNote> {

        return new Promise<IUserNote>((resolve, reject) =>{
            let u_note: IUserNote = {
                id: note_id,
                content: content,
                user_id: null,
                issuer_id: issuer_id,
                update_date:null
            };

             var xhr = new XMLHttpRequest();
             xhr.open('POST','/UserNote/Update',true);
             xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
             xhr.send(JSON.stringify(u_note));

             xhr.onreadystatechange=function(){
                 if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IUserNote>JSON.parse(this.responseText));
             }

        })        
    }

    public static Delete(note_id:string): Promise<IUserNote>{
        return new Promise<IUserNote>((resolve, reject)=>{
            
            var xhr = new XMLHttpRequest();
            var params='note_id='+note_id;
            xhr.open('GET','/UserNote/Delete?'+params,true);
            xhr.send();

            xhr.onreadystatechange=function(){
                 if(this.readyState!=4){
                    return;
                }

                if(this.status!=200){
                    reject(new Error('ошибка: ' + (this.status ? this.statusText : 'запрос не удался')));
                }

                resolve(<IUserNote>JSON.parse(this.responseText));
             }
            
        });
        
    }
}