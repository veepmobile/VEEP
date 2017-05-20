import {IUserNote} from '../models/IUserNote'
import { GenerateGUID, GetDate } from '../lib/Utilites'

export default class IssuerUserNotesFakeAPI{
    private static fake_notes: IUserNote[] = [
        {
            id: GenerateGUID(),
            content: 'Моя первая заметка. Здесь будет город-сад.',
            user_id: 889,
            issuer_id: '3DC84DD11D61CC51C32567400032199E',
            update_date: '12.12.2016'
        }
    ];

    public static Get(user_id: number, issuer_id: string): Promise<IUserNote[]> {
        let notes = this.fake_notes.filter(note => {
            return note.user_id == user_id && note.issuer_id == issuer_id
        });
        return new Promise<IUserNote[]>((resolve, reject) => {
            resolve(<IUserNote[]>JSON.parse(JSON.stringify(notes)));
        })
    }

    public static Update(content: string, user_id: number, issuer_id: string,note_id: string | null = null): Promise<IUserNote> {

        if(note_id===null){                       
            let u_note: IUserNote = {
                id: GenerateGUID(),
                content: content,
                user_id: user_id,
                issuer_id: issuer_id,
                update_date: GetDate()
            };
            this.fake_notes.push(u_note);


            
            return new Promise<IUserNote>((resolve, reject) => {
                setTimeout(function(){
                    resolve(JSON.parse(JSON.stringify(u_note)));
                },500);

            });
        }else{
                return new Promise<IUserNote>((resolve, reject)=>{
                let notes=this.fake_notes.filter((note)=>note.id===note_id);
                if(notes.length!==1){
                    reject(new Error("note not found"));
                }
                let note=notes[0];
                note.content=content;
                note.update_date=GetDate();
                resolve(JSON.parse(JSON.stringify(note)));
            });
        }        
    }

    public static Delete(note_id:string): Promise<IUserNote>{
        return new Promise<IUserNote>((resolve, reject)=>{
            let notes=this.fake_notes.filter((note)=>note.id===note_id);
            if(notes.length!==1){
                reject(new Error("note not found"));
            }
            let note_index = this.fake_notes.indexOf(notes[0]);
            this.fake_notes.splice(note_index,1);
            resolve(JSON.parse(JSON.stringify(notes[0])));
        });
        
    }
}