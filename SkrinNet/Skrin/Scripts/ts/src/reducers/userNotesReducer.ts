import update= require('react-addons-update');

import {RECEIVE_NOTES,RECEIVE_UPDATE_NOTE,RECEIVE_ADD_NOTE,RECEIVE_DELETE_NOTE,ADD_NEW_NOTE,DELETE_NEW_NOTE} from '../models/Constants'
import {IUserNote} from '../models/IUserNote'
import {IAction,IActionId} from '../models/Common'
import {IActionReceiveUserNotes,IActionUserNote,IActionReceiveUserNote} from '../actions/IssuerNoteActionCreators'
import {DeepCopy} from '../lib/Utilites'

export const user_notes=(state:IUserNote[]=[],action:IAction)=>{
    let note_index:number;
    let new_note:IUserNote;
    let exist_note:IUserNote;

    switch (action.type){
        case RECEIVE_NOTES:
            return DeepCopy<IActionReceiveUserNotes>(action).notes;
        case RECEIVE_ADD_NOTE:
            state.push(DeepCopy<IActionReceiveUserNote>(action).note);
            return[...state];
        case RECEIVE_DELETE_NOTE:
            new_note=DeepCopy<IActionReceiveUserNote>(action).note;
            note_index=state.indexOf(state.filter(p=>p.id===new_note.id)[0]);
            state.splice(note_index,1);
            return[...state];
        case RECEIVE_UPDATE_NOTE:
            new_note=DeepCopy<IActionReceiveUserNote>(action).note;
            note_index=state.indexOf(state.filter(p=>p.id===new_note.id)[0]);
            state[note_index]=new_note;
            return [...state];
        default:
            return state;

    }
}

export const new_user_notes=(state:IUserNote[]=[],action:IAction)=>{
    switch (action.type){
        case ADD_NEW_NOTE:
            state.push(DeepCopy<IActionUserNote>(action).note);
            return[...state];
        case DELETE_NEW_NOTE:
            let note_index=state.indexOf(state.filter(p=>p.id===(<IActionId>action).id)[0]);
            state.splice(note_index,1);
            return[...state];         
        default:
            return state;
    }
}