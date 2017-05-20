import {REQUEST_NOTES,RECEIVE_NOTES,REQUEST_UPDATE_NOTE,RECEIVE_UPDATE_NOTE,REQUEST_DELETE_NOTE,RECEIVE_DELETE_NOTE,
    REQUEST_ADD_NOTE,RECEIVE_ADD_NOTE,ADD_NEW_NOTE,DELETE_NEW_NOTE} from '../models/Constants'

import {IAction,IActionId} from '../models/Common'

import {IUserNote} from '../models/IUserNote'
import IssuerUserNotesAPI from '../api/IssuerUserNotesAPI'
import {ModalActionCreators} from '../actions/ModalActionCreators'

export interface IActionReceiveUserNotes extends IAction{
    notes:IUserNote[]
}

export interface IActionReceiveUserNote extends IAction{
    note:IUserNote
}

export interface IActionUserNote extends IAction{
    note:IUserNote
}


export  class IssuerNoteActionCreators
{
    private static _requestNotes():IAction{
        return{type:REQUEST_NOTES}
    }

    private static _recieveNotes(notes:IUserNote[]):IActionReceiveUserNotes{
        return{type:RECEIVE_NOTES,notes:notes}
    }


    static GetNotes(issuer_id:string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestNotes());
                let notes = await IssuerUserNotesAPI.Get(issuer_id);
                dispatch(this._recieveNotes(notes));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка загрузки заметок: " + (error.message || error)));
            }
        }
    }

    private static _requestUpdateNote():IAction{
        return{type:REQUEST_UPDATE_NOTE}
    }

    private static _recieveUpdateNote(note:IUserNote):IActionReceiveUserNote{
        return{type:RECEIVE_UPDATE_NOTE,note:note}
    }



    static UpdateNote(content: string,issuer_id:string,note_id: string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestUpdateNote());
                let note = await IssuerUserNotesAPI.Update(content,issuer_id,note_id);
                dispatch(this._recieveUpdateNote(note));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка обновления заметки: " + (error.message || error)));
            }
        }
    }

    private static _requestAddNote():IAction{
        return{type:REQUEST_ADD_NOTE}
    }

    private static _recieveAddNote(note:IUserNote):IActionReceiveUserNote{
        return{type:RECEIVE_ADD_NOTE,note:note}
    }


    static AddNote(note_id:string,content: string, issuer_id:string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestAddNote());
                let note =  await IssuerUserNotesAPI.Update(content,issuer_id);
                 dispatch(this._recieveAddNote(note));
                 dispatch(this.DeleteNewNote(note_id));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка добавления заметки: " + (error.message || error)));
            }
        }
    }

    private static _requestDeleteNote():IAction{
        return{type:REQUEST_DELETE_NOTE}
    }

    private static _recieveDeleteNote(note:IUserNote):IActionReceiveUserNote{
        return{type:RECEIVE_DELETE_NOTE,note:note}
    }


    static DeleteNote(note_id: string){
        return async (dispatch:any)=>{
            try{
                dispatch(this._requestDeleteNote());
                let note = await IssuerUserNotesAPI.Delete(note_id);
                dispatch(this._recieveDeleteNote(note));
            }
            catch(error){
                dispatch(ModalActionCreators.showModal("Ошибка удаления заметки: " + (error.message || error)));
            }
        }
    }

    static AddNewNote(note:IUserNote):IActionUserNote{
        return {type:ADD_NEW_NOTE,note:note}
    }

    static DeleteNewNote(note_id:string):IActionId{
        return{type:DELETE_NEW_NOTE,id:note_id}
    }
}
